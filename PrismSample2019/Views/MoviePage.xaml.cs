using System;

using PrismSample2019.ViewModels;

using Windows.UI.Xaml.Controls;

namespace PrismSample2019.Views
{
    public sealed partial class MoviePage : Page
    {
        private MovieViewModel ViewModel => DataContext as MovieViewModel;

        // TODO WTS: Change the grid as appropriate to your app, adjust the column definitions on MoviePage.xaml.
        // For more details see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid
        public MoviePage()
        {
            InitializeComponent();
        }
    }
}
