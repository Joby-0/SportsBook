using SportsBook.Models;
using SportsBook.Service;

namespace SportsBook
{
    public partial class TappedMeny : TabbedPage
    {
        int count = 0;

        public TappedMeny()
        {
            InitializeComponent();

            // Define your list of tabs
            var tabs = new List<(string Title, string Content)>
        {
            ("Home", "Welcome to the Home Page!"),
            ("Settings", "Here are your settings."),
            ("Profile", "This is your profile page."),
        };

            // Create tabs from the list
            foreach (var tab in tabs)
            {
                var contentPage = new ContentPage
                {
                    Title = tab.Title,
                    Content = new StackLayout
                    {
                        Children =
                    {
                        new Label
                        {
                            Text = tab.Content,
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
