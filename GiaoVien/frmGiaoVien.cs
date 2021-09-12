using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoVien
{
    public partial class frmGiaoVien : Form
    {
        QuanLyGiaoVien qlgv;
        public frmGiaoVien()
        {
            InitializeComponent();
            
        }

        private void frmGiaoVien_Load(object sender, EventArgs e)
        {
            string lienHe = "https://lms.dlu.edu.vn/my/";
            this.lblLienHe.Links.Add(0, lienHe.Length, lienHe);
            cbbMaSo.SelectedItem = this.cbbMaSo.Items[0];
            qlgv = new QuanLyGiaoVien();
            qlgv.DocTuFile();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            int i = lbDanhSachMH.SelectedItems.Count - 1;
            while (i >= 0)
            {
                this.lbMonHocDay.Items.Add(lbDanhSachMH.SelectedItems[i]);
                this.lbDanhSachMH.Items.Remove(lbDanhSachMH.SelectedItems[i]);
                i--;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int i = this.lbMonHocDay.SelectedItems.Count - 1;
            while (i >= 0)
            {
                this.lbDanhSachMH.Items.Add(lbMonHocDay.SelectedItems[i]);
                this.lbMonHocDay.Items.Remove(lbMonHocDay.SelectedItems[i]);
                i--;
            }

        }

        public GiaoVien GetGiaoVien()
        {
            string gt = "Nam";
            if (rdbNu.Checked)
            {
                gt = "Nữ";
            }

            GiaoVien gv = new GiaoVien();
            gv.MaSo = cbbMaSo.Text;
            gv.GioiTinh = gt;
            gv.HoTen = txtHoTen.Text;
            gv.NgaySinh = dtpNgaySinh.Value;
            gv.Mail = txtMail.Text;
            gv.SoDT = mtxtSoDT.Text;

            //Lấy thông tin ngoại ngữ
            string ngoaiNgu = "";
            for (int i = 0; i < clbNgoaiNgu.Items.Count-1; i++)
            {
                if (clbNgoaiNgu.GetItemChecked(i))
                    ngoaiNgu += clbNgoaiNgu.Items[i] + ";";

                gv.NgoaiNgu = ngoaiNgu.Split(';');

                //Lấy thông tin danh sách môn học
                DanhMucMonHoc mh = new DanhMucMonHoc();
                foreach (object ob in lbMonHocDay.Items)
                {
                    mh.Them(new MonHoc(ob.ToString()));
                }
                gv.dsMonHoc = mh;
              
            }
            return gv;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            frmTBGiaoVien frm = new frmTBGiaoVien();
            frm.SetText(GetGiaoVien().ToString());
            frm.ShowDialog();
        }

        public void Reset()
        {
            this.cbbMaSo.Text = "";
            this.txtHoTen.Text = "";
            this.txtMail.Text = "";
            this.mtxtSoDT.Text = "";
            this.rdbNam.Checked = true;

            //bỏ chọn trên clbNgoaiNgu
            for (int i = 0; i < clbNgoaiNgu.Items.Count-1; i++)
            {

                clbNgoaiNgu.SetItemChecked(i, false);
            }

            //chuyển các môn từ lbMonHocDay sang lbDanhMucMH
            foreach (object ob in lbMonHocDay.Items)
            {
                lbDanhSachMH.Items.Add(ob);
            }
            lbDanhSachMH.Items.Clear();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblLienHe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string s = e.Link.LinkData.ToString();
            Process.Start(s);
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            frmSearch search = new frmSearch();
            search.ShowDialog();
            
      
         
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            GiaoVien gv = GetGiaoVien();
            GiaoVien kq = qlgv.Tim(gv.MaSo, delegate (object obj1, object obj2)
            {
                return (obj2 as GiaoVien).MaSo.CompareTo(obj1.ToString());
            });
            if (kq != null)
                MessageBox.Show("Mã giáo viên đã tồn tại!", "Lỗi thêm dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.qlgv.Them(gv);
                MessageBox.Show("Thêm giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
