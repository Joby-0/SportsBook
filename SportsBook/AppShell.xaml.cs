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

                OpenSportsService service = new OpenSportsService();
                GroupedSports groupedSports = new GroupedSports();
                List<League>? listOfLeagues;
                Sport sS = new Sport();

                sS.List = await service.GetApiSportsAsync(); // Ensure data fetching completes
                var gropedSportList = sS.List.GroupBy(g => g.Group).Select(g => new GroupedSports
                {
                    sportname = g.Key,
                    Sportslist = g.ToList(),

                }).ToList();
               
                // Ensure the list is not null or empty
                if (gropedSportList != null && gropedSportList.Any())
                {
                    foreach (var sport in gropedSportList)
                    {
                        var sc = new ShellContent
                        {
                            Title = sport.sportname, 
                            ContentTemplate = new DataTemplate(() =>
                            {
                                    listOfLeagues = gropedSportList
                                    .FirstOrDefault(g => g.sportname == sport.sportname)?
                                    .Sportslist;
                                

                                return new SportsPage(sport.sportname, listOfLeagues ?? new List<League>());
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
                
            }
        }
    }

}



