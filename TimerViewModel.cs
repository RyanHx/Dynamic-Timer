using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;

namespace DynamicTimer
{
    public class TimerViewModel : BaseViewModel
    {
        public int MainHours { get; set; }
        public int MainMinutes { get; set; }
        public int MainSeconds { get; set; }

        public ObservableCollection<TimeSpan> AddTimers { get; }
        public int AddHours { get; set; }
        public int AddMinutes { get; set; }
        public int AddSeconds { get; set; }

        public ICommand StartTimer { get; }
        public ICommand StopTimer { get; }
        public ICommand AddTimeComm { get; }
        public ICommand CreateAddTimer { get; }
        public ICommand DeleteAddTimer { get; }

        private static Timer Timer;
        public TimeSpan Time { get; set; }
        public string MainTimerText
        {
            get => Time.ToString("hh\\:mm\\:ss");            
        }

        public TimerViewModel()
        {
            AddTimers = new ObservableCollection<TimeSpan>();
            CreateAddTimer = new RelayCommand(() => AddTimers.Add(new TimeSpan(AddHours, AddMinutes, AddSeconds)));
            StartTimer = new RelayCommand(StartMainTimer);
            StopTimer = new RelayCommand(StopMainTimer);
            DeleteAddTimer = new RelayCommand(DeleteAdditionalTimer);
            AddTimeComm = new RelayCommand(AddTime);
            Timer = new Timer(1000);
            Timer.Elapsed += OnTimerElapsed;
        }

        public void StartMainTimer()
        {
            if (Timer.Enabled)
                return;
            Time = new TimeSpan(MainHours, MainMinutes, MainSeconds);            
            Timer.Start();
        }

        private void OnTimerElapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            Time = Time.Subtract(TimeSpan.FromSeconds(1));
            if(Time.TotalSeconds <= 0)
            {
                Timer.Stop();
            }
        }

        public void StopMainTimer()
        {
            Timer.Stop();
        }

        public void DeleteAdditionalTimer(object arg)
        {
            var timer = (TimeSpan)arg;
            AddTimers.Remove(timer);
        }

        public void AddTime(object arg)
        {
            var timeAddModel = (TimeSpan)arg;
            Time = Time.Add(timeAddModel);
        }
    }
}
