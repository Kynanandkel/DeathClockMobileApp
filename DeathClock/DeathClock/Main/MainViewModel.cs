using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DeathClock
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public MainViewModel()
		{
			goToSettings = new Command(() =>
			{
				//code for going to the settings page
			});
		}

        public event PropertyChangedEventHandler PropertyChanged;

		string timeSpent;

		public string TimeSpent
		{
			get => timeSpent;
			set
			{
				timeSpent = value;

				var args = new PropertyChangedEventArgs(nameof(TimeSpent));

				PropertyChanged?.Invoke(this, args);
			}
		}

		string timeLeft;

		public string TimeLeft
		{
			get => timeLeft;
			set
			{
				timeLeft = value;

				var args = new PropertyChangedEventArgs(nameof(TimeLeft));

				PropertyChanged?.Invoke(this, args);
			}
		}

		float lifeProgress;

		public float LifeProgress
		{
			get => lifeProgress;
			set
			{
				lifeProgress = value;

				var args = new PropertyChangedEventArgs(nameof(LifeProgress));

				PropertyChanged?.Invoke(this, args);
			}
		}

		public Command goToSettings { get; }
    }
}

