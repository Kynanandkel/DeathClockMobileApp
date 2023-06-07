using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DeathClock.Main;

namespace DeathClock.Config
{
    public partial class ConfigPage : ContentPage
    {
        INavigation _Navigation;
        public ConfigPage(ConfigFile configFile, INavigation navigation)
        {
            InitializeComponent();
            _Navigation = navigation;
            BindingContext = new ConfigViewModel(configFile);
        }

        /*
         * Method for returning to the first page when the phones back button is pressed 
         * Doesn't follow Proper MVVM but will find a solution in due time
         */
        protected override bool OnBackButtonPressed()
        {
            MainPage mainPage = new MainPage();
            _Navigation.PushModalAsync(mainPage);
            return true;
        }
    }
}

