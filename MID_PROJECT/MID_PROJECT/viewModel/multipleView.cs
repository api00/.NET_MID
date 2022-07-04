using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MID_PROJECT.Models.database;

namespace MID_PROJECT.viewModel
{
    public class multipleView
    {
        public IEnumerable<s_profile> prf { get; set; }
        public IEnumerable<blog>  blg { get; set; }
    }
}