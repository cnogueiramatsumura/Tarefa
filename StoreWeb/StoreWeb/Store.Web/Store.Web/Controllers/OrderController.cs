using Store.Web.StoreWS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult import(HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("filerequired", "Importaçao do arquivo é obrigatoria");
                return View();
            }

            var binReader = new BinaryReader(file.InputStream);
            var binaryfile = binReader.ReadBytes(file.ContentLength);

            var Service = new Store.Web.StoreWS.ServiceSoapClient();
            var test = Service.Example(binaryfile).ToList();
            return View("ShowOrders", test);
        }       
    }
}