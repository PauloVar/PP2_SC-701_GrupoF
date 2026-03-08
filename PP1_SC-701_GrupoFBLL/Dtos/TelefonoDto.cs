using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP1_SC_701_GrupoFBLL.Dtos
{
    public class TelefonoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
    }
}
