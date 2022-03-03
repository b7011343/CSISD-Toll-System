using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CSISD_Toll_Operator_Assignment.Controllers
{
    public class RoadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
