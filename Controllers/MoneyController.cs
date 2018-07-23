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
using System.Web.Mvc.Html;

namespace MoneyMVC.Controllers
{
    public class MoneyController : Controller
    {
        private readonly MoneyService _imoneyService;
        private readonly UnitOfWork _unitOfWork;

        public MoneyController()
        {
            _unitOfWork = new UnitOfWork();
            _imoneyService = new MoneyService(_unitOfWork);
        }

        // GET: Money
        public ActionResult Index()
        {
            ViewData["CategoryItems"] = GetCategoryList();
            ViewData["PageItems"] = GetPageDropDownList();
            ViewData["currentPage"] = 1;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Type,Date,Price,Description")]
                                   MoneyViewModel model)
        {

            ViewData["CategoryItems"] = GetCategoryList();
            ViewData["PageItems"] = GetPageDropDownList();
            /***不知道為什麼model裡面的Type都是None???***/
            if (ModelState.IsValid)
            {
                _imoneyService.Add(model);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(model);
        }

       // [ChildActionOnly]
        public ActionResult ListAction(int page)
        {
            int pagesize = 10;
            var result = _imoneyService.LookupByPageList(page, pagesize);

            ViewData["currentPage"] = page;

            return View(result);
        }

        private SelectList GetPageDropDownList()
        {
            int pagesize = 10;
            var sources = _imoneyService.LookupAllData();

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

        private IList<SelectListItem> GetCategoryList()
        {
            return EnumHelper.GetSelectList(typeof(CategoryType));
        }

    }
}