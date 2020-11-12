using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppControlEmpleados
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            MainPage = new NavigationPage();
            NavigationPage.SetHasNavigationBar(MainPage, false);
            MainPage.Navigation.PushAsync(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
