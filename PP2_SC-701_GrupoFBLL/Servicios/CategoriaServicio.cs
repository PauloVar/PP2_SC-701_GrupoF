using AutoMapper;
using PP2_SC_701_GrupoFBLL.Dtos;
using PP2_SC_701_GrupoFDAL.Entidades;
using PP2_SC_701_GrupoFDAL.Repositorios;

namespace PP2_SC_701_GrupoFBLL.Servicios
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly IGenericRepository<Categoria> _categoriaRepo;
        private readonly IMapper _mapper;

        public CategoriaServicio(IGenericRepository<Categoria> categoriaRepo, IMapper mapper)
        {
            _categoriaRepo = categoriaRepo;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDto>> ObtenerTodos()
        {
            var categorias = await _categoriaRepo.ObtenerTodosAsync();
            return _mapper.Map<List<CategoriaDto>>(categorias);
        }

        public async Task<CategoriaDto> ObtenerPorId(int id)
        {
            var categoria = await _categoriaRepo.ObtenerPorIdAsync(id);
            return _mapper.Map<CategoriaDto>(categoria);
        }

        public async Task Crear(CategoriaDto categoriaDto)
        {
            if (string.IsNullOrWhiteSpace(categoriaDto.Nombre))
                throw new Exception("El nombre de la categoría es obligatorio");

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            await _categoriaRepo.AgregarAsync(categoria);
        }

        public async Task Actualizar(CategoriaDto categoriaDto)
        {
            if (string.IsNullOrWhiteSpace(categoriaDto.Nombre))
                throw new Exception("El nombre de la categoría es obligatorio");

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            await _categoriaRepo.ActualizarAsync(categoria);
        }

        public async Task Eliminar(int id)
        {
            await _categoriaRepo.EliminarAsync(id);
        }
    }
}