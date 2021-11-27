using DeThiHKI2021.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeThiHKI2021.Controllers
{
    public class NVBTController:Controller
    {
        public IActionResult ListThietBiTheoNhanVien(string MaNhanVien)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;
            return View(context.sqlListThietBiTheoNhanVien(MaNhanVien));
        }
        public IActionResult ViewThietBiBaoTri(string MaNV,string MaTB, string MaCH, int lan, string ngay)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;
            // NVBTModel nvbt = context.sqlViewThietBiBaoTri(MaNV, MaTB, MaCH, lan);
            NVBTModel nvbt = new NVBTModel();
            nvbt.MaNhanVien = MaNV;
            nvbt.MaThietBi = MaTB;
            nvbt.MaCanHo = MaCH;
            nvbt.LanThu = lan;
            nvbt.NgayBaoTri = Convert.ToDateTime(ngay);
            ViewData.Model = nvbt;
            return View();
        }

        /*[HttpPost]
        public string UpdateThietBiBaoTri(NVBTModel nvbt)
        {
            int count;
            DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;
            count = context.sqlInsertCanHo(nvbt);
            if (count == 1)
            {
                return "Thêm Thành Công!";
            }
            return "Thêm thất bại";
        }*/
    }
}
