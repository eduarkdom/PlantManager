using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PlantManager_Negocio;
using System.Diagnostics;



namespace PlantManager_GUI.Vistas
{
    public partial class ActualizarPlanta : Window
    {
        PlantManager_Negocio.Planta planta;
        private static Regex s_regex = new Regex("[^0-9]+");

        public ActualizarPlanta(int id)
        {
            InitializeComponent();
            this.Title = string.Format("Actualizar Planta {0}", id);
            this.Owner = Application.Current.MainWindow;
            planta = new PlantManager_Negocio.Planta();
            this.DataContext = planta;
            CargarDatosFormulario(id);
            



        }

        private void ActualizarPlanta_Click(object sender, RoutedEventArgs e)
        {
            planta.NombreComun = txtNombreComun.Text;
            planta.NombreCientifico = txtNombreCientifico.Text;
            planta.TipoPlanta = cmbTipoPlanta.Text;
            planta.Descripcion = txtDescripcion.Text;
            planta.TiempoRiego = Convert.ToInt32(txtTiempoRiego.Text);
            planta.CantidadAgua = Convert.ToInt32(txtCantidadAgua.Text);
            planta.Epoca = cmbEpoca.Text;
            planta.EsVenenosa = (chkEsVenenosa.IsChecked.Value);
            planta.EsAutoctona = (chkEsAutoctona.IsChecked.Value);

            bool response = planta.Update();

            if (response)
            {
                MessageBox.Show(string.Format("Planta {0} actualizada exitósamente!", planta.NombreComun));
                this.Close();
            }
            else
            {
                MessageBox.Show(string.Format("No se ha actualizado la planta {0}", planta.NombreComun));
                this.Close();
            }
        }

        private void CargarDatosFormulario(int id)
        {

            bool response = planta.Read(id);

            if (response)
            {
                
                txtNombreComun.Text = planta.NombreComun;
                txtNombreCientifico.Text = planta.NombreCientifico;
                cmbTipoPlanta.Text = planta.TipoPlanta;
                txtDescripcion.Text = planta.Descripcion;
                txtTiempoRiego.Text = planta.TiempoRiego.ToString();
                txtCantidadAgua.Text = planta.CantidadAgua.ToString();
                cmbEpoca.Text = planta.Epoca;
                chkEsVenenosa.IsChecked = (planta.EsVenenosa);
                chkEsAutoctona.IsChecked = (planta.EsAutoctona);
            }
            else
            {
                MessageBox.Show(string.Format("La Planta con ID {0} no fue encontrada.", id));
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
