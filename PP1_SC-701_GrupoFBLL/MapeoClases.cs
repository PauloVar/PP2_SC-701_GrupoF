using AutoMapper;
using PP1_SC_701_GrupoFBLL.Dtos;
using PP1_SC_701_GrupoFDAL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace PP1_SC_701_GrupoFBLL
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Telefono, TelefonoDto>().ReverseMap();
        }
    }
}