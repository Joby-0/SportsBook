using SportsBook.Models;
using SportsBook.Service;

namespace SportsBook
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        OpenSportsService service;

        public MainPage()
        {
            InitializeComponent();
            service = new OpenSportsService();
            //service.GetLeagueAndAllTeamLogos("hej");
        }



        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
