using Domain.Abstract;
using Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver
    {
        //IKernel kernel;

        //public NinjectDependencyResolver(IKernel kernel)
        //{
        //    this.kernel = kernel;
        //    AddBindings();
        //}

        //private void AddBindings()
        //{
            //Mock<IAreaRepository> mock = new Mock<IAreaRepository>();
            //mock.Setup(m => m.Areas).Returns(new List<Area>
            //{
            //    new Area { ContactaName = "Иванов Иван", MonthPrice=700, ContactaPhone1 = "0509773325"},
            //    new Area { ContactaName = "Петров Петр", MonthPrice=900, ContactaPhone1 = "0996786566"},
            //    new Area { ContactaName = "Николаев Николай", MonthPrice=1100, ContactaPhone1 = "0667897665"}
            //});
            //kernel.Bind<IAreaRepository>().ToConstant(mock.Object);





            //kernel.Bind<IBookRepository>().To<EFBookRepository>();

            //EmailSettings emailSettings = new EmailSettings
            //{
            //    WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            //};

            //kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
            //    .WithConstructorArgument("settings", emailSettings);
        //}

        //public object GetService(Type serviceType)
        //{
        //    return kernel.TryGet(serviceType);
        //}

        //public IEnumerable<object> GetServices(Type serviceType)
        //{
        //    return kernel.GetAll(serviceType);
        //}
    }
}