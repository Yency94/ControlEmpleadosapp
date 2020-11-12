using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppControlEmpleados
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetVisita : ContentPage
    {
        public DetVisita()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            btnCamara.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.camera.png");
            btnAudio.Source = ImageSource.FromResource("AppControlEmpleados.Imagenes.microphone.png");
        }
    }
}