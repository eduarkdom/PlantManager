using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlantManager_Negocio
{
    public class Planta : ObservableObject, IPersistente
    {
        public int PlantaId { get; set; }

        private string _nombreComun;
        private string _nombreCientifico;
        private string _tipoPlanta;
        private string _descripcion;
        private int? _tiempoRiego;
        private int? _cantidadAgua;
        private string _epoca;
        private bool _esVenenosa;
        private bool _esAutoctona;
        

        [Required(ErrorMessage = "El campo es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El Nombre Común de la Planta debe tener entre 3 y 100 caracteres")]
        public string NombreComun
        {
            get { return _nombreComun; }
            set
            {
                ValidateProperty(value);
                OnPropertyChanged(ref _nombreComun, value);
            }
        }

        [Required(ErrorMessage = "El campo es requerido")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "El Nombre Científico de la Planta debe tener entre 10 y 150 caracteres")]
        public string NombreCientifico
        {
            get { return _nombreCientifico; }
            set
            {
                ValidateProperty(value);
                OnPropertyChanged(ref _nombreCientifico, value);
            }
        }

        [Required(ErrorMessage = "El campo es requerido")]
        [IsValidComboBox(ErrorMessage = "El Tipo de Planta debe ser 'Herbácea', 'Matorral', 'Arbusto' o 'Árbol'")]
        public string TipoPlanta
        {
            get { return _tipoPlanta; }
            set
            {
                ValidateProperty(value);
                OnPropertyChanged(ref _tipoPlanta, value);
            }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                
                OnPropertyChanged(ref _descripcion, value);
            }
        }

        [Required(ErrorMessage = "El campo es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El Tiempo de Riego debe ser un valor positivo")]
        public int? TiempoRiego
        {
            get { return _tiempoRiego; }
            set
            {
                ValidateProperty(value);
                OnPropertyChanged(ref _tiempoRiego, value);
            }
        }

        [Required(ErrorMessage = "El campo es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "La Cantidad de Agua debe ser un valor positivo")]
        public int? CantidadAgua
        {
            get { return _cantidadAgua; }
            set
            {
                ValidateProperty(value);
                OnPropertyChanged(ref _cantidadAgua, value);
            }
        }

        [Required(ErrorMessage = "El campo es requerido")]
        [IsValidComboBox(ErrorMessage = "La Época debe ser 'Verano', 'Invierno', 'Otoño' o 'Primavera'")]
        public string Epoca
        {
            get { return _epoca; }
            set
            {
                ValidateProperty(value);
                OnPropertyChanged(ref _epoca, value);
            }
        }

        public bool EsVenenosa
        {
            get { return _esVenenosa; }
            set
            {
                OnPropertyChanged(ref _esVenenosa, value);
            }
        }

        public bool EsAutoctona
        {
            get { return _esAutoctona; }
            set
            {
                OnPropertyChanged(ref _esAutoctona, value);
            }
        }

        private void ValidateProperty<T>(T value, [CallerMemberName] string name = "")
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }

        public bool Create()
        {
            try
            {
                string descripcionValidada = string.IsNullOrEmpty(this.Descripcion) ? "Sin información" : this.Descripcion;

                CommonBC.PlantaModelo.spPlantaSave(
                    AES_Helper.EncryptString(this.NombreComun),
                    AES_Helper.EncryptString(this.NombreCientifico),
                    this.TipoPlanta,
                    AES_Helper.EncryptString(descripcionValidada),
                    this.TiempoRiego,
                    this.CantidadAgua,
                    this.Epoca,
                    this.EsVenenosa,
                    this.EsAutoctona
                );

                CommonBC.PlantaModelo.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        public bool Read(int id)
        {
            try
            {
                PlantManager_DALC.vwGetPlantas planta = CommonBC.PlantaModelo.vwGetPlantas.First(p => p.Id == id);

                this.PlantaId = planta.Id;
                this.NombreComun = AES_Helper.DecryptString(planta.NombreComun);
                this.NombreCientifico = AES_Helper.DecryptString(planta.NombreCientifico);
                this.TipoPlanta = planta.TipoPlanta;
                this.Descripcion = AES_Helper.DecryptString(planta.Descripcion);
                this.TiempoRiego = planta.TiempoRiego;
                this.CantidadAgua = planta.CantidadAgua;
                this.Epoca = planta.Epoca;
                this.EsVenenosa = (bool)planta.EsVenenosa ? true : false ;
                this.EsAutoctona = (bool)planta.EsVenenosa ? true : false;


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        public bool Update()
        {
            try
            {

                string descripcionValidada = string.IsNullOrEmpty(this.Descripcion) ? "Sin información" : this.Descripcion;

                CommonBC.PlantaModelo.spPlantaUpdateById(
                    this.PlantaId,
                    AES_Helper.EncryptString(this.NombreComun),
                    AES_Helper.EncryptString(this.NombreCientifico),
                    this.TipoPlanta,
                    AES_Helper.EncryptString(descripcionValidada),
                    this.TiempoRiego,
                    this.CantidadAgua,
                    this.Epoca,
                    this.EsVenenosa,
                    this.EsAutoctona
                );

                CommonBC.PlantaModelo.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al actualizar la Planta: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }


        public bool Delete(int id)
        {
            try
            {
                CommonBC.PlantaModelo.spPlantaDeleteById(id);
                CommonBC.PlantaModelo.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

    }
}
