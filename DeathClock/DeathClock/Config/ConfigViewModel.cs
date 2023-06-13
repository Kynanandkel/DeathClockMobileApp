using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace DeathClock.Config
{
	public class ConfigViewModel : INotifyPropertyChanged
    {
        
        /*********************
         * constructor method
         **********************/
        public ConfigViewModel(ConfigFile configFile)
		{
            //Setting the ViewModels(_vm) variables to that of the onesin the cofig so users
            //can see what the settings already are
            //this is taken from the config file fed from the MainViewModel where the file is deserialized
            _vmBirthDay = configFile.birthDay;
            _vmAgeOfDeath = configFile.ageOfDeath;
            _vmYear = configFile._year;
            _vmMonth = configFile._month;
            _vmWeek = configFile._week;
            _vmDay = configFile._day;
            _vmHour = configFile._hour;
            _vmMinute = configFile._minute;
            _vmSecond = configFile._second;

            //Save command Serializes the current view models variables into the config file and saves in the apps com. folder
            SaveConfig = new Command(
                execute: () =>
                {
                    using (FileStream fs = new FileStream(Android.App.Application.Context.GetExternalFilesDir("").AbsolutePath + "//DeathClockSettings.xml", FileMode.Create))
                    {
                        ConfigFile ConfigFile = new ConfigFile()
                        {
                            _birthDay = _vmBirthDay,
                            _ageOfDeath = _vmAgeOfDeath,
                            _year = _vmYear,
                            _month = _vmMonth,
                            _week = _vmWeek,
                            _day = _vmDay,
                            _hour = _vmHour,
                            _minute = _vmMinute,
                            _second = _vmSecond
                        };
                        XmlSerializer xml = new XmlSerializer(typeof(ConfigFile));
                        xml.Serialize(fs, ConfigFile);
                    }
                });
           
		}

        /******************
         *Class Properties
         ******************/
        
        DateTime vmBirthDay;

        public DateTime _vmBirthDay
        {
            set { SetProperty(ref vmBirthDay, value); }
            get { return vmBirthDay; }
        }

        
        int vmAgeOfDeath;

        public int _vmAgeOfDeath
        {
            set { SetProperty(ref vmAgeOfDeath, value); }
            get { return vmAgeOfDeath; }
        }

        
        bool vmYear;

        public bool _vmYear
        {
            set { SetProperty(ref vmYear, value); }
            get { return vmYear; }
        }

        
        bool vmMonth;

        public bool _vmMonth
        {
            set { SetProperty(ref vmMonth, value); }
            get { return vmMonth; }
        }

        
        bool vmWeek;
        public bool _vmWeek
        {
            set { SetProperty(ref vmWeek, value); }
            get { return vmWeek; }
        }

        
        bool vmDay;
        public bool _vmDay
        {
            set { SetProperty(ref vmDay, value); }
            get { return vmDay; }
        }

        
        bool vmHour;
        public bool _vmHour
        {
            set { SetProperty(ref vmHour, value); }
            get { return vmHour; }
        }

        
        bool vmMinute;
        public bool _vmMinute
        {
            set { SetProperty(ref vmMinute, value); }
            get { return vmMinute; }
        }

 
        bool vmSecond;
        public bool _vmSecond
        {
            set { SetProperty(ref vmSecond, value); }
            get { return vmSecond; }
        }

        /*********************************
         * section for declaring commands
         *********************************/
        public Command SaveConfig { get; }

        /***************************************************
         * Methods for sending events when properties change 
         ***************************************************/
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

