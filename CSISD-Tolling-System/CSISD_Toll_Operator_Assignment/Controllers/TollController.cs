using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class TollController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
