using Microsoft.AspNetCore.Mvc;
using PP2_SC_701_GrupoFBLL.Dtos;
using PP2_SC_701_GrupoFBLL.Servicios;

namespace PP2_SC_701_GrupoF.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public CategoriaController(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerCategorias()
        {
            var categorias = await _categoriaServicio.ObtenerTodos();
            return Json(new { data = categorias });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaDto categoria)
        {
            if (!ModelState.IsValid)
                return View(categoria);

            await _categoriaServicio.Crear(categoria);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _categoriaServicio.ObtenerPorId(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaServicio.ObtenerPorId(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoriaDto categoria)
        {
            if (!ModelState.IsValid)
                return View(categoria);

            await _categoriaServicio.Actualizar(categoria);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaServicio.ObtenerPorId(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriaServicio.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}