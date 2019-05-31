using PrismSample2019.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismSample2019.Core.Models
{
    /// <summary>
    /// 네비게이트 이벤트 아규먼트
    /// </summary>
    public class NavigateEventArgs
    {
        /// <summary>
        /// 네비게이트 이벤트 액션
        /// </summary>
        public NavigateEventAction NavigateEventAction { get; set; }

        /// <summary>
        /// 네비게이트 페이지명
        /// </summary>
        public string NavigatePageName { get; set; }

        /// <summary>
        /// 네비게이트 파라메터
        /// </summary>
        public object NavigateParameter { get; set; }

        /// <summary>
        /// 콜백
        /// </summary>
        public Action CallBack { get; set; }
    }
}
