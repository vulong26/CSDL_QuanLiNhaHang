using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKhachHang.DAO;
using QuanLyKhachHang.DTO;

namespace QuanLyKhachHang
{
    public partial class UC_KhachHang : UserControl
    {
        BindingSource khList = new BindingSource();
        public UC_KhachHang()
        {
            InitializeComponent();
            Load();
            disEnable(true);
        }

        void disEnable(bool e)
        {
            
            txbSuaTenKH.ReadOnly = e;
            txbSuaSDT.ReadOnly = e;
            btnThemKH.Enabled = e;
            btnSuaKH.Enabled = e;
            btnXoaKH.Enabled = e;
            btnLuuKH.Enabled = !e;
            btnHuyKH.Enabled = !e;
        }
        private void Load()
        {
            disEnable(true);
            dtgvKH.DataSource = khList;
            LoadListKH();
            AddKHBinding();
        }
        private void LoadListKH()
        {
            khList.DataSource = KhachHangDAO.Instance.loadKhachHangList();
        }
        void AddKHBinding()
        {
            txbSuaMaKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "MaKH", true, DataSourceUpdateMode.Never));
            txbSuaTenKH.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "TenKH", true, DataSourceUpdateMode.Never));
            txbSuaSDT.DataBindings.Add(new Binding("Text", dtgvKH.DataSource, "SDT", true, DataSourceUpdateMode.Never));
        }
        void xoa(string makh)
        {
            DataProvider.Instance.executeNonQuery("exec XoaKH @MAKH ", new object[] { makh} );
        }
        void themSua(string makh, string tenkh, string sdt)
        {
            DataProvider.Instance.executeNonQuery("exec ThemSuaKH @MAKH , @TENKH , @SDT ", new object[] { makh, tenkh, sdt });
        }

        List<KhachHang> SearchKhachHangByAll(string tenkh, string makh)
        {
            List<KhachHang> listKhachHang = KhachHangDAO.Instance.SearchKhachHangByAll(tenkh, makh);
            return listKhachHang;
        }
        List<KhachHang> SearchKhachHangByMaKH(string makh)
        {
            List<KhachHang> listKhachHang = KhachHangDAO.Instance.SearchKhachHangByMaKH(makh);
            return listKhachHang;
        }
        List<KhachHang> SearchKhachHangByTenKH(string tenkh)
        {
            List<KhachHang> listKhachHang = KhachHangDAO.Instance.SearchKhachHangByTenKH(tenkh);
            return listKhachHang;
        }
        private void btnThemKH_Click_1(object sender, EventArgs e)
        {
            disEnable(false);
            txbSuaMaKH.Text = DataProvider.Instance.executeScalar("select * from KHACHHANG").ToString();
            txbSuaTenKH.Text = "";
            txbSuaMaKH.Text = "";
        }
        private void btnSuaKH_Click_1(object sender, EventArgs e)
        {
            disEnable(false);
        }
        private void btnXoaKH_Click_1(object sender, EventArgs e)
        {
            string makh = txbSuaMaKH.Text;
            string message = "Bạn có chắc chắn muốn xóa khách hàng?";
            string caption = "Xác nhận xóa khách hàng";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.               
            }
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                xoa(makh);
            }
        }

        private void btnLuuKH_Click_1(object sender, EventArgs e)
        {
            disEnable(true);
            string makh = txbSuaMaKH.Text;
            string tenkh = txbSuaTenKH.Text;
            string sdt = txbSuaSDT.Text;
            themSua(makh, tenkh, sdt);
            MessageBox.Show("Lưu khách hàng thành công");
        }

        private void btnHuyKH_Click_1(object sender, EventArgs e)
        {
            disEnable(true);
        }

        private void dtgvKH_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            disEnable(true);
        }

        private void btnReload_Click_1(object sender, EventArgs e)
        {
            LoadListKH();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txbTimTenKH.Text == "")
            {
                khList.DataSource = SearchKhachHangByMaKH(txbTimMaKH.Text);

            }
            else if (txbTimMaKH.Text == "")
            {
                khList.DataSource = SearchKhachHangByTenKH(txbTimTenKH.Text);
            }
            else if (txbTimMaKH.Text == "" && txbTimTenKH.Text == "")
            {
                LoadListKH();
            }
            else
            {
                khList.DataSource = SearchKhachHangByAll(txbTimTenKH.Text, txbTimMaKH.Text);
            }
        }
    }
}

