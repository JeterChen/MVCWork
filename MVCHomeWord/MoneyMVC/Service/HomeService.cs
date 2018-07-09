using MoneyMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMVC.Service
{
    public class HomeService
    {

        public IEnumerable<MoneyViewModel> LookupAllData()
        {
            for (int i = 1; i <= 50; i++)
            {

                yield return new MoneyViewModel
                {
                    No    = i,
                    Type  = "支出",
                    Date  = DateTime.Now,
                    Price = 100 + i
                };
            }
        }



    }
}
