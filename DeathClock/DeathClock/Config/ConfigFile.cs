using System;
using System.Xml.Serialization;

namespace DeathClock.Config
{
	public class ConfigFile
	{ /* class to represent the settings file for the death clock application*/
        public ConfigFile() { }
		

        /********************************************************************************************************************************************************
         * I've set up the properties up this way to save users from having to calculate the their age of death or for them to enter their birthdate to their age
         ********************************************************************************************************************************************************/

        [XmlElement]
        public DateTime birthDay;
        public DateTime _birthDay
        {
            get { return birthDay; }
            set
            {
                birthDay = value;
                _age = (DateTime.Now - birthDay).Days / 365;
            }
        }

        public int _age { get; private set;}


        [XmlElement]
        public int ageOfDeath;
        public int _ageOfDeath
        {
            get { return ageOfDeath; }
            set
            {
                ageOfDeath = value;
                _dateOfDeath = _birthDay.AddYears(_ageOfDeath);
            } 
        }


        public DateTime _dateOfDeath { get; private set; }


        [XmlElement]
        public bool _year { get; set; }

        

        [XmlElement]
        public bool _month { get; set; }

        

        [XmlElement]
        public bool _week { get; set; }

        

        [XmlElement]
        public bool _day { get; set; }

        

        [XmlElement]
        public bool _hour { get; set; }

        

        [XmlElement]
        public bool _minute { get; set; }

        

        [XmlElement]
        public bool _second { get; set; }

    }

}

