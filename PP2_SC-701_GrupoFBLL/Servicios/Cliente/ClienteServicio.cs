using AutoMapper;
using PP2_SC_701_GrupoFBLL.Dtos;
using PP2_SC_701_GrupoFDAL.Repositorio.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PP2_SC_701_GrupoFBLL.Servicios.Cliente
{
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMapper _mapper;

        public ClienteServicio(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<ClienteDto>>> ObtenerClientesAsync()
        {
            var response = new CustomResponse<List<ClienteDto>>();
            var clientes = _clienteRepositorio.ObtenerClientes();
            response.Data = _mapper.Map<List<ClienteDto>>(clientes);
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> ObtenerClientePorIdAsync(int id)
        {
            var response = new CustomResponse<ClienteDto>();

            if (id <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id debe ser mayor a 0.";
                response.codigoStatus = 400;
                return response;
            }

            var cliente = _clienteRepositorio.ObtenerClientePorId(id);
            if (cliente == null)
            {
                response.esCorrecto = false;
                response.mensaje = "El cliente no existe.";
                response.codigoStatus = 404;
                return response;
            }

            response.Data = _mapper.Map<ClienteDto>(cliente);
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> AgregarClienteAsync(ClienteDto clienteDto)
        {
            var response = new CustomResponse<ClienteDto>();

            var errores = ValidarCliente(clienteDto, esNuevo: true);
            if (errores.Any())
            {
                response.esCorrecto = false;
                response.mensaje = string.Join(" | ", errores);
                response.codigoStatus = 400;
                return response;
            }

            // Regla: Identificación única
            var existeIdentificacion = _clienteRepositorio.ObtenerClientes()
                .Any(c => string.Equals(c.Identificacion?.Trim(), clienteDto.Identificacion?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (existeIdentificacion)
            {
                response.esCorrecto = false;
                response.mensaje = "Ya existe un cliente con esa identificación.";
                response.codigoStatus = 400;
                return response;
            }

            var entidad = _mapper.Map<PP2_SC_701_GrupoFDAL.Entidades.Cliente>(clienteDto);
            _clienteRepositorio.AgregarCliente(entidad);

            response.mensaje = "Cliente agregado correctamente.";
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> ActualizarClienteAsync(ClienteDto clienteDto)
        {
            var response = new CustomResponse<ClienteDto>();

            var errores = ValidarCliente(clienteDto, esNuevo: false);
            if (errores.Any())
            {
                response.esCorrecto = false;
                response.mensaje = string.Join(" | ", errores);
                response.codigoStatus = 400;
                return response;
            }

            var existente = _clienteRepositorio.ObtenerClientePorId(clienteDto.Id);
            if (existente == null)
            {
                response.esCorrecto = false;
                response.mensaje = "El cliente no existe.";
                response.codigoStatus = 404;
                return response;
            }

            // Regla: Identificación única (excluyendo el mismo Id)
            var existeIdentificacion = _clienteRepositorio.ObtenerClientes()
                .Any(c => c.Id != clienteDto.Id &&
                          string.Equals(c.Identificacion?.Trim(), clienteDto.Identificacion?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (existeIdentificacion)
            {
                response.esCorrecto = false;
                response.mensaje = "Ya existe otro cliente con esa identificación.";
                response.codigoStatus = 400;
                return response;
            }

            var entidad = _mapper.Map<PP2_SC_701_GrupoFDAL.Entidades.Cliente>(clienteDto);
            _clienteRepositorio.ActualizarCliente(entidad);

            response.mensaje = "Cliente actualizado correctamente.";
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> EliminarClienteAsync(int id)
        {
            var response = new CustomResponse<ClienteDto>();

            if (id <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El id debe ser mayor a 0.";
                response.codigoStatus = 400;
                return response;
            }

            var existente = _clienteRepositorio.ObtenerClientePorId(id);
            if (existente == null)
            {
                response.esCorrecto = false;
                response.mensaje = "El cliente no existe.";
                response.codigoStatus = 404;
                return response;
            }

            _clienteRepositorio.EliminarCliente(id);
            response.mensaje = "Cliente eliminado correctamente.";
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> AgregarTelefonoAsync(int clienteId, TelefonoDto telefonoDto)
        {
            var response = new CustomResponse<ClienteDto>();

            if (clienteId <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "El clienteId debe ser mayor a 0.";
                response.codigoStatus = 400;
                return response;
            }

            var cliente = _clienteRepositorio.ObtenerClientePorId(clienteId);
            if (cliente == null)
            {
                response.esCorrecto = false;
                response.mensaje = "El cliente no existe.";
                response.codigoStatus = 404;
                return response;
            }

            var errores = ValidarTelefono(telefonoDto);
            if (errores.Any())
            {
                response.esCorrecto = false;
                response.mensaje = string.Join(" | ", errores);
                response.codigoStatus = 400;
                return response;
            }

            var telEntidad = _mapper.Map<PP2_SC_701_GrupoFDAL.Entidades.Telefono>(telefonoDto);
            _clienteRepositorio.AgregarTelefono(clienteId, telEntidad);

            response.mensaje = "Teléfono agregado correctamente.";
            return response;
        }

        public async Task<CustomResponse<ClienteDto>> EliminarTelefonoAsync(int clienteId, int telefonoId)
        {
            var response = new CustomResponse<ClienteDto>();

            if (clienteId <= 0 || telefonoId <= 0)
            {
                response.esCorrecto = false;
                response.mensaje = "clienteId y telefonoId deben ser mayores a 0.";
                response.codigoStatus = 400;
                return response;
            }

            var cliente = _clienteRepositorio.ObtenerClientePorId(clienteId);
            if (cliente == null)
            {
                response.esCorrecto = false;
                response.mensaje = "El cliente no existe.";
                response.codigoStatus = 404;
                return response;
            }

            _clienteRepositorio.EliminarTelefono(clienteId, telefonoId);
            response.mensaje = "Teléfono eliminado correctamente.";
            return response;
        }

        // ----------------- Validaciones -----------------

        private List<string> ValidarCliente(ClienteDto clienteDto, bool esNuevo)
        {
            var errores = new List<string>();

            if (clienteDto == null)
            {
                errores.Add("El cliente no puede ser nulo.");
                return errores;
            }

            if (!esNuevo && clienteDto.Id <= 0)
                errores.Add("El Id del cliente debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(clienteDto.Nombre))
                errores.Add("El nombre es requerido.");

            if (string.IsNullOrWhiteSpace(clienteDto.Apellidos))
                errores.Add("Los apellidos son requeridos.");

            if (string.IsNullOrWhiteSpace(clienteDto.Identificacion))
                errores.Add("La identificación es requerida.");
            else
            {
                var esAlfanumerico = Regex.IsMatch(clienteDto.Identificacion.Trim(), @"^[a-zA-Z0-9]+$");
                if (!esAlfanumerico)
                    errores.Add("La identificación solo puede contener letras y números (sin espacios ni símbolos).");
            }


            // Email: súper básico (si viene vacío, no lo forzamos; si viene, que tenga forma válida)
            if (!string.IsNullOrWhiteSpace(clienteDto.Email) && !clienteDto.Email.Contains("@"))
                errores.Add("El email no tiene un formato válido.");

            // Si trae teléfonos, validar cada uno (sin “inventar” reglas raras)
            if (clienteDto.Telefonos != null)
            {
                foreach (var tel in clienteDto.Telefonos)
                    errores.AddRange(ValidarTelefono(tel));
            }

            return errores;
        }

        private List<string> ValidarTelefono(TelefonoDto telefonoDto)
        {
            var errores = new List<string>();

            if (telefonoDto == null)
            {
                errores.Add("El teléfono no puede ser nulo.");
                return errores;
            }

            if (string.IsNullOrWhiteSpace(telefonoDto.Numero))
                errores.Add("El número de teléfono es requerido.");

            if (string.IsNullOrWhiteSpace(telefonoDto.Tipo))
                errores.Add("El tipo de teléfono es requerido.");

            // Formato sencillo: acepta "88881111" o "8888-1111"
            if (!string.IsNullOrWhiteSpace(telefonoDto.Numero))
            {
                var ok = Regex.IsMatch(telefonoDto.Numero.Trim(), @"^\d{4}-?\d{4}$");
                if (!ok) errores.Add("El número debe tener formato ####-#### o ########.");
            }

            return errores;
        }
    }
}