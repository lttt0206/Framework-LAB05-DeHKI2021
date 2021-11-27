using DeThiHKI2021.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeThiHKI2021.Controllers
{
    public class NhanVienController: Controller
    {
        public IActionResult LieTKeNhanVienTheoSoLan()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ListByTime(int solan)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;
            return View(context.sqlListByTimeNhanVien(solan));
        }

        public IActionResult ListNhanVien()
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;
            return View(context.sqlListNhanVien());
        }
    }
}
