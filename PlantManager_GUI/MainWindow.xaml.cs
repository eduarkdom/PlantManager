using PlantManager_Negocio;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PlantManager_GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string enteredPassword = txtPassword.Password;

            string storedPassword = ObtenerContraseñaDesdeBD(username);

            if (storedPassword != null && SALT_Helper.ValidateSaltyPassword(enteredPassword, storedPassword))
            {
                Vistas.Menu menu = new Vistas.Menu(username);
                Close();
                menu.Show();
            }
            else
            {
                lblMessage.Content = "Nombre de usuario o contraseña incorrectos";
            }
        }

        private string ObtenerContraseñaDesdeBD(string username)
        {
            using (var context = new PlantManager_DALC.El_SaltoEntities())
            {
                var result = context.spLogin(username).FirstOrDefault();

                if (result != null)
                {
                    return result;
                }

                return null;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }
    }
}
