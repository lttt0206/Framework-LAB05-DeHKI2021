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
            //DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;
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

        [HttpPost]
        public IActionResult UpdateThietBiBaoTri(string maNV,NVBTModel nvbt)
        {
            int count;
            DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;
            nvbt.MaNhanVien = maNV;
            count = context.sqlUpdateNVBT(nvbt);
            if (count > 0)
            {
                ViewData["thongbao"] = "Update thành công";
            } else ViewData["thongbao"] = "Update không thành công";
            return View();
        }
        public IActionResult DeleteThietBiBaoTri(string MaNV, string MaTB, string MaCH, int lan, string ngay)
        {
            int count;
            DataContext context = HttpContext.RequestServices.GetService(typeof(DeThiHKI2021.Models.DataContext)) as DataContext;            
            count = context.sqlDeleteNVBT(MaNV, MaTB, MaCH, lan);
            if (count > 0)
            {
                ViewData["thongbao"] = "Delete thành công";
            }
            else ViewData["thongbao"] = "Delete không thành công";
            return View();
        }
    }
}
