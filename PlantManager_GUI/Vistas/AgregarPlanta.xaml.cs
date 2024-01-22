using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PlantManager_Negocio;

namespace PlantManager_GUI.Vistas
{
    public partial class AgregarPlanta : Window
    {
        private static Regex s_regex = new Regex("[^0-9]+");

        PlantManager_Negocio.Planta planta;

        public AgregarPlanta()
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            planta = new PlantManager_Negocio.Planta();
            this.DataContext = planta;
        }

        private void AgregarPlanta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PlantaYaExiste())
                {
                    MessageBox.Show("Ya existe una planta con el mismo Nombre Común. Intente nuevamente con otro nombre.");
                    return;
                }

                bool response = planta.Create();

                if (response)
                {
                    MessageBox.Show("Planta fue agregada correctamente");
                    AgregarOtraPlanta();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error. Intente nuevamente");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AgregarOtraPlanta()
        {
            string title = "Agregar nueva Planta";
            string message = "¿Desea agregar otra Planta?";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);

            if (result == MessageBoxResult.No)
            {
                this.Close();
            }
        }

        private void LimpiarCampos()
        {
            txtNombreComun.Text = string.Empty;
            txtNombreCientifico.Text = string.Empty;
            cmbTipoPlanta.SelectedIndex = 0;
            txtDescripcion.Text = string.Empty;
            txtTiempoRiego.Text = null;
            txtCantidadAgua.Text = null;
            cmbEpoca.SelectedIndex = 0;
            chkEsVenenosa.IsChecked = false;
            chkEsAutoctona.IsChecked = false;
        }

        private bool PlantaYaExiste()
        {
            try
            {                
                string nombreComunDesencriptado = AES_Helper.EncryptString(planta.NombreComun);

                var plantaExistente = CommonBC.PlantaModelo.vwGetPlantas
                    .FirstOrDefault(p => p.NombreComun == nombreComunDesencriptado);

                return plantaExistente != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar si la planta ya existe: {ex.Message}");
                return false;
            }
        }


        private void CheckTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = s_regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = s_regex.IsMatch(e.Text);
        }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
