using MoneyMVC.Enums;
using MoneyMVC.Extensions;
using MoneyMVC.Models.ViewModels;
using MoneyMVC.Repository;
using MoneyMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyMVC.Controllers
{
    public class MoneyController : Controller
    {
        private readonly MoneyService _ImoneyService;
        private readonly UnitOfWork _unitOfWork;

        public MoneyController()
        {
            _unitOfWork = new UnitOfWork();
            _ImoneyService = new MoneyService(_unitOfWork);
        }

        // GET: Money
        public ActionResult Index()
        {

            var categoryResource = GetCategoryList();
            var pageDropDownList = GetPageDropDownList();

            ViewData["CategoryItems"] = categoryResource;
            ViewData["PageItems"] = pageDropDownList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Type,Date,Price,Description")]
                                   MoneyViewModel model)
        {


            /***不知道為什麼model裡面的Type都是None???***/
            if (ModelState.IsValid)
            {
                _ImoneyService.Add(model);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(model);
        }

       // [ChildActionOnly]
        public ActionResult ListAction(int page)
        {
            int pagesize = 10;
            var result = _ImoneyService.LookupByPageList(page, pagesize);

            return View(result);
        }

        private SelectList GetPageDropDownList()
        {
            int pagesize = 10;
            var sources = _ImoneyService.LookupAllData();

            var pageResult = sources.Select((item, inx) => new { item, inx })
                                 .GroupBy(x => x.inx / pagesize)
                                 .Select(g => g.Select(s => s.item));

            var _resources = pageResult.Select((p,inx) => new PageDropDownListViewModel
            {
                name = inx + 1,
                value = inx
            });


            return new SelectList(_resources,"value","name",0);


        }

        private SelectList GetCategoryList()
        {
            Dictionary<string, int?> _resuouces = new Dictionary<string, int?>
            {
                [CategoryType.None.GetDisplayName()] = null,
                [CategoryType.Expenditure.GetDisplayName()] = 0,
                [CategoryType.Income.GetDisplayName()] = 1
            };

            var categoryListItems = _resuouces.Select((k) =>
                  new MoneyCategoryViewModel
                  {
                      name = k.Key,
                      value = k.Value
                  }
            );

            return new SelectList(categoryListItems, "value", "name", 1); ;
        }

    }
}