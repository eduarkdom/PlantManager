using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PlantManager_Negocio;

namespace PlantManager_GUI.Vistas
{
    public partial class ListarPlantas : Window
    {
        PlantManager_Negocio.PlantaCollection plantaCollection;

        public ListarPlantas()
        {
            InitializeComponent();
            CargarGrilla();
            this.Owner = Application.Current.MainWindow;
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            var filaSeleccionada = (PlantManager_Negocio.Planta)dgListadoPlantas.SelectedItem;
            ActualizarPlanta actualizarPlanta = new ActualizarPlanta(filaSeleccionada.PlantaId);
            CentrarVentanaHija(this, actualizarPlanta);
            actualizarPlanta.ShowDialog();
        
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var filaSeleccionada = (PlantManager_Negocio.Planta)dgListadoPlantas.SelectedItem;
            string nombrePlanta = filaSeleccionada.NombreComun;
            int id = filaSeleccionada.PlantaId;
            string title = "Eliminar Planta";
            string message = $"¿Estás seguro de eliminar la Planta {nombrePlanta}?";

            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);

            if (result == MessageBoxResult.Yes)
            {
                _ = filaSeleccionada.Delete(id) ?
                MessageBox.Show($"Planta {nombrePlanta} eliminada") :
                MessageBox.Show($"La Planta {nombrePlanta} no ha sido eliminada");
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            plantaCollection = new PlantManager_Negocio.PlantaCollection();
            dgListadoPlantas.ItemsSource = plantaCollection.ReadAll();
        }

        public static void CentrarVentanaHija(Window parentWindow, Window childWindow)
        {
            childWindow.Owner = parentWindow;
            childWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
    }
}
