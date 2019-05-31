using System;
using System.Linq;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using PrismSample2019.Core.Enums;
using PrismSample2019.Core.Events;
using PrismSample2019.Core.Models;
using PrismSample2019.Helpers;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using WinUI = Microsoft.UI.Xaml.Controls;

namespace PrismSample2019.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private static INavigationService _navigationService;
        private Frame _frame;
        private WinUI.NavigationView _navigationView;
        private bool _isBackEnabled;
        private WinUI.NavigationViewItem _selected;

        public ICommand ItemInvokedCommand { get; }

        private readonly IEventAggregator _eventAggregator;

        public bool IsBackEnabled
        {
            get { return _isBackEnabled; }
            set { SetProperty(ref _isBackEnabled, value); }
        }

        public WinUI.NavigationViewItem Selected
        {
            get { return _selected; }
            set { SetProperty(ref _selected, value); }
        }

        public ShellViewModel(INavigationService navigationServiceInstance, IEventAggregator eventAggregator)
        {
            _navigationService = navigationServiceInstance;
            ItemInvokedCommand = new DelegateCommand<WinUI.NavigationViewItemInvokedEventArgs>(OnItemInvoked);

            _eventAggregator = eventAggregator;
        }

        public void Initialize(Frame frame, WinUI.NavigationView navigationView)
        {
            _frame = frame;
            _navigationView = navigationView;
            _frame.NavigationFailed += (sender, e) =>
            {
                throw e.Exception;
            };
            _frame.Navigated += Frame_Navigated;
            _navigationView.BackRequested += OnBackRequested;

            _eventAggregator.GetEvent<NavigateEvent>()
                .Subscribe(ReceiveNavigateEvent);
        }

        private void ReceiveNavigateEvent(NavigateEventArgs obj)
        {
            switch (obj.NavigateEventAction)
            {
                case NavigateEventAction.None:
                    break;
                case NavigateEventAction.Navigate:
                    _navigationService.Navigate(obj.NavigatePageName, obj.NavigateParameter);
                    break;
                case NavigateEventAction.GoHome:
                    _navigationService.Navigate(PageTokens.MainPage, obj.NavigateParameter);
                    _navigationService.RemoveAllPages();
                    break;
                case NavigateEventAction.GoBack:
                    break;
                case NavigateEventAction.GoForeword:
                    break;
            }

        }

        private void OnItemInvoked(WinUI.NavigationViewItemInvokedEventArgs args)
        {
            var item = _navigationView.MenuItems
                            .OfType<WinUI.NavigationViewItem>()
                            .First(menuItem => (string)menuItem.Content == (string)args.InvokedItem);
            var pageKey = item.GetValue(NavHelper.NavigateToProperty) as string;
            _navigationService.Navigate(pageKey, null);
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            IsBackEnabled = _navigationService.CanGoBack();
            Selected = _navigationView.MenuItems
                            .OfType<WinUI.NavigationViewItem>()
                            .FirstOrDefault(menuItem => IsMenuItemForPageType(menuItem, e.SourcePageType));
        }

        private void OnBackRequested(WinUI.NavigationView sender, WinUI.NavigationViewBackRequestedEventArgs args)
        {
            _navigationService.GoBack();
        }

        private bool IsMenuItemForPageType(WinUI.NavigationViewItem menuItem, Type sourcePageType)
        {
            var sourcePageKey = sourcePageType.Name;
            sourcePageKey = sourcePageKey.Substring(0, sourcePageKey.Length - 4);
            var pageKey = menuItem.GetValue(NavHelper.NavigateToProperty) as string;
            return pageKey == sourcePageKey;
        }
    }
}
