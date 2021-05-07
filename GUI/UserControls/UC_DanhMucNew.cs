using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKhachHang.DAO;
using QuanLyKhachHang.DTO;
using QuanLyKhachHang.GUI;

namespace QuanLyKhachHang.GUI.UserControls.DanhMuc
{
    public partial class UC_DanhMucNew : UserControl
    {
        string thread;
        public UC_DanhMucNew()
        {
            InitializeComponent();
            loadMonAn();
            DisEnbBtn(false);
        }

        private void btnMon_Click(object sender, EventArgs e)
        {
            pageDanhMuc.SelectTab(0);
            clearAllBindings();
            loadMonAn();
        }

        private void btnLoai_Click(object sender, EventArgs e)
        {
            pageDanhMuc.SelectTab(1);
            clearAllBindings();
            loadLoaiMon();
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            pageDanhMuc.SelectTab(2);
            bunifuCustomDataGridBanAn.DataSource = (new BanAnDAO()).GetListBanAn();
            Load_BanAN();
            MacDinhBanAn();
        }

 

        void clearAllBindings()
        {
            ClearAllBindingsLoaiMon();
            ClearAllBindingsMonAn();
        }

        #region Món ăn
        void loadMonAn()
        {
            ClearAllBindingsMonAn();
            loadMonAnList();
            monAnBinding();
            loadLoaiMonComboBox();

        }
        void loadMonAnList()
        {
            dtgvMonAn.DataSource = MonAnDAO.Instance.getMonAnList();
        }

        void loadLoaiMonComboBox()
        {
            List<LoaiMon> list = LoaiMonDAO.Instance.getListLoaiMon();
            cbbLoaiMon.DataSource = list;
            cbbLoaiMon.DisplayMember = "Tenloaimon";

        }

        void monAnBinding()
        {
            txtMaMon.DataBindings.Add(new Binding("text", dtgvMonAn.DataSource, "mama"));
            txtTenMon.DataBindings.Add(new Binding("text", dtgvMonAn.DataSource, "tenmonan"));
            txtDVT.DataBindings.Add(new Binding("text", dtgvMonAn.DataSource, "dvt"));
            txtDonGia.DataBindings.Add(new Binding("text", dtgvMonAn.DataSource, "dongia"));
            //cbbLoaiMon.DataBindings.Add(new Binding("Text", dtgvMonAn.DataSource, "Tenloaimon"));
        }
        void ClearAllBindingsMonAn()
        {
            foreach (Control c in grbChinhSuaMon.Controls)
                c.DataBindings.Clear();
        }
        private void btnThemMA_Click_1(object sender, EventArgs e)
        {

            thread = txtMaMon.Text;
            DisEnbBtn(true);
            txtMaMon.Text = DataProvider.Instance.executeScalar("select dbo.TAOMAMA()").ToString();
            txtTenMon.Text = "";
            txtDVT.Text = "";
            txtDonGia.Text = "";
        }

        private void btnSuaMA_Click_1(object sender, EventArgs e)
        {
            thread = txtMaMon.Text;
            DisEnbBtn(true);
        }

        private void btnXoaMA_Click_1(object sender, EventArgs e)
        {
            string MaMa = txtMaMon.Text;
            if (MessageBox.Show("Bạn có thực sự muốn xoá món này?", "Xác nhận", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (MonAnDAO.Instance.DeleteFood(MaMa))
                {
                    MessageBox.Show("Xoá món thành công");

                }
                else
                {
                    MessageBox.Show("Có lỗi khi xoá món");
                }
                loadMonAn();
            }
        }

        private void bunifuButton11_Click_1(object sender, EventArgs e)
        {
            string MaMa = txtMaMon.Text;
            string TenMa = txtTenMon.Text;
            string DVT = txtDVT.Text;
            int Dongia = Convert.ToInt32(txtDonGia.Text);
            string MaLM = (cbbLoaiMon.SelectedItem as LoaiMon).Malm;
            if (string.Compare(thread, txtMaMon.Text, true) != 0)
            {
                if (MonAnDAO.Instance.InsertFood(MaMa, TenMa, DVT, Dongia, MaLM))
                {
                    MessageBox.Show("Thêm món thành công");
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm món");
                }
            }
            else
            {
                if (MonAnDAO.Instance.UpdateFood(MaMa, TenMa, DVT, Dongia, MaLM))
                {
                    MessageBox.Show("Sửa món thành công");
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm món");
                }
            }
            loadMonAn();
            DisEnbBtn(false);
        }
        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            DisEnbBtn(false);
        }
        private void bunifuTextBox9_TextChange(object sender, EventArgs e)
        {
            string tenmonan = bunifuTextBox9.Text;
            string mama = bunifuTextBox9.Text;
            dtgvMonAn.DataSource = MonAnDAO.Instance.SearchFood(tenmonan);
        }

        #endregion


        #region Loại món

        void loadLoaiMon()
        {
            ClearAllBindingsLoaiMon();
            loadLoaiMonList();
            loaiMonBinding();

        }
        void loadLoaiMonList()
        {
            dtgvLoaiMon.DataSource = LoaiMonDAO.Instance.getListLoaiMon();
        }
        void loaiMonBinding()
        {
            txtMaLoaiMon.DataBindings.Add(new Binding("Text", dtgvLoaiMon.DataSource, "Malm"));
            txtTenLoaiMon.DataBindings.Add(new Binding("text", dtgvLoaiMon.DataSource, "Tenloaimon"));
        }
        void ClearAllBindingsLoaiMon()
        {
            foreach (Control c in grbChinhSuaLoai.Controls)
                c.DataBindings.Clear();
        }
        void DisEnbBtn(bool e)
        {
            bunifuButton10.Enabled = e;
            bunifuButton11.Enabled = e;
            bunifuButton12.Enabled = e;
            bunifuButton13.Enabled = e;
            btnThemLoaiMon.Enabled = !e;
            btnThemMA.Enabled = !e;
            btnSuaLoaiMon.Enabled = !e;
            btnSuaMA.Enabled = !e;
            btnXoaMA.Enabled = !e;
            btnXoaLoaiMon.Enabled = !e;
        }
        private void btnThemLoaiMon_Click_1(object sender, EventArgs e)
        {
            thread = txtMaLoaiMon.Text;
            DisEnbBtn(true);
            txtMaLoaiMon.Text = DataProvider.Instance.executeScalar("select dbo.TAOMALOAIMON()").ToString();
            txtTenLoaiMon.Text = "";
        }

        private void btnSuaLoaiMon_Click_1(object sender, EventArgs e)
        {
            thread = txtMaLoaiMon.Text;
            DisEnbBtn(true);
        }

        private void btnXoaLoaiMon_Click_1(object sender, EventArgs e)
        {
            string MaLM = txtMaLoaiMon.Text;
            string TenLM = txtTenLoaiMon.Text;
            if (MessageBox.Show("Bạn có thực sự muốn xoá loại món này?", "Xác nhận", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (LoaiMonDAO.Instance.DeleteFoodCategory(MaLM, TenLM))
                {
                    MessageBox.Show("Xoá loại món thành công");

                }
                else
                {
                    MessageBox.Show("Có lỗi khi xoá loại món");
                }
                loadLoaiMon();
            }
        }

        private void bunifuButton13_Click_1(object sender, EventArgs e)
        {
            string MaLM = txtMaLoaiMon.Text;
            string TenLM = txtTenLoaiMon.Text;
            if (string.Compare(thread, txtMaLoaiMon.Text, true) != 0)
            {
                if (LoaiMonDAO.Instance.InsertFoodCategory(MaLM, TenLM))
                {
                    MessageBox.Show("Thêm loại món thành công");

                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm loại món");
                }
            }
            else
            {
                if (LoaiMonDAO.Instance.UpdateFoodCategory(MaLM, TenLM))
                {
                    MessageBox.Show("Sửa loại món thành công");

                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm loại món");
                }
            }


            loadLoaiMon();
            DisEnbBtn(false);
        }

        private void bunifuButton12_Click_1(object sender, EventArgs e)
        {
            DisEnbBtn(false);
        }

        private void bunifuTextBox6_TextChange_1(object sender, EventArgs e)
        {
            string tenlm = bunifuTextBox6.Text;
            dtgvLoaiMon.DataSource = LoaiMonDAO.Instance.SearchFoodCategory(tenlm);
 
        }
        #endregion





        #region Bàn ăn
        readonly BanAnDTO bandto = new BanAnDTO();
        readonly BanAnDAO bandao = new BanAnDAO();
        public void MacDinhBanAn()
        {
            bunifuTextBoxmabanan.Enabled = false;
            bunifuTextBoxsochongoi.Enabled = false;
            BtnThemBanAn.Enabled = true;
            BtnSuaBanAn.Enabled = true;
            BtnXoaBanAn.Enabled = true;
            BtnLuuBanAn.Enabled = false;
            BtnHuyBanAn.Enabled = false;
        }
        public void SuaBanAn()
        {
            bunifuTextBoxsochongoi.Enabled = true;
            bunifuTextBoxmabanan.Enabled = true;
            BtnThemBanAn.Enabled = false;
            BtnSuaBanAn.Enabled = true;
            BtnXoaBanAn.Enabled = false;
            BtnLuuBanAn.Enabled = true;
            BtnHuyBanAn.Enabled = true;
        }

        private void Load_BanAN()
        {

            //bunifuCustomDataGridBanAn.Columns["MABAN"].HeaderText = "Mã Bàn Ăn";
            //bunifuCustomDataGridBanAn.Columns["SOCHONGOI"].HeaderText = "Số Ngồi Tối Đa";
            //bunifuCustomDataGridBanAn.Columns["MAPYC"].HeaderText = "Mã Phiếu Yêu Cầu";
            //bunifuCustomDataGridBanAn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //bunifuCustomDataGridBanAn.Columns["MABANAN"].Width = 100;
            //bunifuCustomDataGridBanAn.Columns["SOCHONGOI"].Width = 200;
            //bunifuCustomDataGridBanAn.Columns["MAPYC"].Width = 200;
            //bunifuCustomDataGridBanAn.DataSource = bandao.GetListBanAn();
        }
        public bool CheckThemBA()
        {

            if (bunifuTextBoxmabanan.Text.Trim().Equals(""))
            {
                MessageBox.Show("Lỗi", "Bạn chưa nhập số bàn!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBoxmabanan.Focus();
                return false;
            }

            if (bunifuTextBoxsochongoi.Text.Trim().Equals(""))
            {
                MessageBox.Show("Lỗi", "Bạn chưa nhập số chỗ ngồi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bunifuTextBoxsochongoi.Focus();
                return false;
            }
            return true;
        }
        public void Load_Lai_BanAn()
        {
            bunifuCustomDataGridBanAn.DataSource = (bandao.GetListBanAn());
        }


        private void BunifuCustomDataGridBanAn_SelectionChanged(object sender, EventArgs e)
        {
            int index = bunifuCustomDataGridBanAn.CurrentCell == null ? -1 : bunifuCustomDataGridBanAn.CurrentCell.RowIndex;
            if (index != -1)
            {
                bunifuTextBoxmabanan.Text = bunifuCustomDataGridBanAn.Rows[bunifuCustomDataGridBanAn.CurrentRow.Index].Cells[0].Value.ToString().Trim();
                bunifuTextBoxsochongoi.Text = bunifuCustomDataGridBanAn.Rows[bunifuCustomDataGridBanAn.CurrentRow.Index].Cells[2].Value.ToString().Trim();
            }
        }
        private void BtnThemBanAn_Click(object sender, EventArgs e)
        {

            Add_BanAn BanAn = new Add_BanAn();
            BanAn.ShowDialog();
            Load_Lai_BanAn();
        }
        private void BtnSuaBanAn_Click(object sender, EventArgs e)
        {
            SuaBanAn();
        }
        private void BunifuButtonLuuBanAn_Click(object sender, EventArgs e)
        {
            MacDinhBanAn();
            if (CheckThemBA())
            {
                bandto.MABAN = bunifuTextBoxmabanan.Text.ToString().Trim();
                bandto.SOCHONGOI = int.Parse(bunifuTextBoxsochongoi.Text.ToString().Trim());
                bandto.MAPYC = "";
                bool suaban = bandao.Update_ban(bandto);
                if (suaban == true)
                {
                    DialogResult result = MessageBox.Show("Thành công", "Chỉnh sửa", MessageBoxButtons.OK);
                    Load_Lai_BanAn();
                }
            }
        }
        private void BtnXoaBanAn_Click(object sender, EventArgs e)
        {
            bandto.MABAN = bunifuTextBoxmabanan.Text.ToString().Trim();
            bandto.SOCHONGOI = int.Parse(bunifuTextBoxsochongoi.Text.ToString().Trim());
            DialogResult x = MessageBox.Show("Xóa", "Bạn có chắc chắn muốn xóa bàn số " + bandto.MABAN + " chứ ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);  
            bool xoaban = bandao.XoaBA(bandto);
            if(xoaban == true && x == DialogResult.Yes)
            {
                DialogResult result = MessageBox.Show("Thành công", "Xóa", MessageBoxButtons.OK);
                Load_Lai_BanAn();
            }    

        }

        private void BtnHuyBanAn_Click(object sender, EventArgs e)
        {
            Load_Lai_BanAn();
            MacDinhBanAn();
        }





        #endregion

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
