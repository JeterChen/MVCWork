using System.ComponentModel.DataAnnotations;

namespace MoneyMVC.Enums
{
    public enum CategoryType
    {
        [Display(Name = "請選擇")]
        None = 0,

       [Display(Name = "支出")]
        Expenditure = 1,

        [Display(Name = "收入")]
        Income = 2,

    }
}
