using Prism.Events;
using Prism.Unity.Windows;
using PrismSample.RT;
using PrismSample2019.Core.Events;
using PrismSample2019.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace PrismSample2019.Services
{
    public class WebViewService : IWebViewService
    {
        private WebView _webView;
        private readonly IEventAggregator _eventAggregator;

        public WebViewService(WebView webViewInstance, IEventAggregator eventAggregator)
        {
            _webView = webViewInstance;
            _webView.NavigationCompleted += WebView_NavigationCompleted;
            _webView.NavigationFailed += WebView_NavigationFailed;
            _webView.NavigationStarting += _webView_NavigationStarting;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<RunScriptEvent>()
                .Subscribe(ReceiveRunScriptEventAsync);

            _webView.ScriptNotify += _webView_ScriptNotifyAsync;
        }

        private async void _webView_ScriptNotifyAsync(object sender, NotifyEventArgs e)
        {
            var paras = e.Value.Split(':');
            if (paras.Length < 2) return;
            var messageDialog = new MessageDialog(paras.LastOrDefault());
            if (paras.FirstOrDefault() == "confirm")
            {
                messageDialog.Commands.Add(new UICommand("Yes"));
                messageDialog.Commands.Add(new UICommand("No"));
                messageDialog.DefaultCommandIndex = 1;
                messageDialog.CancelCommandIndex = 1;
            }
            var restul = await messageDialog.ShowAsync();
            if (paras.FirstOrDefault() != "confirm") return;
            if (restul.Label.Equals("Yes"))
            {
                ReceiveRunScriptEventAsync(new RunScriptEventArgs
                {
                    Scripts = new string[] { "javascript:confirmBoxResult('yes')" }
                });
            }
            else
            {
                ReceiveRunScriptEventAsync(new RunScriptEventArgs
                {
                    Scripts = new string[] { "javascript:confirmBoxResult('no')" }
                });
            }
        }

        /// <summary>
        /// 스크립트 실행 이벤트 수신
        /// </summary>
        /// <param name="obj"></param>
        private async void ReceiveRunScriptEventAsync(RunScriptEventArgs obj)
        {
            if (obj.Scripts == null || obj.Scripts.Any() == false) return;
            //스크립트 실행
            var result = await _webView.InvokeScriptAsync("eval", obj.Scripts);
            if (string.IsNullOrEmpty(result)) return;
            var messageDialog = new MessageDialog(result);
            await messageDialog.ShowAsync();
            
        }

        private void _webView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            var component = PrismUnityApplication.Current.Container.TryResolve<InterfaceComponent>();
            _webView.AddWebAllowedObject("interface", component);

        }

        public void Detatch()
        {
            if (_webView != null)
            {
                _webView.NavigationCompleted -= WebView_NavigationCompleted;
                _webView.NavigationFailed -= WebView_NavigationFailed;
                _webView.NavigationStarting -= _webView_NavigationStarting;

                _eventAggregator.GetEvent<RunScriptEvent>()
                    .Unsubscribe(ReceiveRunScriptEventAsync);

                _webView.ScriptNotify -= _webView_ScriptNotifyAsync;
            }
        }

        private void WebView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            NavigationFailed?.Invoke(sender, e);
        }

        private void WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs e)
        {
            NavigationComplete?.Invoke(sender, e);
        }

        public void Refresh()
        {
            _webView?.Refresh();
        }

        public void GoForward()
        {
            _webView?.GoForward();
        }

        public void GoBack()
        {
            _webView?.GoBack();
        }

        public bool CanGoForward => _webView?.CanGoForward == true;

        public bool CanGoBack => _webView?.CanGoBack == true;

        public event EventHandler<WebViewNavigationCompletedEventArgs> NavigationComplete;

        public event EventHandler<WebViewNavigationFailedEventArgs> NavigationFailed;
    }
}
