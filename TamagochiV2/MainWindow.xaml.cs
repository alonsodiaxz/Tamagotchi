using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TamagochiV2
{

    public partial class MainWindow : Window
    {
        String nombre;
        // BBDD Access
        OleDbConnection myconect;
        OleDbCommand mycommand;

        const int mveces = 3;
        int nveces = 0;
        int con = 0;
        int record = 0;
        int puntuacion = 0;
        int puntuacionfinal = 0;
        int nivel = 0;
        int campeon = 0;
        DispatcherTimer timer;
        String r = "";

        public MainWindow()
        {
            InitializeComponent();
            VentanaBienvenida pantallaInicio = new VentanaBienvenida(this);
            pantallaInicio.ShowDialog();
            OleDbConnection conx = conectarBBDD();
            leerDatos(conx);
            temporizador();
            this.TBPuntuacionJuego.Text = "Puntuacion final: " + PrbarDiversion.Value + " pts.";
            this.TBRecord.Text = "Record: " + record + " pts.";
            this.TBNivel.Text = "Nivel: " + 0;
        }

        private void leerDatos(OleDbConnection myconect)
        {
            List<Ranking> ranking;
            mycommand = myconect.CreateCommand();
            mycommand.CommandText = "SELECT * FROM Ranking ";
            mycommand.CommandType = CommandType.Text;
            OleDbDataReader DBreader = mycommand.ExecuteReader();
            ranking = new List<Ranking>();

            while (DBreader.Read())
            {
                int id = Convert.ToInt32(DBreader["Id"].ToString());
                String nombre = DBreader["Name"].ToString();
                int energia = Convert.ToInt32(DBreader["Energia"].ToString());
                int comida = Convert.ToInt32(DBreader["Comida"].ToString());
                int diversion = Convert.ToInt32(DBreader["Diversión"].ToString());
                ranking.Add(new Ranking() { Id = id, Name = nombre, Energia = energia, Comida = comida, Diversión = diversion });
            }
            this.DTGVRanking.ItemsSource = ranking;
        }

        private OleDbConnection conectarBBDD()
        {
            r = Directory.GetCurrentDirectory();
            myconect = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = "+r+"/Ranking.accdb");
            myconect.Open();
            return myconect;
        }

        private void insertarDatos(OleDbConnection c)
        {
            mycommand = c.CreateCommand();
            if (this.nombre == null)
            {
                this.nombre = "user";
            }
            mycommand.CommandText = "INSERT INTO Ranking (Name, Energia, Comida, Diversión) VALUES ('" + this.nombre.ToUpper() + "'," + this.PrBarEnergia.Value + "," + this.PrbarAlimento.Value + "," + this.PrbarDiversion.Value + ")";
            mycommand.CommandType = CommandType.Text;

            OleDbDataReader DBreader = mycommand.ExecuteReader();
        }

        private void BtnDescansar_Click(object sender, RoutedEventArgs e)
        {

            PrBarEnergia.Value += 5;
            dormir();


        }

        private void dormir()
        {
            r = Directory.GetCurrentDirectory();
            String ruta = r + "/Sonidos/dormir.wav";
            reproducirSonidos(ruta);
            if (ParpadoIzq.Height == 16.016 || ParpadoDer.Height == 16.016)
            {
                BtnDescansar.IsEnabled = false;
                DoubleAnimation cerrarOjos = new DoubleAnimation();
                cerrarOjos.From = ParpadoDer.Height;
                cerrarOjos.To = ParpadoDer.Height + 10;
                cerrarOjos.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                cerrarOjos.Completed += new EventHandler(finCerrarOjoDer);

                ParpadoDer.BeginAnimation(Ellipse.HeightProperty, cerrarOjos);
                ParpadoIzq.BeginAnimation(Ellipse.HeightProperty, cerrarOjos);
                
            }

        }

        private void finJugar(object sender, EventArgs e)
        {
            btnJugar.IsEnabled = true;
           
        }

        private void finComer(object sender, EventArgs e)
        {
            BtnComer.IsEnabled = true;
        }

        private void finCerrarOjoDer(object sender, EventArgs e)
        {
            BtnDescansar.IsEnabled = true;
        }

        private void cambiarFondo(object sender, MouseButtonEventArgs e)
        {
            this.imaFondo.Source = ((Image)sender).Source;
        }

        private void BtnComer_Click(object sender, RoutedEventArgs e)
        {
            r = Directory.GetCurrentDirectory();
            String ruta = r + "/Sonidos/comer.wav";
            BtnComer.IsEnabled = false;
            PrbarAlimento.Value += 2;
            Storyboard Comer = (Storyboard)this.Resources["Comer"];
            Comer.Completed += new EventHandler(finComer);
            Comer.Begin();
            reproducirSonidos(ruta);
        }

        private int gradoAletoriedad(int nivel)
        {
            var random = new Random();
            int value = 0;
            if (nivel == 0)
            {
                value = random.Next(35, 45);
            }
            else if (nivel == 1)
            {
                value = random.Next(30, 45);
            }
            else if (nivel == 2)
            {
                value = random.Next(25, 45);

            }
            else if (nivel == 3)
            {
               value = random.Next(22, 42);
                
            }
            else if (nivel == 4)
            {
                value = random.Next(20, 40);
                
            }

            return value;

        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Stop();
            int value = gradoAletoriedad(nivel);
            btnJugar.IsEnabled = false;
            animacionDisparo();
            animacionSaltar();
            abrirParpado();

            if (nveces < mveces)
            {
                puntuacion = Convert.ToInt32(PrbarDiversion.Value += value);
                this.TBPuntuacion.Text = "Puntuacion: " + value + "pts.";
                nveces++;
                con++;

                if (con == 3)
                {
                    puntuacionfinal = Convert.ToInt32(PrbarDiversion.Value);
                    this.TBPuntuacionJuego.Text = "Puntuacion final: " + PrbarDiversion.Value + " pts.";
                    con = 0;
                    nuevoRecord(puntuacionfinal);
                    actualizarDatos();
                    if (nivel == 4 && puntuacionfinal == 100)
                    {
                        r = Directory.GetCurrentDirectory();
                        String ruta = r + "/Sonidos/campeon.wav";
                        reproducirSonidos(ruta);
                        eventoGanador();
                        this.imCampeon.Visibility = Visibility.Visible;
                        nivel = 0;
                        campeon++;
                    }
                }
            }
            else
            {
                if (nivel == 0 && campeon > 0)
                {
                    this.ImRecordPremio.Visibility = Visibility.Hidden;
                    this.imCampeon.Visibility = Visibility.Hidden;
                    this.ImPremioNivel.Visibility = Visibility.Hidden;
                }
                pasarNivel(puntuacionfinal);
                puntuacionfinal = 0;
                PrbarDiversion.Value = 0;
                this.TBPuntuacionJuego.Text = "Puntuacion final: " + 0 + " pts.";
                this.TBPuntuacion.Text = "Puntuacion: " + 0 + "pts.";
                nveces = 0;
                this.TBRecord.Text = "Record: " + record;
            }   
        }

        private void FondoDesierto(object sender, MouseButtonEventArgs e)
        {
            this.imaFondo.Source = ((Image)sender).Source;
        }

        private void FondoRocoso(object sender, MouseButtonEventArgs e)
        {
            this.imaFondo.Source = ((Image)sender).Source;
        }

        private void acercaDE(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult resultado = System.Windows.MessageBox.Show("Programa realizado por \n\nAlonso Diaz \n\n¿Desea salir?", "IPO2 Tamagotchi", MessageBoxButton.YesNo);
            switch (resultado)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
            }
        }

        public void setNombre(String nombre)
        {
            if (String.IsNullOrEmpty(nombre))
            {
                this.nombre = "user";
            }
            else
            {
                this.nombre = nombre;
            }
            String frase = "¡Bienvenid@ " + this.nombre + "!";
            String may = frase.ToUpper();
            this.tbBienvenido.Text = may;

        }

        private void FondoHogwart(object sender, MouseButtonEventArgs e)
        {
            this.imaFondo.Source = ((Image)sender).Source;
        }

        private void FondoPirata(object sender, MouseButtonEventArgs e)
        {
            this.imaFondo.Source = ((Image)sender).Source;
        }

        private void FondoPlaya(object sender, MouseButtonEventArgs e)
        {
            this.imaFondo.Source = ((Image)sender).Source;
        }

        private void añadirObjeto(object sender, System.Windows.DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));
            switch (aux.Name)
            {
                case "pngLoro":
                    pngLoro_Copy.Visibility = Visibility.Visible;
                    break;

                case "pngEspada":
                    pngEspada_Copy.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void inicioArrastrar(object sender, MouseButtonEventArgs e)
        {
            System.Windows.DataObject dataO = new System.Windows.DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, dataO, System.Windows.DragDropEffects.Move);
        }

        private void asignarNivel(int nivel)
        {
            if (nivel == 1)
            {
                this.imaFondo.Source = new BitmapImage(new Uri(@"calavera.jpg", UriKind.Relative));
                
            }
            else if (nivel == 2)
            {
                this.imaFondo.Source = new BitmapImage(new Uri(@"barco.jpg", UriKind.Relative));
               
            }
            else if (nivel == 3)
            {
                this.imaFondo.Source = new BitmapImage(new Uri(@"cielo.jpg", UriKind.Relative));
                
            }
            else if (nivel == 4)
            {
                this.imaFondo.Source = new BitmapImage(new Uri(@"mar.jpg", UriKind.Relative));
            }
            
        }

        private void pasarNivel(int puntuacionfinal)
        {
            if (puntuacionfinal == 100)
            {
                
                if (nivel == 0 && campeon > 0)
                {
                    nivel++;
                    record = 0;
                    this.TBNivel.Text = "Nivel: " + nivel;
                    asignarNivel(nivel);
                }
                else
                {
                    r = Directory.GetCurrentDirectory();
                    String ruta = r + "/Sonidos/nivel.wav";
                    reproducirSonidos(ruta);
                    nivel++;
                    record = 0;
                    this.TBNivel.Text = "Nivel: " + nivel;
                    asignarNivel(nivel);
                    eventoLogroNivel();
                }
                
            }
        }

        private void nuevoRecord(int puntuacionfinal)
        {
            if (puntuacionfinal > record)
            {
                r = Directory.GetCurrentDirectory();
                String ruta = r + "/Sonidos/logro.wav";
                reproducirSonidos(ruta);
                record = puntuacionfinal;
                this.TBRecord.Text = "Record: " + record + " pts.";
                eventoLogroRecord();
                this.ImRecordPremio.Visibility = Visibility.Visible;
            }
        }

        private void abrirParpado()
        {
            if (ParpadoIzq.Height == 26.016 || ParpadoDer.Height == 26.016)
            {
                DoubleAnimation abrirOjos = new DoubleAnimation();
                abrirOjos.From = ParpadoDer.Height;
                abrirOjos.To = ParpadoDer.Height - 10;
                abrirOjos.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                abrirOjos.Completed += new EventHandler(finJugar);

                ParpadoDer.BeginAnimation(Ellipse.HeightProperty, abrirOjos);
                ParpadoIzq.BeginAnimation(Ellipse.HeightProperty, abrirOjos);
            }
        }

        private void animacionDisparo()
        {
            Storyboard disparo = (Storyboard)this.Resources["Disparo"];
            disparo.Completed += new EventHandler(finJugar);
            disparo.Begin();
            disparo.AutoReverse = true;
        }

        private void animacionSaltar()
        {
            Storyboard saltar = (Storyboard)this.Resources["Saltar"];
            saltar.Completed += new EventHandler(finJugar);
            saltar.Begin();
            saltar.AutoReverse = true;
        }

        private void eventoLogroNivel()
        {
            Storyboard sbNivel = (Storyboard)this.Resources["RayoLogro"];
            sbNivel.Begin();
            this.ImPremioNivel.Visibility = Visibility.Visible;
        }

        private void eventoLogroRecord()
        {
            Storyboard sbRecord = (Storyboard)this.Resources["NuevoRecordLogo"];
            sbRecord.Begin();
        }

        private void eventoGaviota()
        {
            Storyboard sbGaviota = (Storyboard)this.Resources["gaviota"];
            sbGaviota.Begin();
        }

        private void eventoGanador()
        {
            Storyboard sbGanador = (Storyboard)this.Resources["campeon"];
            sbGanador.Begin();
        }

        private void actualizarDatos()
        {
            OleDbConnection c = conectarBBDD();
            insertarDatos(c);
            leerDatos(c);

        }

        private void temporizador()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += (a, b) =>
            {
                this.PrBarEnergia.Value -= 8;
                this.PrbarAlimento.Value -= 4;
                morir();
            };
            timer.Start();

        }

        private void morir()
        {
            if (PrbarAlimento.Value == 0 || PrBarEnergia.Value == 0)
            {
                DispatcherTimer timer = new DispatcherTimer();
                dormir();
                eventoGaviota();
                r = Directory.GetCurrentDirectory();
                String ruta = r + "/Sonidos/gameover.wav";
                reproducirSonidos(ruta);
                timer.Interval = new TimeSpan(0, 0, 7);
                timer.Tick += (a, b) =>
                {
                    this.Close();
                };
                timer.Start();
                
            }
        }

        private void reproducirSonidos(String ruta)
        {

            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = ruta;
            Player.Play();
         
        }
    }

    public class Ranking
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Energia { get; set; }
        public int Comida { get; set; }
        public int Diversión { get; set; }
    }
}
