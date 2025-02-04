using SportsBook.Models;
using SportsBook.Service;
using System.Threading.Tasks;

namespace SportsBook
{
    public partial class AppShell : Shell
    {
        
        public AppShell()
        {
            InitializeComponent();
            
            AddShellContentasync();
            


        }
        private async void AddShellContentasync()
        {
            try
            {

                //OpenSportsService service = new OpenSportsService();
                //GroupedSports groupedSports = new GroupedSports();
                //List<League>? listOfLeagues;
                //Sport sS = new Sport();
                EspnSportService a = new EspnSportService();
                var b = await a.GetEspnSportsAsync();

                //sS.List = await service.GetApiSportsAsync(); // Ensure data fetching completes
                //var gropedSportList = sS.List.GroupBy(g => g.Group).Select(g => new GroupedSports
                //{
                //    sportname = g.Key,
                //    Sportslist = g.ToList(),

                //}).ToList();

                // Ensure the list is not null or empty
                if (b != null && b.Any())
                {
                    foreach (var sport in b)
                    {
                        var sc = new ShellContent
                        {
                            Title = sport.SportName,
                            ContentTemplate = new DataTemplate(() =>
                            {
                                

                               var tabbedPage = new SportsPage(sport.SportName, sport.SportUrl);

                                return tabbedPage;
                            })
                        };

                        this.Items.Add(sc); // Add to Shell items
                    }

                }
                else
                {
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

}



