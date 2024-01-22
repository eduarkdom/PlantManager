using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlantManager_Negocio
{
    public class PlantaCollection
    {
        public List<Planta> ReadAll()
        {
            try
            {
                var plantasFromDB = CommonBC.PlantaModelo.vwGetPlantas.ToList();

                return plantasFromDB.Select(plantaDB => new Planta
                {
                    PlantaId = plantaDB.Id,
                    NombreComun = AES_Helper.DecryptString(plantaDB.NombreComun),
                    NombreCientifico = AES_Helper.DecryptString(plantaDB.NombreCientifico),
                    TipoPlanta = plantaDB.TipoPlanta,
                    Descripcion = AES_Helper.DecryptString(plantaDB.Descripcion),
                    TiempoRiego = plantaDB.TiempoRiego,
                    CantidadAgua = plantaDB.CantidadAgua,
                    Epoca = plantaDB.Epoca,
                    EsVenenosa = (bool)plantaDB.EsVenenosa,
                    EsAutoctona = (bool)plantaDB.EsAutoctona
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return new List<Planta>();
            }
        }
    }
}