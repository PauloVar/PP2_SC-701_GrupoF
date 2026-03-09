using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PP2_SC_701_GrupoFBLL.Dtos;
using PP2_SC_701_GrupoFBLL.Servicios;

namespace PP2_SC_701_GrupoF.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoServicio _productoServicio;
        private readonly ICategoriaServicio _categoriaServicio;

        public ProductoController(IProductoServicio productoServicio, ICategoriaServicio categoriaServicio)
        {
            _productoServicio = productoServicio;
            _categoriaServicio = categoriaServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _productoServicio.ObtenerTodos();
            var categorias = await _categoriaServicio.ObtenerTodos();

            var data = productos.Select(p => new
            {
                id = p.Id,
                nombre = p.Nombre,
                descripcion = p.Descripcion,
                precio = p.Precio,
                stock = p.Stock,
                categoriaId = p.CategoriaId,
                categoriaNombre = categorias.FirstOrDefault(c => c.Id == p.CategoriaId)?.Nombre ?? ""
            }).ToList();

            return Json(new { data });
        }

        public async Task<IActionResult> Create()
        {
            await CargarCategorias();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                await CargarCategorias(producto.CategoriaId);
                return View(producto);
            }

            await _productoServicio.Crear(producto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var producto = await _productoServicio.ObtenerPorId(id);
            if (producto == null)
                return NotFound();

            var categorias = await _categoriaServicio.ObtenerTodos();
            ViewBag.NombreCategoria = categorias.FirstOrDefault(x => x.Id == producto.CategoriaId)?.Nombre ?? "";

            return View(producto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _productoServicio.ObtenerPorId(id);
            if (producto == null)
                return NotFound();

            await CargarCategorias(producto.CategoriaId);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                await CargarCategorias(producto.CategoriaId);
                return View(producto);
            }

            await _productoServicio.Actualizar(producto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _productoServicio.ObtenerPorId(id);
            if (producto == null)
                return NotFound();

            var categorias = await _categoriaServicio.ObtenerTodos();
            ViewBag.NombreCategoria = categorias.FirstOrDefault(x => x.Id == producto.CategoriaId)?.Nombre ?? "";

            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productoServicio.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarCategorias(int categoriaSeleccionada = 0)
        {
            var categorias = await _categoriaServicio.ObtenerTodos();

            ViewBag.Categorias = categorias.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre,
                Selected = c.Id == categoriaSeleccionada
            }).ToList();
        }
    }
}