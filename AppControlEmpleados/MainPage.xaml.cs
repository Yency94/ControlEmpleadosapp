using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Net;
using Xamarin.Essentials;
using System.IO;

namespace AppControlEmpleados
{
    public partial class MainPage : ContentPage
    {
        string RutaServicios = "http://161.97.77.167:8030/servicioconsultas?verif=SyscomeAuth$280257&q=";
        public MainPage()
        {
            InitializeComponent();
            txtFecha.Date = DateTime.Today;
            txtFecha.DateSelected += TxtFecha_DateSelected;
            NavigationPage.SetHasNavigationBar(this, false);

            //   btnConf.Source = ImageSource.FromFile("Imagenes/settings.png");
            btnConf.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.settings.png");
            add.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.add.png");
            btnSincronizar.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.cloud.png");
            btnRefrech.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.refresh.png");
            btnSALIR.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.logout.png");
        }

        private void TxtFecha_DateSelected(object sender, DateChangedEventArgs e)
        {
            WebClient _web = new WebClient();
            string Rutaagenda = RutaServicios + "obteneragenda&fecha=" + txtFecha.Date.ToString("yyyy-MM-dd") + "&Empleado=2";
            String _info = _web.DownloadString(Rutaagenda);
            String[] _Agenda = _info.Split(';');
            List<Agenda> _Agenda2 = new List<Agenda>();
            foreach (String _l in _Agenda)
            {
                if (_l.Length > 0)
                {


                    String[] _it = _l.Split('|');
                    Agenda _Ag = new Agenda();
                    _Ag.Codcliente = _it[0].ToString();
                    _Ag.Nombre = _it[1].ToString();
                    _Ag.Fecha =Convert.ToDateTime( _it[2]);
                    _Ag.detalle = _it[3].ToString();
                    _Ag.Zona = Convert.ToInt32(_it[4]);
                    _Ag.idAgenda = Convert.ToInt32(_it[5]);
                    _Agenda2.Add(_Ag);

                }
            }
            detalleProductos.Children.Clear();
            foreach (var item in _Agenda2)
            {
              

                Label la = new Label
                    {
                        Text =item.idAgenda+" Nombre Cliente: " +item.Nombre +" Detalle: "+item.detalle ,
                        FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label)),
                        VerticalOptions = LayoutOptions.Center
                    };
                    ImageButton imbu = new ImageButton
                    {
                        BackgroundColor = Color.BlueViolet,
                        Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.check-mark.png"),
                        Padding = 10,
                        WidthRequest = 60,
                        HeightRequest = 40,


                    };
                

               
               
                imbu.Clicked += async delegate
                {
                    TappedEventArgs _e1 = new TappedEventArgs(item.idAgenda);
                    await Imbu_Clicked(null, _e1);
                   // imbu.Clicked += (null, _e1);

                };
                    StackLayout stacla = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.Fill,
                        Margin = new Thickness(5),
                        Children =
                    {
                 new Frame
            {
                CornerRadius=8,
                HorizontalOptions=LayoutOptions.Fill,
                BorderColor = Color.Gray,
                Padding = new Thickness(5),

                Content = new StackLayout

                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 15,
                     VerticalOptions=LayoutOptions.Fill,

                    Children =
                        {

                        la,
                        imbu

                        //new Button { BackgroundColor = Color.BlueViolet,Text="Confirm" ,
                        //TextColor=Color.White, 



                    }
                }
            }

           }


                    };
                    detalleProductos.Children.Add(stacla);
                

            }

        }

        private async Task Imbu_Clicked(object sender, TappedEventArgs e)
        {
            DetVisita detVisita = new DetVisita();
            await Navigation.PushAsync(detVisita, true);
            e.Parameter.ToString();

          
        }

        private async void btnConf_Clicked(object sender, EventArgs e)
        {


            Login login = new Login();
            await Navigation.PushAsync(login, true);
        }

        private async void add_Clicked(object sender, EventArgs e)
        {
            DetVisita detVisita = new DetVisita();
            await Navigation.PushAsync(detVisita, true);
        }

        private async void btnSincronizar_Clicked(object sender, EventArgs e)
        {
            var profiles = Connectivity.ConnectionProfiles;
            //Verifica que el wifi este activo
            if (profiles.Contains(ConnectionProfile.WiFi))
            {
                // Active Wi-Fi connection.
                var current = Connectivity.NetworkAccess;
                //Verifica que tenga conexion a internet
                if (current != NetworkAccess.Internet)
                {
                    await DisplayAlert("Alerta", "Verifica tu conexión a Internet", "OK");
                }
                //Si tiene acceso a internet ejectua el metodo para subir archivos
                await DisplayAlert("Alerta", "Tienes Wifi", "OK");
            }
            //Verifica Que los datos moviles esten activos
            else if (profiles.Contains(ConnectionProfile.Cellular))
            {
                // Active Wi-Fi connection.
                var current = Connectivity.NetworkAccess;
                //Verifica que tenga conexion a internet
                if (current != NetworkAccess.Internet)
                {
                    await DisplayAlert("Alerta", "Verifica tu conexión a Internet", "OK");
                }
                
            }
            String filepath =  obtenerArchivo();
            subirPorFtp(filepath);
            
        }
        public async void SubirImagenFtp(string filePath)
        {
            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                await DisplayAlert("Upload", DependencyService.Get<IFtpWebRequest>().upload("ftp://ftp.swfwmd.state.fl.us", filePath, "Anonymous", "gabriel@icloud.com", "/pub/incoming"), "Ok");

            await Navigation.PopAsync();
        }

        public string obtenerArchivo()
        {
            var backingFile = Path.Combine(FileSystem.AppDataDirectory, "Pictures/Sample/test.jpg");
            return backingFile;
        }

        public async void subirPorFtp(string filepath)
        {
            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                await DisplayAlert("Upload", DependencyService.Get<IFtpWebRequest>().upload("ftp://ftp.swfwmd.state.fl.us", filepath, "Anonymous", "gabriel@icloud.com", "/pub/incoming"), "Ok");

            await Navigation.PopAsync();
        }
    }
}
