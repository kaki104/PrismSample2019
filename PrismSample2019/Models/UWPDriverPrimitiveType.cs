using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PrismSample2019.Models
{
    public class UWPDriverPrimitiveType : IDriver
    {
        private readonly ICar _car;
        private readonly string _driverName;

        public UWPDriverPrimitiveType(ICar car, string driverName)
        {
            _car = car;
            _driverName = driverName;
        }
        public async Task RunCarAsync()
        {
            var message = $"{_driverName} is running {_car?.GetType().Name} - {_car?.Run()} Mile";
            var dialog = new MessageDialog(message);
            _ = await dialog.ShowAsync();
        }
    }
}
