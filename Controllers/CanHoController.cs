using DeThiHKI2021.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeThiHKI2021.Controllers
{
    public class CanHoController:Controller
    {
        public IActionResult ThemCanHo()
        {
            return View();
        }

        [HttpPost]
        public string AddCH(CanHoModel canho)
        {
            int count;
            DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;
            count = context.sqlInsertCanHo(canho);
            if (count == 1)
            {
                return "Thêm Thành Công!";
            }
            return "Thêm thất bại";
        }
    }
}
