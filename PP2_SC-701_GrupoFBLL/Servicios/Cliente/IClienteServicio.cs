using PP2_SC_701_GrupoFBLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP2_SC_701_GrupoFBLL.Servicios.Cliente
{
    public interface IClienteServicio
    {
        Task<CustomResponse<List<ClienteDto>>> ObtenerClientesAsync();
        Task<CustomResponse<ClienteDto>> ObtenerClientePorIdAsync(int id);

        Task<CustomResponse<ClienteDto>> AgregarClienteAsync(ClienteDto clienteDto);
        Task<CustomResponse<ClienteDto>> ActualizarClienteAsync(ClienteDto clienteDto);
        Task<CustomResponse<ClienteDto>> EliminarClienteAsync(int id);

        Task<CustomResponse<ClienteDto>> AgregarTelefonoAsync(int clienteId, TelefonoDto telefonoDto);
        Task<CustomResponse<ClienteDto>> EliminarTelefonoAsync(int clienteId, int telefonoId);
    }
}