using QuanLyKhachHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachHang.DAO
{

    class KhachHangDAO
    {
        private static KhachHangDAO instance;

        internal static KhachHangDAO Instance
        {
            get { if (instance == null) instance = new KhachHangDAO(); return instance; }
            private set { instance = value; }
        }

        private KhachHangDAO() { }
            
        public List<KhachHang> loadKhachHangList()
        {
            List<KhachHang> tableList = new List<KhachHang>();
            DataTable data = DataProvider.Instance.executeQuery("select * from KHACHHANG");
            foreach (DataRow item in data.Rows)
            {
                KhachHang table = new KhachHang(item);
                tableList.Add(table);
            }
            return tableList;
        }
        //Tim kiem
        public List<KhachHang> SearchKhachHangByAll(string tenkh, string makh)
        {
            List<KhachHang> list = new List<KhachHang>();
            string query = string.Format("select * from KHACHHANG where [dbo].[GetUnsignString](TENKH) like N'%'" +
                "+[dbo].[GetUnsignString](N'{0}')+'%'and[dbo].[GetUnsignString](MAKH) " +
                "like N'%' +[dbo].[GetUnsignString](N'{1}') + '%'", tenkh, makh);
            DataTable data = DataProvider.Instance.executeQuery(query);

            foreach (DataRow item in data.Rows)
            {
                KhachHang KhachHang = new KhachHang(item);
                list.Add(KhachHang);
            }
            return list;
        }

        public List<KhachHang> SearchKhachHangByMaKH(string makh)
        {
            List<KhachHang> list = new List<KhachHang>();
            string query = string.Format("select * from KHACHHANG where [dbo].[GetUnsignString](MAKH) " +
                "like N'%'+[dbo].[GetUnsignString](N'{0}')+'%' ", makh);
            DataTable data = DataProvider.Instance.executeQuery(query);

            foreach (DataRow item in data.Rows)
            {
                KhachHang KhachHang = new KhachHang(item);
                list.Add(KhachHang);
            }
            return list;
        }
        public List<KhachHang> SearchKhachHangByTenKH(string tenkh)
        {
            List<KhachHang> list = new List<KhachHang>();
            string query = string.Format("select * from KHACHHANG where [dbo].[GetUnsignString](TENKH) " +
                "like N'%'+[dbo].[GetUnsignString](N'{0}')+'%' ", tenkh);
            DataTable data = DataProvider.Instance.executeQuery(query);

            foreach (DataRow item in data.Rows)
            {
                KhachHang KhachHang = new KhachHang(item);
                list.Add(KhachHang);
            }
            return list;
        }
        //
        public KhachHang timKHTheoMa(string makh)
        {

            DataTable table = DataProvider.Instance.executeQuery("select * from KHACHHANG where makh = '" + makh+"'");
            KhachHang kh = new KhachHang(table.Rows[0]);
            return kh;
        }
        public int kiemTraKH(string makh)
        {
            int data = (int)DataProvider.Instance.executeScalar("select count(*) from KHACHHANG where makh = '" + makh + "'");
            return data;
        }

    }
}
