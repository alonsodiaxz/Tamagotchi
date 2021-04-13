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

namespace TamagochiV2
{
    /// <summary>
    /// Lógica de interacción para VentanaBienvenida.xaml
    /// </summary>
    public partial class VentanaBienvenida : Window
    {
        MainWindow padre;
        public VentanaBienvenida(MainWindow padre)
        {
            InitializeComponent();
            this.padre = padre;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            padre.Show();
            enviarNombre();
            this.Close();
        }

        private void enviarNombre()
        {
            padre.setNombre(this.tbnombre.Text);
            //padre.tbBienvenido.Text = "Bienvenido " + tbnombre.Text;
            
        }
    }
}
