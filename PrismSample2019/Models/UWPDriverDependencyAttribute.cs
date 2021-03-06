﻿using Microsoft.Practices.Unity;
using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PrismSample2019.Models
{
    public class UWPDriverDependencyAttribute : IDriver
    {
        public UWPDriverDependencyAttribute()
        {
        }

        [Dependency]
        public ICar Car { get; set; }

        public async Task RunCarAsync()
        {
            var message = $"Running {Car?.GetType().Name} - {Car?.Run()} Mile";
            var dialog = new MessageDialog(message);
            _ = await dialog.ShowAsync();
        }
    }
}
