using Prism.Commands;
using Prism.Unity.Windows;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using PrismSample2019.Core.Helpers;
using PrismSample2019.Core.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace PrismSample2019.ViewModels
{
    public class MovieViewModel : ViewModelBase
    {
        private SearchRoot _searchResult;
        private IList<Search> _searchs;
        private Search _selectedMovie;
        private readonly INavigationService _navigationService;
        private string _inputMovieTitle;

        public Search SelectedMovie
        {
            get => _selectedMovie;
            set => SetProperty(ref _selectedMovie, value);
        }

        public ICommand BeginningEditCommand { get; set; }

        public ICommand SelectionChangedCommand { get; set; }

        public ICommand QuerySubmittedCommand { get; set; }

        public ICommand HelpCommand { get; set; }

        public MovieViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Init();
        }

        private void Init()
        {
            if (DesignMode.DesignMode2Enabled)
            {
                return;
            }

            BeginningEditCommand = new DelegateCommand(OnBeginningEditCommand);

            SelectionChangedCommand = new DelegateCommand<object>(OnSelectionChangedCommand);

            QuerySubmittedCommand = new DelegateCommand(OnQuerySubmittedCommand);

            HelpCommand = new DelegateCommand(OnHelpCommand);

            PropertyChanged += MovieViewModel_PropertyChanged;

        }

        private void OnHelpCommand()
        {
            Popup popup = PrismUnityApplication.Current.Container.TryResolve<Popup>();
            StackPanel stackPanel = PrismUnityApplication.Current.Container.TryResolve<StackPanel>();
            stackPanel.Background = new SolidColorBrush(Colors.DarkGray);
            stackPanel.Width = 1024;
            stackPanel.Height = 768;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            stackPanel.VerticalAlignment = VerticalAlignment.Stretch;

            TextBlock textBlock = PrismUnityApplication.Current.Container.TryResolve<TextBlock>();
            textBlock.Text = "Popup!!!";
            Button button = PrismUnityApplication.Current.Container.TryResolve<Button>();
            button.Content = "Close";
            button.Click += (s, e) => { popup.IsOpen = false; };
            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(button);
            popup.Child = stackPanel;
            popup.IsOpen = true;
        }

        private async void OnQuerySubmittedCommand()
        {
            await GetMoviesAsync(InputMovieTitle);
        }

        /// <summary>
        /// 입력한 영화 제목
        /// </summary>
        public string InputMovieTitle
        {
            get => _inputMovieTitle;
            set => SetProperty(ref _inputMovieTitle, value);
        }

        private void OnSelectionChangedCommand(object obj)
        {
            IList list = obj as IList;
            if (list == null)
            {
                return;
            }

            Debug.WriteLine($"SelectionChanged {list.Count}");
        }

        private void OnBeginningEditCommand()
        {

        }

        private void MovieViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedMovie):
                    Debug.WriteLine($"SelectedMovie Changed!! {SelectedMovie.Title}");

                    if (SelectedMovie == null)
                    {
                        return;
                    }

                    _navigationService.Navigate(PageTokens.MovieDetailPage, SelectedMovie.ImdbId);
                    break;
            }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            //await GetMoviesAsync();
        }

        private async Task GetMoviesAsync(string movieTitle)
        {
            using (HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync($"http://www.omdbapi.com/?s={movieTitle}&apikey=32941a88");
                if (string.IsNullOrEmpty(result))
                {
                    return;
                }

                SearchRoot searchRoot = await Json.ToObjectAsync<SearchRoot>(result);
                if (searchRoot == null)
                {
                    return;
                }

                _searchResult = searchRoot;
                Searchs = _searchResult.Search?.ToList();
            }
        }
        /// <summary>
        /// 검색 결과
        /// </summary>
        public IList<Search> Searchs
        {
            get => _searchs;
            set => SetProperty(ref _searchs, value);
        }
    }
}
