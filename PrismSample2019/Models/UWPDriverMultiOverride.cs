using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PrismSample2019.Models
{
    public class UWPDriverMultiOverride : IDriver
    {
        private readonly ICar _car1;
        private readonly ICarKey _carKey1;
        private readonly ICar _car2;
        private readonly ICarKey _carKey2;

        public UWPDriverMultiOverride(ICar car1, ICarKey carKey1, ICar car2, ICarKey carKey2)
        {
            _car1 = car1;
            _carKey1 = carKey1;
            _car2 = car2;
            _carKey2 = carKey2;
        }

        public async Task RunCarAsync()
        {
            var message = $"Running {_car1?.GetType().Name} with {_carKey1.GetType().Name} - {_car1?.Run()} Mile";
            message += $"\n\rRunning {_car2?.GetType().Name} with {_carKey2.GetType().Name} - {_car2?.Run()} Mile";
            var dialog = new MessageDialog(message);
            _ = await dialog.ShowAsync();
        }
    }
}
