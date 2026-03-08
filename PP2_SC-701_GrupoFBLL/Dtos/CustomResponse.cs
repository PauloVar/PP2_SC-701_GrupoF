using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP1_SC_701_GrupoFBLL.Dtos
{
    public class CustomResponse<T>
    {
        public bool esCorrecto { get; set; }
        public string mensaje { get; set; }
        public T Data { get; set; }
        public int codigoStatus { get; set; }

        public CustomResponse()
        {
            esCorrecto = true;
            mensaje = "Operación realizada correctamente.";
            codigoStatus = 200;
        }
    }
}
