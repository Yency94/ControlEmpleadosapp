using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;

namespace AppControlEmpleados
{
    public partial class MainPage : ContentPage
    {
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
            // Actualizar Agenda
            //  DetalledeAgenda.Children.Clear();
            detalleProductos.Children.Clear();

            for (int i = 0; i < 5; i++)
            {

                Label la = new Label
                {
                    Text = "Ciente Prueba",
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

                imbu.Clicked += Imbu_Clicked;
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

        private void Imbu_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
    }
}
