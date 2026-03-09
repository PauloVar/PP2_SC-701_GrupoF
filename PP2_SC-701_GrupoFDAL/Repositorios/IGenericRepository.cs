using System;
using System.Collections.Generic;
using System.Text;

namespace PP2_SC_701_GrupoFDAL.Repositorios
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> ObtenerTodosAsync();
        Task<T?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task EliminarAsync(int id);
    }
}
