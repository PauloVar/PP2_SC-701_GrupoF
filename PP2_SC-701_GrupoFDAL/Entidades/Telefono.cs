using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP2_SC_701_GrupoFDAL.Entidades
{
    public class Telefono
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }   // FK lógica (en memoria)
        public string Numero { get; set; }
        public string Tipo { get; set; }     // Ej: "Celular", "Casa", etc.

        public DateTime FechaRegistro { get; set; } // Campo tipo BD
    }
}
