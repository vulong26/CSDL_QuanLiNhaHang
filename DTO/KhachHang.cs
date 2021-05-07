using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachHang.DTO
{
    public class KhachHang
    {
        private string maKH;
        private string tenKH;
        private string sDT;

        public string MaKH { get => maKH; set => maKH = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string SDT { get => sDT; set => sDT = value; }

        public KhachHang(string makh, string tenkh, string sdt)
        {
            MaKH = makh;
            TenKH = tenkh;
            SDT = sdt;
        }
        public KhachHang (DataRow row)
        {
            MaKH = row["makh"].ToString();
            TenKH = row["tenkh"].ToString();
            SDT = row["sdt"].ToString();
        }

    }
}
