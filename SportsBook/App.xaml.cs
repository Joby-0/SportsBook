namespace SportsBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SportsBook.TappedMeny();
        }
    }
}
