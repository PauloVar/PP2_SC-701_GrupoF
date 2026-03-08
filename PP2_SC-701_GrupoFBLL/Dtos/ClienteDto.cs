using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP1_SC_701_GrupoFBLL.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Identificacion { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public List<TelefonoDto> Telefonos { get; set; } = new();
    }
}