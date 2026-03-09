using AutoMapper;
using PP2_SC_701_GrupoFBLL.Dtos;
using PP2_SC_701_GrupoFDAL.Entidades;
using PP2_SC_701_GrupoFDAL.Repositorios;

namespace PP2_SC_701_GrupoFBLL.Servicios
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IGenericRepository<Producto> _productoRepo;
        private readonly IMapper _mapper;

        public ProductoServicio(IGenericRepository<Producto> productoRepo, IMapper mapper)
        {
            _productoRepo = productoRepo;
            _mapper = mapper;
        }

        public async Task<List<ProductoDto>> ObtenerTodos()
        {
            var productos = await _productoRepo.ObtenerTodosAsync();
            return _mapper.Map<List<ProductoDto>>(productos);
        }

        public async Task<ProductoDto> ObtenerPorId(int id)
        {
            var producto = await _productoRepo.ObtenerPorIdAsync(id);
            return _mapper.Map<ProductoDto>(producto);
        }

        public async Task Crear(ProductoDto productoDto)
        {
            if (string.IsNullOrWhiteSpace(productoDto.Nombre))
                throw new Exception("El nombre del producto es obligatorio");

            if (productoDto.Precio <= 0)
                throw new Exception("El precio debe ser mayor que 0");

            if (productoDto.Stock < 0)
                throw new Exception("El stock no puede ser negativo");

            var producto = _mapper.Map<Producto>(productoDto);

            await _productoRepo.AgregarAsync(producto);
        }

        public async Task Actualizar(ProductoDto productoDto)
        {
            if (string.IsNullOrWhiteSpace(productoDto.Nombre))
                throw new Exception("El nombre del producto es obligatorio");

            if (productoDto.Precio <= 0)
                throw new Exception("El precio debe ser mayor que 0");

            if (productoDto.Stock < 0)
                throw new Exception("El stock no puede ser negativo");

            var producto = _mapper.Map<Producto>(productoDto);

            await _productoRepo.ActualizarAsync(producto);
        }

        public async Task Eliminar(int id)
        {
            await _productoRepo.EliminarAsync(id);
        }
    }
}