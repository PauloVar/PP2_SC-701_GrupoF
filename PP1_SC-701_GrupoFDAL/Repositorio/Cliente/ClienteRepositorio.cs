using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP1_SC_701_GrupoFDAL.Repositorio.Cliente
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private List<Entidades.Cliente> clientes = new List<Entidades.Cliente>()
        {
            new Entidades.Cliente
            {
                Id = 1,
                Nombre = "Juan",
                Apellidos = "Perez",
                Identificacion = "1-1111-1111",
                Email = "juan@test.com",
                Direccion = "San Jose",
                FechaRegistro = DateTime.Now,
                Telefonos = new List<Entidades.Telefono>
                {
                    new Entidades.Telefono { Id = 1, ClienteId = 1, Numero = "8888-1111", Tipo = "Celular", FechaRegistro = DateTime.Now },
                    new Entidades.Telefono { Id = 2, ClienteId = 1, Numero = "2222-3333", Tipo = "Casa", FechaRegistro = DateTime.Now }
                }
            },
            new Entidades.Cliente
            {
                Id = 2,
                Nombre = "Maria",
                Apellidos = "Lopez",
                Identificacion = "2-2222-2222",
                Email = "maria@test.com",
                Direccion = "Heredia",
                FechaRegistro = DateTime.Now,
                Telefonos = new List<Entidades.Telefono>
                {
                    new Entidades.Telefono { Id = 3, ClienteId = 2, Numero = "8777-4444", Tipo = "Celular", FechaRegistro = DateTime.Now }
                }
            }
        };

        public List<Entidades.Cliente> ObtenerClientes()
        {
            
            return clientes.Select(c => new Entidades.Cliente
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellidos = c.Apellidos,
                Identificacion = c.Identificacion,
                Email = c.Email,
                Direccion = c.Direccion,
                FechaRegistro = c.FechaRegistro,
                Telefonos = c.Telefonos.Select(t => new Entidades.Telefono
                {
                    Id = t.Id,
                    ClienteId = t.ClienteId,
                    Numero = t.Numero,
                    Tipo = t.Tipo,
                    FechaRegistro = t.FechaRegistro
                }).ToList()
            }).ToList();
        }

        public Entidades.Cliente ObtenerClientePorId(int id)
        {
            var found = clientes.FirstOrDefault(c => c.Id == id);
            if (found == null) return null;

            return new Entidades.Cliente
            {
                Id = found.Id,
                Nombre = found.Nombre,
                Apellidos = found.Apellidos,
                Identificacion = found.Identificacion,
                Email = found.Email,
                Direccion = found.Direccion,
                FechaRegistro = found.FechaRegistro,
                Telefonos = found.Telefonos.Select(t => new Entidades.Telefono
                {
                    Id = t.Id,
                    ClienteId = t.ClienteId,
                    Numero = t.Numero,
                    Tipo = t.Tipo,
                    FechaRegistro = t.FechaRegistro
                }).ToList()
            };
        }

        public void AgregarCliente(Entidades.Cliente cliente)
        {
            var newId = clientes.Any() ? clientes.Max(c => c.Id) + 1 : 1;

            var nuevoCliente = new Entidades.Cliente
            {
                Id = newId,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Identificacion = cliente.Identificacion,
                Email = cliente.Email,
                Direccion = cliente.Direccion,
                FechaRegistro = DateTime.Now,
                Telefonos = new List<Entidades.Telefono>()
            };

            // Asignar IDs a teléfonos si vienen incluidos
            if (cliente.Telefonos != null && cliente.Telefonos.Any())
            {
                foreach (var tel in cliente.Telefonos)
                {
                    AgregarTelefonoInterno(nuevoCliente, tel);
                }
            }

            clientes.Add(nuevoCliente);
        }

        public void ActualizarCliente(Entidades.Cliente cliente)
        {
            var index = clientes.FindIndex(c => c.Id == cliente.Id);
            if (index < 0) return;

            // Actualiza datos básicos
            clientes[index].Nombre = cliente.Nombre;
            clientes[index].Apellidos = cliente.Apellidos;
            clientes[index].Identificacion = cliente.Identificacion;
            clientes[index].Email = cliente.Email;
            clientes[index].Direccion = cliente.Direccion;


            if (cliente.Telefonos != null)
            {
                clientes[index].Telefonos = cliente.Telefonos.Select(t => new Entidades.Telefono
                {
                    Id = t.Id == 0 ? GenerarTelefonoId() : t.Id,
                    ClienteId = cliente.Id,
                    Numero = t.Numero,
                    Tipo = t.Tipo,
                    FechaRegistro = t.FechaRegistro == default ? DateTime.Now : t.FechaRegistro
                }).ToList();
            }
        }

        public void EliminarCliente(int id)
        {
            // Elimina cliente (y por ende sus teléfonos porque van dentro del objeto)
            clientes.RemoveAll(c => c.Id == id);
        }

        public void AgregarTelefono(int clienteId, Entidades.Telefono telefono)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null) return;

            AgregarTelefonoInterno(cliente, telefono);
        }

        public void EliminarTelefono(int clienteId, int telefonoId)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente == null) return;

            cliente.Telefonos.RemoveAll(t => t.Id == telefonoId);
        }

        private void AgregarTelefonoInterno(Entidades.Cliente cliente, Entidades.Telefono telefono)
        {
            cliente.Telefonos.Add(new Entidades.Telefono
            {
                Id = GenerarTelefonoId(),
                ClienteId = cliente.Id,
                Numero = telefono.Numero,
                Tipo = telefono.Tipo,
                FechaRegistro = DateTime.Now
            });
        }

        private int GenerarTelefonoId()
        {
            var todos = clientes.SelectMany(c => c.Telefonos).ToList();
            return todos.Any() ? todos.Max(t => t.Id) + 1 : 1;
        }
    }
}

