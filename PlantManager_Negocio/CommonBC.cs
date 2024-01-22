using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantManager_Negocio
{
    public class CommonBC
    {
        private static PlantManager_DALC.El_SaltoEntities _plantaModelo;

        public static PlantManager_DALC.El_SaltoEntities PlantaModelo
        {
            get
            {
                if (_plantaModelo == null)
                {
                    _plantaModelo = new PlantManager_DALC.El_SaltoEntities();
                }
                return _plantaModelo;
            }
        }

        public CommonBC() { }
    }
}