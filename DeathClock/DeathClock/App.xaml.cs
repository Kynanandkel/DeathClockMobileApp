using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeathClock
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Main.MainPage());
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

