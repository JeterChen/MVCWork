﻿using MoneyMVC.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyMVC.Models.ViewModels
{
    public class MoneyViewModel
    {
        [Required(ErrorMessage = "請選擇類別")]
        public CategoryType Type { get; set; }

        [Required(ErrorMessage = "請輸入日期")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "請填入金額")]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int Price { get; set; }

        [Required(ErrorMessage = "請輸入備註")]
        public string Description { get; set; }
    }
}
