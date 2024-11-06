using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Women_T20_2024
{
    public class T20_Data : INotifyPropertyChanged
    {
        #region Fields

        private int selectedIndex;
        private int play;
        private int wins;
        private string? runs;
        private int catchValue;
        private int score6s;
        private int score4s;
        private int boundaries;
        private int batter;
        private string? batterImage;
        private int wicketTaker;
        private string? wicketTakerImage;
        private string? name;
        private int score;
        private Dictionary<string, CountryData>? countryData;
        private Dictionary<string, List<PlayersData>>? playersData;
        private ObservableCollection<PlayersData>? data;
        private ObservableCollection<ScoreInfo>? scoreData;

        #endregion

        #region Properties

        public ObservableCollection<string> CountryOption { get; set; }

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                UpdateIndex(value);
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        public int Play
        {
            get => play;
            set { play = value; OnPropertyChanged(nameof(Play)); }
        }

        public int Wins
        {
            get => wins;
            set { wins = value; OnPropertyChanged(nameof(Wins)); }
        }

        public string? Runs
        {
            get => runs;
            set { runs = value; OnPropertyChanged(nameof(Runs)); }
        }

        public int CatchValue
        {
            get => catchValue;
            set { catchValue = value; OnPropertyChanged(nameof(CatchValue)); }
        }

        public int Score6s
        {
            get => score6s;
            set { score6s = value; OnPropertyChanged(nameof(Score6s)); }
        }

        public int Score4s
        {
            get => score4s;
            set { score4s = value; OnPropertyChanged(nameof(Score4s)); }
        }

        public int Boundaries
        {
            get => boundaries;
            set { boundaries = value; OnPropertyChanged(nameof(Boundaries)); }
        }

        public int Batter
        {
            get => batter;
            set { batter = value; OnPropertyChanged(nameof(Batter)); }
        }

        public string? BatterImage
        {
            get => batterImage;
            set { batterImage = value; OnPropertyChanged(nameof(BatterImage)); }
        }

        public int WicketTaker
        {
            get => wicketTaker;
            set { wicketTaker = value; OnPropertyChanged(nameof(WicketTaker)); }
        }

        public string? WicketTakerImage
        {
            get => wicketTakerImage;
            set { wicketTakerImage = value; OnPropertyChanged(nameof(WicketTakerImage)); }
        }

        public string? Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public int Score
        {
            get => score;
            set { score = value; OnPropertyChanged(nameof(Score)); }
        }

        public ObservableCollection<PlayersData>? Data
        {
            get => data;
            set { data = value; OnPropertyChanged(nameof(Data)); }
        }

        public ObservableCollection<ScoreInfo>? ScoreData
        {
            get => scoreData;
            set { scoreData = value; OnPropertyChanged(nameof(ScoreData)); }
        }

        #endregion

        #region Constructor

        public T20_Data()
        {
            CountryOption = new ObservableCollection<string> { "New Zealand", "Australia", "India", "Pakistan","Sri Lanka",
                "West Indies", "South Africa", "England", "Bangladesh", "Scotland"};

            var executingAssembly = typeof(App).GetTypeInfo().Assembly;
            using (var stream = executingAssembly.GetManifestResourceStream("Women_T20_2024.Resources.country_data.json"))
                if (stream != null)
                {
                    using (var textStream = new StreamReader(stream))
                    {
                        countryData = JsonConvert.DeserializeObject<Dictionary<string, CountryData>>(textStream.ReadToEnd().Trim());
                    }
                }

            using (var stream = executingAssembly.GetManifestResourceStream("Women_T20_2024.Resources.players_data.json"))
                if (stream != null)
                {
                    using (var textStream = new StreamReader(stream))
                    {
                        playersData = JsonConvert.DeserializeObject<Dictionary<string, List<PlayersData>>>(textStream.ReadToEnd().Trim());
                    }
                }

            SelectedIndex = 0;
        }

        #endregion

        #region Method

        private void UpdateIndex(int value)
        {
            if (value >= 0 && value < CountryOption.Count)
            {
                string selectedCountry = CountryOption[value];
                if (countryData != null)
                {
                    if (countryData.ContainsKey(selectedCountry))
                    {
                        var data = countryData[selectedCountry];
                        Play = data.Play;
                        Wins = data.Wins;
                        Runs = data.Runs;
                        CatchValue = data.CatchValue;
                        Boundaries = data.Boundaries;
                        Batter = data.Batter;
                        BatterImage = data.BatterImage;
                        WicketTaker = data.WicketTaker;
                        WicketTakerImage = data.WicketTakerImage;
                        Score6s = data.Score6s;
                        Score4s = data.Score4s;

                        ScoreData = new ObservableCollection<ScoreInfo>
                        {
                            new ScoreInfo { Category = "Total 6s Scored", Value = Score6s },
                            new ScoreInfo { Category = "Total 4s Scored", Value = Score4s }
                        };
                    }
                }

                if (playersData != null)
                {
                    if (playersData.ContainsKey(selectedCountry))
                    {
                        Data = new ObservableCollection<PlayersData>(playersData[selectedCountry]);

                        if (Data.Count > 0)
                        {
                            Name = Data[0].Name;
                            Score = Data[0].Score;
                        }
                    }
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}