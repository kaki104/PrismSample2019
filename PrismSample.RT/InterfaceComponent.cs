using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace PrismSample.RT
{
    [AllowForWeb]
    public sealed class InterfaceComponent
    {
        private string _answer;

        public string GetAnswer()
        {
            //return "The answer is 42.";
            var answer = _answer ?? "null";
            return $"The answer is {answer}";
        }

        public void SetAnswer(string answer)
        {
            _answer = answer;
        }
    }
}
