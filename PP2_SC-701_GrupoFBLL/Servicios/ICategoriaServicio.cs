using PP2_SC_701_GrupoFBLL.Dtos;

namespace PP2_SC_701_GrupoFBLL.Servicios
{
    public interface ICategoriaServicio
    {
        Task<List<CategoriaDto>> ObtenerTodos();

        Task<CategoriaDto> ObtenerPorId(int id);

        Task Crear(CategoriaDto categoria);

        Task Actualizar(CategoriaDto categoria);

        Task Eliminar(int id);
    }
}