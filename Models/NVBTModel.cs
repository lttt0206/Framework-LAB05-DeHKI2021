using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeThiHKI2021.Models
{
    public class NVBTModel
    {
        public string MaNhanVien { get; set; }
        public string MaThietBi { get; set; }
        public string MaCanHo { get; set; }
        public int LanThu { get; set; }        
        public DateTime NgayBaoTri { get; set; }
        
    }
}
