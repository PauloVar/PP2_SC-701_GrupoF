using Microsoft.AspNetCore.Mvc;
using PP1_SC_701_GrupoFBLL.Dtos;
using PP1_SC_701_GrupoFBLL.Servicios.Cliente;

namespace PP1_SC_701_GrupoF.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteServicio _clienteServicio;

        public ClienteController(IClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerClientes()
        {
            var response = await _clienteServicio.ObtenerClientesAsync();
            return Json(response);
        }

        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            var response = await _clienteServicio.ObtenerClientePorIdAsync(id);
            return Json(response);
        }

        public async Task<IActionResult> AgregarCliente(ClienteDto cliente)
        {
            var response = await _clienteServicio.AgregarClienteAsync(cliente);
            return Json(response);
        }

        public async Task<IActionResult> ActualizarCliente(ClienteDto cliente)
        {
            var response = await _clienteServicio.ActualizarClienteAsync(cliente);
            return Json(response);
        }

        public async Task<IActionResult> EliminarCliente(int id)
        {
            var response = await _clienteServicio.EliminarClienteAsync(id);
            return Json(response);
        }

        // Telefonos
        public async Task<IActionResult> AgregarTelefono(int clienteId, TelefonoDto telefono)
        {
            var response = await _clienteServicio.AgregarTelefonoAsync(clienteId, telefono);
            return Json(response);
        }

        public async Task<IActionResult> EliminarTelefono(int clienteId, int telefonoId)
        {
            var response = await _clienteServicio.EliminarTelefonoAsync(clienteId, telefonoId);
            return Json(response);
        }
    }
}

