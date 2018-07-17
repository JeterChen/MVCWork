using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoneyMVC.App_Start
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();

            //Controllers, 必須註冊Controller, 才能在Controller裡面做依賴注入
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            Assembly assembly = Assembly.Load("MoneyMVC");



            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();


            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
