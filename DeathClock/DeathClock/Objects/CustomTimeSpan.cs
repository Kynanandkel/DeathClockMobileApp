using System;
namespace DeathClock.Objects
{
    public class CustomTimeSpan
    {

        // Consts for converting between times, using seconds as its the lowest form of time used in the app so is best for doing maths to
        // Not the most accurate so may come in and convert to longs to get the fractions that are caused by leap years
        // or may make a stratergy to find out what kind of year it is and what month is is.

        const double SecondsInAYear = 31536000; // this is the calculation taken from 365 days * 24 hours * 60 minutes * 60 seconds
        const double SecondsInAMonth = SecondsInAYear / 12;
        const double SecondsInADay = SecondsInAYear / 365;
        const double SecondsInAWeek = SecondsInADay * 7;
        const double SecondsInAnHour = SecondsInADay / 24;
        const double SecondsInAMinute = 60;

        public static string FormatTimeSpan(TimeSpan time, bool year, bool month, bool week, bool day, bool hour, bool minute, bool second)
        {
            string timeSpan = "";

            if (year)
            {
                timeSpan = $"   {((int)(time.TotalSeconds / SecondsInAYear))} Years \n";

                TimeSpan newTimeSpan = new TimeSpan(0, 0, 0, (int)(time.TotalSeconds % SecondsInAYear));
                time = newTimeSpan;



            }
            if (month)
            {
                timeSpan += $"   {((int)(time.TotalSeconds / SecondsInAMonth))} Months \n";

                TimeSpan newTimeSpan = new TimeSpan(0, 0, 0, (int)(time.TotalSeconds % SecondsInAMonth));
                time = newTimeSpan;



            }
            if (week)
            {
                timeSpan += $"   {((int)(time.TotalSeconds / SecondsInAWeek))} Weeks \n";

                TimeSpan newTimeSpan = new TimeSpan(0, 0, 0, (int)(time.TotalSeconds % SecondsInAWeek));
                time = newTimeSpan;



            }
            if (day)
            {
                timeSpan += $"   {((int)(time.TotalSeconds / SecondsInADay))} Days \n";

                TimeSpan newTimeSpan = new TimeSpan(0, 0, 0, (int)(time.TotalSeconds % SecondsInADay));
                time = newTimeSpan;



            }
            if (hour)
            {
                timeSpan += $"   {((int)(time.TotalSeconds / SecondsInAnHour))} Hours \n";

                TimeSpan newTimeSpan = new TimeSpan(0, 0, 0, (int)(time.TotalSeconds % SecondsInAnHour));
                time = newTimeSpan;



            }
            if (minute)
            {
                timeSpan += $"   {((int)(time.TotalSeconds / SecondsInAMinute))} Minutes \n";

                TimeSpan newTimeSpan = new TimeSpan(0, 0, 0, (int)(time.TotalSeconds % SecondsInAMinute));
                time = newTimeSpan;



            }
            if (second)
            {
                timeSpan += $"   {(int)(time.TotalSeconds)} Seconds";

                TimeSpan newTimeSpan = new TimeSpan(0, 0, 0, (int)(time.TotalSeconds % SecondsInAnHour));
                time = newTimeSpan;




            }

            return timeSpan;
        }
    }
}

