using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Windows.Mvvm;
using PrismSample2019.Core.Events;
using PrismSample2019.Core.Helpers;
using PrismSample2019.Core.Models;
using PrismSample2019.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace PrismSample2019.ViewModels
{
    public class WebViewViewModel : ViewModelBase
    {
        // TODO WTS: Set the URI of the page to show by default
        //private const string DefaultUrl = "http://naver.com";
        private const string DefaultUrl = "http://localhost:11520/";
        private const string DefaultApiUrl = "http://localhost:11520/api/";

        public WebViewViewModel(IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            _eventAggregator = eventAggregator;

            IsLoading = true;
            Source = new Uri(DefaultUrl);

            BrowserBackCommand = new DelegateCommand(() => _webViewService?.GoBack(), () => _webViewService?.CanGoBack ?? false);
            BrowserForwardCommand = new DelegateCommand(() => _webViewService?.GoForward(), () => _webViewService?.CanGoForward ?? false);
            RefreshCommand = new DelegateCommand(() => _webViewService?.Refresh());
            RetryCommand = new DelegateCommand(Retry);
            OpenInBrowserCommand = new DelegateCommand(async () => await Windows.System.Launcher.LaunchUriAsync(Source));

            RunScriptCommand = new DelegateCommand<string>(OnRunScriptCommand);
            GetValuesCommand = new DelegateCommand(OnGetValuesCommand);
            SetValuesCommand = new DelegateCommand(OnSetValuesCommand);

            _eventAggregator.GetEvent<TextChangedEvent>()
                .Subscribe(ReceiveTextChangedEvent);

            _unityContainer = unityContainer;
            _httpFilter = _unityContainer.Resolve(typeof(HttpBaseProtocolFilter), "httpFilter") as HttpBaseProtocolFilter;
            if (_httpFilter == null) return;
        }

        private void ReceiveTextChangedEvent(Core.Models.TextChangedEventArgs obj)
        {
            OnRunScriptCommand($"javascript:textChanging('{obj.Id}','{obj.Text}');");
        }

        private async void OnSetValuesCommand()
        {
            var uri = $"{DefaultApiUrl}values";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new Windows.Web.Http.Headers.HttpMediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                var list = new string[] { "http://kakisoft.com", "http://www.youtube.com/FutureOfDotNET" };
                var content = new HttpStringContent(await Json.StringifyAsync(list), Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

                HttpResponseMessage response = await client.PostAsync(new Uri(uri), content);
                if (response.IsSuccessStatusCode == false)
                {
                    return;
                }
            }

        }

        private async void OnGetValuesCommand()
        {
            string uri = $"{DefaultApiUrl}values";

            HttpCookie blog = Cookies.FirstOrDefault(c => c.Name == "custom_blog");
            if (blog != null)
            {
                blog.Value = "http://kaki104.tistory.com";
                _httpFilter.CookieManager.SetCookie(blog);
            }
            else
            {
                var existCookie = Cookies.FirstOrDefault();
                if (existCookie == null) return;
                var cookie = new HttpCookie("custom_blog", existCookie.Domain, existCookie.Path)
                {
                    Value = "http://kakisoft.com"
                };
                _httpFilter.CookieManager
                    .SetCookie(cookie);
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(new Uri(uri));
                if (response.IsSuccessStatusCode == false)
                {
                    return;
                }

                string result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(result);
            }
        }

        private void OnRunScriptCommand(string script)
        {
            _eventAggregator.GetEvent<RunScriptEvent>()
                .Publish(new Core.Models.RunScriptEventArgs
                {
                    Scripts = new string[]
                    {
                        script
                    }
                });
        }

        private Uri _source;

        public Uri Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private bool _isLoading;
        private readonly IEventAggregator _eventAggregator;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (value)
                {
                    IsShowingFailedMessage = false;
                }

                SetProperty(ref _isLoading, value);
                IsLoadingVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _isLoadingVisibility;

        public Visibility IsLoadingVisibility
        {
            get => _isLoadingVisibility;
            set => SetProperty(ref _isLoadingVisibility, value);
        }

        private bool _isShowingFailedMessage;

        public bool IsShowingFailedMessage
        {
            get => _isShowingFailedMessage;
            set
            {
                if (value)
                {
                    IsLoading = false;
                }

                SetProperty(ref _isShowingFailedMessage, value);
                FailedMesageVisibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _failedMessageVisibility;

        public Visibility FailedMesageVisibility
        {
            get => _failedMessageVisibility;
            set => SetProperty(ref _failedMessageVisibility, value);
        }

        private IWebViewService _webViewService;
        private HttpCookieCollection _cookies;
        private IWebViewService _webViewService2;

        public IWebViewService WebViewService
        {
            get => _webViewService;
            // the WebViewService is set from within the view (instead of IoC) because it needs a reference to the control
            set
            {
                if (_webViewService != null)
                {
                    _webViewService.NavigationComplete -= WebViewService_NavigationComplete;
                    _webViewService.NavigationFailed -= WebViewService_NavigationFailed;
                }

                _webViewService = value;
                _webViewService.NavigationComplete += WebViewService_NavigationComplete;
                _webViewService.NavigationFailed += WebViewService_NavigationFailed;
            }
        }

        public IWebViewService WebViewService2
        {
            get => _webViewService2;
            // the WebViewService is set from within the view (instead of IoC) because it needs a reference to the control
            set
            {
                if (_webViewService2 != null)
                {
                    _webViewService2.NavigationComplete -= WebViewService_NavigationComplete;
                    _webViewService2.NavigationFailed -= WebViewService_NavigationFailed;
                }

                _webViewService2 = value;
                _webViewService2.NavigationComplete += WebViewService_NavigationComplete;
                _webViewService2.NavigationFailed += WebViewService_NavigationFailed;
            }
        }

        public ICommand BrowserBackCommand { get; }

        public ICommand BrowserForwardCommand { get; }

        public ICommand RefreshCommand { get; }

        public ICommand RetryCommand { get; }

        public ICommand OpenInBrowserCommand { get; }

        private void WebViewService_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            NavFailed(e);
        }

        private void WebViewService_NavigationComplete(object sender, WebViewNavigationCompletedEventArgs e)
        {
            NavCompleted(e);
        }

        private void NavCompleted(WebViewNavigationCompletedEventArgs e)
        {
            var manager = _httpFilter.CookieManager;
            var cookies = manager.GetCookies(e.Uri);
            foreach (var item in cookies)
            {
                item.Value = Uri.UnescapeDataString(item.Value);
            }
            Cookies = cookies;

            IsLoading = false;
            RaisePropertyChanged(nameof(BrowserBackCommand));
            RaisePropertyChanged(nameof(BrowserForwardCommand));
        }

        private void NavFailed(WebViewNavigationFailedEventArgs e)
        {
            // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
            IsShowingFailedMessage = true;
        }

        private void Retry()
        {
            IsShowingFailedMessage = false;
            IsLoading = true;

            _webViewService?.Refresh();
        }

        /// <summary>
        /// 스크립트 실행 커맨드
        /// </summary>
        public ICommand RunScriptCommand { get; set; }

        private readonly IUnityContainer _unityContainer;
        private readonly HttpBaseProtocolFilter _httpFilter;

        /// <summary>
        /// 쿠키
        /// </summary>
        public HttpCookieCollection Cookies
        {
            get => _cookies;
            private set => SetProperty(ref _cookies, value);
        }

        public ICommand GetValuesCommand { get; set; }

        public ICommand SetValuesCommand { get; set; }
    }
}
