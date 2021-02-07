using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Markisa.Models
{
    public class ConfirmEmailViewModel
    {
        public bool Success { get; set; }
        public string SuccessActionLink { get; set; }
        public string FailedActionLink { get; set; }
    }
}
