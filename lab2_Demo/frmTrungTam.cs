using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2_Demo
{
    public partial class frmTrungTam : Form
    {
        public frmTrungTam()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReSet();
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
            int s = 0;
            if (cbTinHocA.Checked)
                s += int.Parse(lblTienTHA.Text.Split('.')[0]);
            if (cbTinHocB.Checked)
                s += int.Parse(lblTienTHB.Text.Split('.')[0]);
            if (cbTiengAnhA.Checked)
                s += int.Parse(lblTienTAA.Text.Split('.')[0]);
            if (cbTiengAnhB.Checked)
                s += int.Parse(lblTienTAB.Text.Split('.')[0]);
            this.txtTongTien.Text = s + ".000 đồng";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReSet()
        {
            cbbMaHV.Text = "";
            txtHoTen.Text = "";
            dtpNgayDangKy.Value = DateTime.Now;
            rdNam.Checked = true;
            cbTiengAnhA.Checked = false;
            cbTiengAnhB.Checked = false;
            cbTinHocA.Checked = false;
            txtTongTien.Text = "";
        }
    }
}
