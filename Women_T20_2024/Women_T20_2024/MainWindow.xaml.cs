using Syncfusion.UI.Xaml.Charts;
using System.Windows;

namespace Women_T20_2024
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChartTrackBallBehavior_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            if (viewModel != null && e.CurrentPointInfos.Count > 0)
            {
                var dataPointInfo = e.CurrentPointInfos.First();
                var dataPoint = dataPointInfo.Item as PlayersData;
                if (dataPoint != null)
                {
                    viewModel.Name = dataPoint.Name;
                    viewModel.Score = dataPoint.Score; 
                }
            }
        }
    }
}