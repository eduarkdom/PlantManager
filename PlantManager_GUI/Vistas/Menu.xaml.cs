using System;
using System.Windows;

using PlantManager_Negocio;
namespace PlantManager_GUI.Vistas
{
    public partial class Menu : Window
    {
        PlantManager_Negocio.PlantaCollection plantaCollection;
        PlantManager_Negocio.PlantaReportes plantaReportes;
        private string nombreUsuario;
        public Menu(string nombreUsuario)
        {
            InitializeComponent();
            this.nombreUsuario = nombreUsuario;
            plantaCollection = new PlantManager_Negocio.PlantaCollection();
            CargarGrilla();
            MensajeBienvenida();
            this.Title = "Menu";
        }

        private void MensajeBienvenida()
        {
            string formatName = FormatearNombreUsuario(this.nombreUsuario);
            txtBienvenida.Text = $"Bienvenido {formatName}";
        }

        private string FormatearNombreUsuario(string username)
        {
            return char.ToUpper(username[0]) + username.Substring(1).ToLower();
        }

        public static void CentrarVentanaHija(Window parentWindow, Window childWindow)
        {
            childWindow.Owner = parentWindow;
            childWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void AgregarPlanta_Click(object sender, RoutedEventArgs e)
        {

            Vistas.AgregarPlanta agregarPlanta = new Vistas.AgregarPlanta();
            CentrarVentanaHija(this, agregarPlanta);
            agregarPlanta.ShowDialog();
        }

        private void ListarPlantas_Click(object sender, RoutedEventArgs e)
        {

            Vistas.ListarPlantas listarPlantas = new Vistas.ListarPlantas();
            CentrarVentanaHija(this, listarPlantas);
            listarPlantas.ShowDialog();
        }

        private void Reportes_Click(object sender, RoutedEventArgs e)
        {
            plantaReportes = new PlantManager_Negocio.PlantaReportes();

            int herbaceas = plantaReportes.ObtenerCantidadPlantasPorTipo("Herbácea");
            int matorrales = plantaReportes.ObtenerCantidadPlantasPorTipo("Matorral");
            int arbustos = plantaReportes.ObtenerCantidadPlantasPorTipo("Arbusto");
            int arboles = plantaReportes.ObtenerCantidadPlantasPorTipo("Árbol");

            string message = string.Format(
                "Cantidad de Plantas Herbáceas   : {0} \n" +
                "Cantidad de Plantas de Matorral : {1} \n" +
                "Cantidad de Plantas de Arbusto  : {2} \n" +
                "Cantidad de Plantas de Árbol      : {3}",
                herbaceas, matorrales, arbustos, arboles
            );

            MessageBox.Show(message);
        }


        private void AcercaDe_Click(object sender, RoutedEventArgs e)
        {
            Vistas.AcercaDe acercaDe = new Vistas.AcercaDe();
            CentrarVentanaHija(this, acercaDe);
            acercaDe.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            dgListadoPlantas.ItemsSource = plantaCollection.ReadAll();
        }

    }
}
