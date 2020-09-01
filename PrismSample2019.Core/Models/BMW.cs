using PrismSample2019.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismSample2019.Core.Models
{
    public class BMW : ICar
    {
        private int _miles = 0;

        public int Run()
        {
            return ++_miles;
        }
    }
}
