using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Xamarin.Forms;
using static DeathClock.Objects.CustomTimeSpan;
using DeathClock.Config;
using System.IO;
using System.Xml.Serialization;
using SkiaSharp.Views.Forms;
using SkiaSharp;

namespace DeathClock.Main
{
	public class MainViewModel : INotifyPropertyChanged
    { 

        public MainViewModel(INavigation navigation)
		{
            /**********************************************************************************************************
             * Construction Method that runs the main progrram of calculating the time lived and the time left to live.
             **********************************************************************************************************/

            //local variable that stores the configuration file to format the application
            ConfigFile configFile;

            // checks is the settings file exists 
            if (File.Exists(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath + "//DeathClockSettings.xml"))
            {
                // Deserializes the file into the configFile object for use in the calculation and formating
                using (FileStream fs = new FileStream(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath + "//DeathClockSettings.xml", FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(ConfigFile));
                    configFile = (ConfigFile)xml.Deserialize(fs);
                }
            }
            else
            {
                // if the file doesnt exists we set some defauts based on my age until a settings file is made by the configuartion
                configFile = new ConfigFile()
                {
                    _birthDay = new DateTime(1998, 3, 13),
                    _ageOfDeath = 85,
                    _year = true,
                    _month = true,
                    _week = true,
                    _day = true,
                    _hour = true,
                    _minute = true,
                    _second = true
                };
                
            }

            /*********************************************************************************** 
             * calculating the interval based on the lowest amount of time that is formated 
             * e.g if the time is formated Year/Month/Day then the inetrval will be every day
             * since you won't see a change in the calculation until at lease a day has gone by
             ***********************************************************************************/
            TimeSpan Interval;
            if (configFile._second)
            {
                Interval = TimeSpan.FromSeconds(1);
            }
            else if (configFile._minute)
            {
                Interval = TimeSpan.FromMinutes(1);
            }
            else if (configFile._hour)
            {
                Interval = TimeSpan.FromHours(1);
            }
            else if (configFile._day)
            {
                Interval = TimeSpan.FromDays(1);
            }
            else if (configFile._week)
            {
                Interval = TimeSpan.FromDays(7);
            }
            else if (configFile._month)
            {
                Interval = TimeSpan.FromDays(30);
            }
            else if (configFile._year)
            {
                Interval = TimeSpan.FromDays(365);
            }

            /****************************************************************************************
             * This is the main loop of the app calculationg the time spent and time until death 
             * should be self explanitory
             ****************************************************************************************/

            //local variables for storing the time left and spent of ones life
            TimeSpan TimeSpanSpent;
            TimeSpan TimeSpanLeft;

            //local variable that stores the percent of the life the user has spent
            float percentLifeLeft;

            _renderer = new Renderers.LoadingBarRenderer();

            //Timer to run the calculation every Interval 
            Device.StartTimer(Interval, () =>
            {
                // get the current time
                DateTime CurrentTime = DateTime.Now;

                //calculating the time lived so far;
                TimeSpanSpent = CurrentTime - configFile.birthDay;
                TimeSpanLeft = configFile._dateOfDeath - CurrentTime;

                //formtaing the time left and time lived into the desired format
                _timeLeft = "--Time Left--\n" + FormatTimeSpan(TimeSpanLeft, configFile._year, configFile._month, configFile._week, configFile._day, configFile._hour, configFile._minute, configFile._second);
                _timeSpent = "--Time Spent--\n" +  FormatTimeSpan(TimeSpanSpent, configFile._year, configFile._month, configFile._week, configFile._day, configFile._hour, configFile._minute, configFile._second);

                percentLifeLeft = (float) (TimeSpanSpent.TotalSeconds / ((TimeSpanSpent.TotalSeconds + TimeSpanLeft.TotalSeconds) / 100)) ;

                ((Renderers.LoadingBarRenderer)_renderer).Percentage = percentLifeLeft;

                return true;
            });


            /*********************************************************
             * Section for Implimenting the commands for the main view
             *********************************************************/

            //creating the command for going to the configuration page
            GoToSettings = new Command(
                execute: async () =>
                {
                    //
                    ConfigPage configurationPage = new ConfigPage(configFile,navigation);
                    await navigation.PushModalAsync(configurationPage);
                });

        }

        /*********************
         * Properties Section 
         *********************/
        string timeLeft;

        public string _timeLeft
        {
            set { SetProperty(ref timeLeft, value); }
            get { return timeLeft; }
        }
       
        string timeSpent;

        public string _timeSpent
        {
            set { SetProperty(ref timeSpent, value); }
            get { return timeSpent; }
        }

       

        float lifeProgress;

        public float _lifeProgress
        {
            set { SetProperty(ref lifeProgress, value); }
            get { return lifeProgress; }
        }

        public Renderers.IRenderer _renderer { get; set; }
        /*********************************
         * Section for Declaring commands
         *********************************/
        public Command GoToSettings { get; }

        /*****************************
         * Other methods for the Page
         *****************************/

        
        /*************************************************************************
         * Methods for setting getting sending events when properties are changed
         *************************************************************************/
        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }



        


    }
}

