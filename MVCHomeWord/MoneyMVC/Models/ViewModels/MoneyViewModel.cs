using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMVC.Models.ViewModels
{
    public class MoneyViewModel
    {
        public int No { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public int Price { get; set; }
    }
}
