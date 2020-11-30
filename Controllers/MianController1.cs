using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace The_Blob.Controllers
{
    public class MianController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
