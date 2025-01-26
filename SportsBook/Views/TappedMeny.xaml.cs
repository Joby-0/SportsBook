using SportsBook.Models;
using SportsBook.Service;

namespace SportsBook
{
    public partial class TappedMeny : TabbedPage
    {

        public TappedMeny(string sportname, List<League> leagues)
        {
            InitializeComponent();

            // Define your list of tabs
            
        

            // Create tabs from the list
            foreach (var tab in leagues)
            {
                var contentPage = new ContentPage
                {
                    Title = tab.Description,
                    Content = new StackLayout
                    {
                        Children =
                    {
                        new Label
                        {
                            Text = tab.Description,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        }
                    }
                    }
                };

                // Add the tab to the TabbedPage
                this.Children.Add(contentPage);
            }
        }



        
    }

}
