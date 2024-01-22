using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantManager_Negocio;

namespace PlantManager_Negocio
{
    public class PlantaReportes
    {
        public int ObtenerCantidadPlantasPorTipo(string tipoPlanta)
        {
            return Convert.ToInt32(CommonBC.PlantaModelo.spObtenerCantidadPlantasPorTipo(tipoPlanta).First().Value);
        }
    }
}
