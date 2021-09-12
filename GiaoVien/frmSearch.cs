using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoVien
{
    public partial class frmSearch : Form
    {
        frmGiaoVien frmGiaoVien;
        QuanLyGiaoVien ql;
        public frmSearch()
        {
            InitializeComponent();
        }

        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GiaoVien gv = frmGiaoVien.GetGiaoVien();
            GiaoVien kq = ql.Tim(txtSearch.Text, delegate (object o1, object o2)
            {
                return (o2 as GiaoVien).MaSo.CompareTo(o1.ToString());
            });

            if (kq == null)
            {
                MessageBox.Show("Không tìm thấy thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                frmTBGiaoVien frm = new frmTBGiaoVien();
                frm.SetText(frmGiaoVien.GetGiaoVien().ToString());
                frm.ShowDialog();
            }
           
            
        }

        private void rdbMaGV_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Mã GV";
        }

        private void rdbHoTen_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Họ Tên GV";
        }

        private void rdbSDT_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = " Số điện thoại GV";
        }
    }
}
