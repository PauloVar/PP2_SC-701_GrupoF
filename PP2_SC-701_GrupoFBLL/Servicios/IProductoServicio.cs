using PP2_SC_701_GrupoFBLL.Dtos;

namespace PP2_SC_701_GrupoFBLL.Servicios
{
    public interface IProductoServicio
    {
        Task<List<ProductoDto>> ObtenerTodos();

        Task<ProductoDto> ObtenerPorId(int id);

        Task Crear(ProductoDto producto);

        Task Actualizar(ProductoDto producto);

        Task Eliminar(int id);
    }
}