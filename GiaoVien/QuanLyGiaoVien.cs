using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiaoVien
{
    public delegate int SoSanh(object gv1, object gv2);

    enum KieuTim
    {
        TheoMa,
        TheoHoTen,
        TheoSDT
    }
    public class QuanLyGiaoVien
    {
        SoSanh soSanh;
        List<GiaoVien> dsGiaoVien;


        public QuanLyGiaoVien()
        {

            dsGiaoVien = new List<GiaoVien>();

        }

        public void Them(GiaoVien gv)
        {
            dsGiaoVien.Add(gv);
        }
        public bool ThemGiaoVien(GiaoVien gv)
        {
            bool kq = false;
            for (int i = 0; i < dsGiaoVien.Count - 1; i++)
            {
                if (soSanh(gv.MaSo, dsGiaoVien[i].MaSo) != 0)
                {
                    kq = true;
                    dsGiaoVien.Add(gv);
                }
            }
            return kq;
        }

        public GiaoVien Tim(object temp, SoSanh ss)
        {
            GiaoVien gv = null;
            foreach (GiaoVien giaoVien in dsGiaoVien)
            {
                if (ss(temp, giaoVien) == 0)
                {
                    gv = giaoVien;
                    break;
                }
            }
            return gv;
        }

        public void Xoa(object temp, SoSanh ss)
        {
            int i = dsGiaoVien.Count - 1;
            for (; i >= 0; i--)
            {
                //if (ss(temp, this[i]) == 0)
                //    this.dsGiaoVien.RemoveAt(i);
            }
        }

        public void DocTuFile()
        {
            string fileName = "DanhSachGV.txt", t;
            string[] s;
            GiaoVien gv;

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    while ((t = reader.ReadLine()) != null)
                    {
                        s = t.Split('*');
                        gv = new GiaoVien();
                        gv.MaSo = s[0];
                        gv.HoTen = s[1];
                        gv.NgaySinh = DateTime.Parse(s[2]);
                        gv.Mail = s[3];
                        gv.SoDT = s[4];
                        gv.GioiTinh = s[5];


                        string[] nn = s[6].Split(',');
                        foreach (string c in nn)
                        {
                            gv.NgoaiNgu.Concat(new string[] { c }).ToArray();
                        }
                        string[] s7 = s[7].Split(',');
                        ArrayList list = new ArrayList();
                        list.Add(s7[0]);
                        list.Add(s7[1]);

                        foreach (MonHoc monHoc in list)
                        {
                            gv.dsMonHoc.Them(monHoc);
                        }

                        this.dsGiaoVien.Add(gv);
                    }

                }
            }
       
        }

        public string TraVeGiaoVien()
        {
            string kq = "";
            foreach (var gv in dsGiaoVien)
            {
                if (gv.MaSo == "002")
                {
                    kq = "Mã số: " + gv.MaSo + '\n' + "Họ tên: " + gv.HoTen + '\n' +
                        "Ngày sinh: " + gv.NgaySinh + '\n' + "Giới tính: " + gv.GioiTinh +
                        '\n' + "Số đt:";
                }
            }
            return kq;
        }
    }
}
