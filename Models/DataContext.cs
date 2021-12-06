using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DeThiHKI2021.Models
{
    public class DataContext
    {
        public string ConnectionString { get; set; } // Biến thành viên

        public DataContext(string connectionstring)
        {
            this.ConnectionString = connectionstring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /* -----------------------------
         *  SQL TABLE CANHO
         *--------------------------------*/
        public int sqlInsertCanHo(CanHoModel canho)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into canho values(@macanho, @tencanho)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("macanho", canho.MaCanHo);
                cmd.Parameters.AddWithValue("tencanho", canho.TenChuHo);
                return (cmd.ExecuteNonQuery());
            }
        }

        /* -----------------------------
         *  SQL TABLE QUANLYNHANVIEN
         *--------------------------------*/
        public List<NhanVienModel> sqlListNhanVien()
        {
            List<NhanVienModel> list = new List<NhanVienModel>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = @"SELECT * FROM NHANVIEN";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NhanVienModel()
                        {
                            MaNhanVien = reader["manhanvien"].ToString(),
                            TenNhanVien = reader["tennhanvien"].ToString(),
                            SoDienThoai = reader["sodienthoai"].ToString(),
                            GioiTinh = Convert.ToBoolean(reader["gioitinh"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<object> sqlListByTimeNhanVien(int soLan)
        {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = @"select nv.manhanvien, nv.sodienthoai, count(*) AS SoLan
                                from nhanvien nv join nv_bt on nv.manhanvien = nv_bt.manhanvien 
                                group by nv.manhanvien, nv.sodienthoai
                                having count(*) >= @SoLanInput";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("SoLanInput", soLan);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaNV = reader["manhanvien"].ToString(),
                            SoDT = reader["sodienthoai"].ToString(),
                            SoLan = Convert.ToInt32(reader["SoLan"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        public List<NVBTModel> sqlListThietBiTheoNhanVien(string maNV)
        {
            List<NVBTModel> list = new List<NVBTModel>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = @"select * 
                                from NV_BT
                                where MaNhanVien=@maNhanVien";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("maNhanVien", maNV);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NVBTModel()
                        {
                            MaNhanVien=maNV,
                            MaThietBi=reader["mathietbi"].ToString(),
                            MaCanHo = reader["macanho"].ToString(),
                            LanThu = Convert.ToInt32(reader["lanthu"]),
                            NgayBaoTri=Convert.ToDateTime(reader["ngaybaotri"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        //------------------------------------------------
        public NVBTModel sqlViewThietBiBaoTri(string maNV,string maTB, string maCH, int lanThu)
        {
            NVBTModel nvbt = new NVBTModel();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select * 
                            from NV_BT
                            where MaNhanVien=@maNhanVien and MaThietBi=@maThietBi and MaCanHo=@maCanHo and LanThu=@lan";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("maNhanVien", maNV);
                cmd.Parameters.AddWithValue("maThietBi", maTB);
                cmd.Parameters.AddWithValue("maCanHo", maCH);
                cmd.Parameters.AddWithValue("lan", lanThu);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    nvbt.MaNhanVien = reader["manhanvien"].ToString();
                    nvbt.MaThietBi = reader["mathietbi"].ToString();
                    nvbt.MaCanHo = reader["macanho"].ToString();
                    nvbt.LanThu = Convert.ToInt32(reader["lanthu"]);
                    nvbt.NgayBaoTri = Convert.ToDateTime(reader["ngaybaotri"]);
                    reader.Close();
                }
                conn.Close();
            }
            return nvbt;
        }
        public int sqlDeleteNVBT(string maNV, string maTB, string maCH, int lanThu)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"delete NV_BT where MaNhanVien=@maNhanVien and MaThietBi=@maThietBi and MaCanHo=@maCanHo and LanThu=@lan";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("maNhanVien", maNV);
                cmd.Parameters.AddWithValue("maThietBi", maTB);
                cmd.Parameters.AddWithValue("maCanHo", maCH);
                cmd.Parameters.AddWithValue("lan", lanThu);
                return (cmd.ExecuteNonQuery());
            }
        }
        public NVBTModel viewTBBT(string maNV, string maTB, string maCH, int lanThu, DateTime ngaybt)
        {
            NVBTModel nvbt = new NVBTModel();
            nvbt.MaNhanVien = maNV;
            nvbt.MaThietBi = maTB;
            nvbt.MaCanHo = maCH;
            nvbt.LanThu = lanThu;
            nvbt.NgayBaoTri = ngaybt;
            return nvbt;
        }
        public int sqlUpdateNVBT(string maNVkey, string maTBkey, string maCHkey, int lanthukey, NVBTModel nvbt)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"update NV_BT 
                            set MaThietBi=@maThietBi, MaCanHo=@maCanHo, LanThu=@lan, NgayBaoTri=@ngaybt 
                            where MaNhanVien=@maNVkey and MaThietBi=@maTBkey and MaCanHo=@maCHkey and LanThu=@lanthukey";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("maNVkey", maNVkey);
                cmd.Parameters.AddWithValue("maTBkey", maTBkey);
                cmd.Parameters.AddWithValue("maCHkey", maCHkey);
                cmd.Parameters.AddWithValue("lanthukey", lanthukey);
                cmd.Parameters.AddWithValue("maThietBi", nvbt.MaThietBi);
                cmd.Parameters.AddWithValue("maCanHo", nvbt.MaCanHo);
                cmd.Parameters.AddWithValue("lan", nvbt.LanThu);
                cmd.Parameters.AddWithValue("ngaybt", nvbt.NgayBaoTri.ToString("yyyy-MM-dd"));
                return (cmd.ExecuteNonQuery());
            }
        }
    }
}
