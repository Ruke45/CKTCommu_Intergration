using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZkCommunicate_.Models;
using ZkCommunicate_.Device_;

namespace ZkCommunicate_.Controllers
{
    public class HomeController : Controller
    {
        Device_Management DManager = new Device_Management();


        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult ConnectToDevice(Connect_deviceModel Model)
        {
            string result = "Error";
            string Connect = DManager.Connect_Tcpip(Model.Device_IP, Model.Port);

            if (Connect == "Connected")
            {
                result = "Succes";
            }
            else
            {
                result = "Failed";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
