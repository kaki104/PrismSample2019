﻿using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Windows.Mvvm;
using PrismSample2019.Controls;
using PrismSample2019.Core.Interfaces;
using PrismSample2019.Core.Models;
using PrismSample2019.Models;
using System;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls.Primitives;

namespace PrismSample2019.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUnityContainer _unityContainer;

        public ICommand AddPersonCommand { get; set; }
        public ICommand TestCommand { get; set; }

        public MainViewModel(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;

            Init();
        }

        private void Init()
        {
            AddPersonCommand = new DelegateCommand(OnAddPersonCommand);
            TestCommand = new DelegateCommand<string>(OnTestCommand);
        }

        private void OnTestCommand(string obj)
        {
            switch (obj)
            {
                case "1":
                    Test1();
                    break;
                case "2":
                    Test2();
                    break;
                case "3":
                    Test3();
                    break;
                case "4":
                    Test4();
                    break;
                case "5":
                    Test5();
                    break;
                case "6":
                    Test6();
                    break;
                case "7":
                    Test7();
                    break;
                case "11":
                    Test11();
                    break;
                case "12":
                    Test12();
                    break;
                case "13":
                    Test13();
                    break;
                case "21":
                    Test21();
                    break;
                case "22":
                    Test22();
                    break;
                case "23":
                    Test23();
                    break;
                case "31":
                    Test31();
                    break;
                case "32":
                    Test32();
                    break;
                case "41":
                    Test41();
                    break;
                case "42":
                    Test42();
                    break;
                case "43":
                    Test43();
                    break;
                case "44":
                    Test44();
                    break;
                case "51":
                    Test51();
                    break;
                case "52":
                    Test52();
                    break;
                case "53":
                    Test53();
                    break;
            }
        }

        /// <summary>
        /// HierarchicalLifetimeManager
        /// </summary>
        private async void Test53()
        {
            _unityContainer.RegisterType<ICar, Audi>(new HierarchicalLifetimeManager());
            _unityContainer.RegisterType<IDriver, UWPDriver>();

            var childContainer = _unityContainer.CreateChildContainer();

            var driver1 = _unityContainer.Resolve<IDriver>();
            await driver1.RunCarAsync();

            var driver2 = _unityContainer.Resolve<IDriver>();
            await driver2.RunCarAsync();

            var driver3 = childContainer.Resolve<IDriver>();
            await driver3.RunCarAsync();

            var driver4 = childContainer.Resolve<IDriver>();
            await driver4.RunCarAsync();
        }

        /// <summary>
        /// ContainerControlledLifetimeManager
        /// </summary>
        private async void Test52()
        {
            _unityContainer.RegisterType<ICar, Audi>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IDriver, UWPDriver>();

            var driver1 = _unityContainer.Resolve<IDriver>();
            await driver1.RunCarAsync();

            var driver2 = _unityContainer.Resolve<IDriver>();
            await driver2.RunCarAsync();
        }

        /// <summary>
        /// TransientLifetimeManager
        /// </summary>
        private async void Test51()
        {
            _unityContainer.RegisterType<ICar, Audi>(new TransientLifetimeManager());
            _unityContainer.RegisterType<IDriver, UWPDriver>();

            var driver1 = _unityContainer.Resolve<IDriver>();
            await driver1.RunCarAsync();

            var driver2 = _unityContainer.Resolve<IDriver>();
            await driver2.RunCarAsync();
        }

        /// <summary>
        /// DependencyOverride
        /// </summary>
        private async void Test44()
        {
            _unityContainer.RegisterType<ICar, Audi>();
            _unityContainer.RegisterType<IDriver, UWPDriver>();

            var driver1 = _unityContainer.Resolve<IDriver>();
            await driver1.RunCarAsync();

            var driver2 = _unityContainer.Resolve<IDriver>(
                new DependencyOverride(typeof(ICar), new BMW()));
            await driver2.RunCarAsync();
        }

        /// <summary>
        /// PropertyOverride
        /// </summary>
        private async void Test43()
        {
            _unityContainer.RegisterType<ICar, Audi>();
            _unityContainer.RegisterType<IDriver, UWPDriverDependencyAttribute>();

            var driver1 = _unityContainer.Resolve<IDriver>();
            await driver1.RunCarAsync();

            var driver2 = _unityContainer.Resolve<IDriver>(
                new PropertyOverride("Car", new BMW()));
            await driver2.RunCarAsync();
        }

        /// <summary>
        /// Override Multiple Parameters
        /// </summary>
        private async void Test42()
        {
            _unityContainer.RegisterType<IDriver, UWPDriverMultiOverride>();

            var p = new ParameterOverrides
            {
                { "car1", new Audi() },
                { "carKey1", new AudiKey() },
                { "car2", new BMW() },
                { "carKey2", new BMWKey() }
            };
            var driver = _unityContainer.Resolve<IDriver>(p);
            await driver.RunCarAsync();
        }

        /// <summary>
        /// ParameterOverride
        /// </summary>
        private async void Test41()
        {
            _unityContainer.RegisterType<ICar, Audi>();
            _unityContainer.RegisterType<IDriver, UWPDriver>();

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();

            var driver2 = _unityContainer.Resolve<IDriver>(
                new ParameterOverride("car", new BMW()));
            await driver2.RunCarAsync();
        }

        /// <summary>
        /// Run-time Configuration Method Injection
        /// </summary>
        private async void Test32()
        {
            _unityContainer.RegisterType<IDriver, UWPDriverRuntimeMethod>(
                new InjectionMethod("MyCar", new Audi()),
                new InjectionMethod("MyCarKey", new BMWKey()));

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();
        }

        /// <summary>
        /// Method Injection
        /// </summary>
        private async void Test31()
        {
            _unityContainer.RegisterType<ICar, Audi>();

            _unityContainer.RegisterType<IDriver, UWPDriverInjectionMethod>();

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();
        }

        /// <summary>
        /// Run-time Configuration
        /// </summary>
        private async void Test23()
        {
            _unityContainer.RegisterType<IDriver, UWPDriverRuntime>(
                new InjectionProperty("MyCar", new BMW()));

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();
        }

        /// <summary>
        /// Named Mapping
        /// </summary>
        private async void Test22()
        {
            _unityContainer.RegisterType<ICar, BMW>();
            _unityContainer.RegisterType<ICar, Audi>("MyCar");

            _unityContainer.RegisterType<IDriver, UWPDriverNamedMapping>();

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();
        }

        /// <summary>
        /// Dependency Attribute
        /// </summary>
        private async void Test21()
        {
            _unityContainer.RegisterType<ICar, BMW>();

            _unityContainer.RegisterType<IDriver, UWPDriverDependencyAttribute>();

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();
        }

        /// <summary>
        /// Primitive Type Parameter
        /// </summary>
        private async void Test13()
        {
            _unityContainer.RegisterType<ICar, Audi>();

            _unityContainer.RegisterType<IDriver, UWPDriverPrimitiveType>(
                new InjectionConstructor(
                    new object[] { _unityContainer.Resolve<ICar>(), "Kaki" }));

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();
        }

        /// <summary>
        /// Multiple Constructors
        /// </summary>
        private async void Test12()
        {
            _unityContainer.RegisterType<ICar, BMW>();

            _unityContainer.RegisterType<IDriver, UWPDriverMultiConstructor>();

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();

            var driver2 = new UWPDriverMultiConstructor("Empty");
            await driver2.RunCarAsync();
        }

        /// <summary>
        /// Multiple Parameters
        /// </summary>
        private async void Test11()
        {
            _unityContainer.RegisterType<ICar, Audi>();
            _unityContainer.RegisterType<ICarKey, AudiKey>();

            _unityContainer.RegisterType<IDriver, UWPDriverWithKey>();

            var driver = _unityContainer.Resolve<IDriver>();
            await driver.RunCarAsync();
        }

        /// <summary>
        /// Register Instance
        /// </summary>
        private async void Test7()
        {
            _unityContainer.RegisterType<ICar, Audi>();
            var car = _unityContainer.Resolve<ICar>();
            //register a singleton using Container.RegisterType<IInterface, Type>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterInstance(car);

            var driver1 = _unityContainer.Resolve<UWPDriver>();
            await driver1.RunCarAsync();
            await driver1.RunCarAsync();

            var driver2 = _unityContainer.Resolve<UWPDriver>();
            await driver2.RunCarAsync();
        }

        /// <summary>
        /// Register Driver type
        /// </summary>
        private async void Test6()
        {
            _unityContainer.RegisterType<ICar, BMW>();
            _unityContainer.RegisterType<ICar, Audi>("OpenCar");

            _unityContainer.RegisterType<IDriver, UWPDriver>();
            _unityContainer.RegisterType<IDriver, UWPDriver>("OpenCarDriver",
                new InjectionConstructor(_unityContainer.Resolve<ICar>("OpenCar")));

            var driver1 = _unityContainer.Resolve<IDriver>();
            await driver1.RunCarAsync();

            var driver2 = _unityContainer.Resolve<IDriver>("OpenCarDriver");
            await driver2.RunCarAsync();
        }

        /// <summary>
        /// Register Named Type
        /// </summary>
        private async void Test5()
        {
            _unityContainer.RegisterType<ICar, BMW>();
            _unityContainer.RegisterType<ICar, Audi>("OpenCar");

            var bmw = _unityContainer.Resolve<ICar>();
            var driver1 = new UWPDriver(bmw);
            await driver1.RunCarAsync();

            var audi = _unityContainer.Resolve<ICar>("OpenCar");
            var driver2 = new UWPDriver(audi);
            await driver2.RunCarAsync();

        }

        /// <summary>
        /// Multiple Registration
        /// </summary>
        private async void Test4()
        {
            _unityContainer.RegisterType<ICar, BMW>();
            _unityContainer.RegisterType<ICar, Audi>();

            var driver = _unityContainer.Resolve<UWPDriver>();
            await driver.RunCarAsync();
        }

        /// <summary>
        /// Resolve 2Driver
        /// </summary>
        private async void Test3()
        {
            _unityContainer.RegisterType<ICar, BMW>();

            var driver1 = _unityContainer.Resolve<UWPDriver>();
            await driver1.RunCarAsync();

            var driver2 = _unityContainer.Resolve<UWPDriver>();
            await driver2.RunCarAsync();
        }

        private async void Test2()
        {
            //인터페이스에 BMW연결
            _unityContainer.RegisterType<ICar, BMW>();

            var driver = _unityContainer.Resolve<UWPDriver>();
            await driver.RunCarAsync();
        }

        private void Test1()
        {
            //수동 종속성 주입
            var driver = new UWPDriver(new BMW());
            driver.RunCarAsync().GetAwaiter().GetResult();
            //await driver.RunCarAsync();
        }

        private void OnAddPersonCommand()
        {
            var popup = new Popup();
            var addPerson = new AddPersonControl();
            #region UnityContainer

            //var popup = _unityContainer.Resolve<Popup>();
            //var addPerson = _unityContainer.Resolve<AddPersonControl>();

            #endregion
            var bounds = CoreWindow.GetForCurrentThread().Bounds;
            addPerson.Width = bounds.Width / 2;
            addPerson.Height = bounds.Height / 2;
            popup.HorizontalOffset = (bounds.Width - addPerson.Width) / 2;
            popup.VerticalOffset = (bounds.Height - addPerson.Height) / 2;
            popup.Child = addPerson;
            popup.Closed += Popup_Closed;
            popup.IsOpen = true;

            void Popup_Closed(object sender, object e)
            {
                popup.Closed -= Popup_Closed;
                popup.Child = null;
            }
        }


    }
}
