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

                EspnSportService EspnService = new EspnSportService();
                var SportList = await EspnService.GetEspnSportsAsync();

                

                // Ensure the list is not null or empty
                if (SportList != null && SportList.Any())
                {
                    foreach (var sport in SportList)
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
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}



