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
        public static List<Grids> grids;

        static HomeService()
        {
            grids = new List<Grids>();

            for (int i = 1; i <= 50; i++)
            {
                grids.Add
                    (
                        new Grids
                        {
                            No = i,
                            Type = "支出",
                            Date = DateTime.Now.ToString("yyyy-MM-dd"),
                            Price = 100 + i
                        }
                    );
            }
        }



    }
}
