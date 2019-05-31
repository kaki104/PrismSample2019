using System;
using System.Collections.Generic;
using System.Text;

namespace PrismSample2019.Core.Models
{
    /// <summary>
    /// 자바스크립트 실행 이벤트 아규먼트
    /// </summary>
    public class RunScriptEventArgs
    {
        /// <summary>
        /// 스크립트 문자열
        /// </summary>
        public IList<string> Scripts { get; set; }

    }
}
