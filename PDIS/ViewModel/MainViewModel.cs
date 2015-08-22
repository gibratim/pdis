using GalaSoft.MvvmLight;
using System;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using PDIS.Model;
namespace PDIS.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public const string DayListPropertyName = "DayList";
        private ObservableCollection<Day> _dayList = null;
        public ObservableCollection<Day> DayList
        {
            get
            {
                return _dayList;
            }
            set
            {
                if (_dayList == value)
                {
                    return;
                }
                RaisePropertyChanged(DayListPropertyName);
                
                _dayList = value;
                RaisePropertyChanged(DayListPropertyName);
            }
        }

        public const string CurrentDayPropertyName = "CurrentDay";
        private Day _currentDay = null;

        public Day CurrentDay
        {
            get
            {
                return _currentDay;
            }
            set
            {
                if (_currentDay == value)
                {
                    return;
                }
                RaisePropertyChanged(CurrentDayPropertyName);
             
                _currentDay = value;
                RaisePropertyChanged(CurrentDayPropertyName);
            }
        }


        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                ////    // Code runs in Blend --> create design time data.
                DayList = new ObservableCollection<Day>();
                DayList.Add(new Day { humidity = 20, time = DateTime.Now, weather = new System.Collections.Generic.List<Weather> { new Weather { icon = "01d" } } });
                DayList.Add(new Day { humidity = 20, time = DateTime.Now.AddDays(1) });
                DayList.Add(new Day { humidity = 20, time = DateTime.Now.AddDays(2) });
                DayList.Add(new Day { humidity = 20, time = DateTime.Now.AddDays(3) });
                DayList.Add(new Day { humidity = 20, time = DateTime.Now.AddDays(4) });
                CurrentDay = DayList[0];
            }
            else
            {
                WebClient client = new WebClient();
                client.DownloadStringCompleted += (s, e) =>
                {

                    if (e.Error == null)
                    {

                        RootObject result = JsonConvert.DeserializeObject<RootObject>(e.Result);
                        DayList = new ObservableCollection<Day>(result.list);
                        CurrentDay = DayList[0];
                    }
                    else
                    {
                        MessageBox.Show("sorry try again no internet connection");

                    }
                };
                client.DownloadStringAsync(new Uri("http://api.openweathermap.org/data/2.5/forecast/daily?q=kampala&cnt=7&mode=json&units=metric"));
            }
        }
    }
}