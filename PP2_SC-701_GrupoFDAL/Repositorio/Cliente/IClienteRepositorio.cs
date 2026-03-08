using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP2_SC_701_GrupoFDAL.Repositorio.Cliente
{
    public interface IClienteRepositorio
    {
        List<Entidades.Cliente> ObtenerClientes();
        Entidades.Cliente ObtenerClientePorId(int id);

        void AgregarCliente(Entidades.Cliente cliente);
        void ActualizarCliente(Entidades.Cliente cliente);
        void EliminarCliente(int id);

        // Extras para teléfonos (CRUD básico dentro del cliente)
        void AgregarTelefono(int clienteId, Entidades.Telefono telefono);
        void EliminarTelefono(int clienteId, int telefonoId);
    }
}
