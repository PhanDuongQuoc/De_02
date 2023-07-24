using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace De02
{
    public partial class Form1 : Form
    {

        Model1 context = new Model1();
        List<Sanpham> listsanpham;
        public Form1()
        {
            InitializeComponent();
        }


        public void listloaisp(List<LoaiSP> listloaisanpham) 
        {
            cbloaisp.DataSource= listloaisanpham;
            cbloaisp.DisplayMember = "maloai";
            cbloaisp.ValueMember = "tenloai";
        }

        public void danhsachsanpham(List<Sanpham> listsanpham) 
        {
            listView1.Items.Clear();
            foreach(Sanpham sp in listsanpham) 
            {
                ListViewItem lv = new ListViewItem(sp.masp);
                lv.SubItems.Add(sp.tensp);
                lv.SubItems.Add(sp.ngaynhap.ToString());
                lv.SubItems.Add(sp.maloai);
                listView1.Items.Add(lv);
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            listsanpham=context.Sanphams.ToList();
            List<LoaiSP>listloai=context.LoaiSPs.ToList();

            danhsachsanpham(listsanpham);
            listloaisp(listloai);
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            try
            {
                string masanpham = txtmasp.Text;
                Sanpham them = context.Sanphams.FirstOrDefault(s => s.masp == masanpham);

                if (them == null)
                {
                    Sanpham s = new Sanpham()
                    {
                        masp = txtmasp.Text,
                        tensp = txttensp.Text,
                        ngaynhap = DateTime.Parse(dtpngaynhap.Text),
                        maloai =cbloaisp.Text,
                    };
                    context.Sanphams.Add(s);
                    context.SaveChanges();
                  danhsachsanpham( context.Sanphams.ToList());
                    MessageBox.Show("thêm dữ liệu thành công !");
                }
                else
                {
                    MessageBox.Show("dữ liệu đã tồn tại !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:" + ex.Message);
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                try
                {
                    string masp = listView1.SelectedItems[0].Text;
                    Sanpham sanPham = context.Sanphams.FirstOrDefault(sp => sp.masp == masp);

                    if (sanPham != null)
                    {
                 
                        sanPham.tensp = txttensp.Text;
                        sanPham.ngaynhap = dtpngaynhap.Value;
                        sanPham.maloai = cbloaisp.Text;

                     
                        context.SaveChanges();

                    
                      
                        danhsachsanpham(context.Sanphams.ToList());

                        MessageBox.Show("Cập nhật thông tin sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Sản phẩm không tồn tại trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để chỉnh sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    

    public void timkiem()
        {
            string tenspham = txttensp.Text;


            List<Sanpham> timkiem = context.Sanphams.Where(s => s.tensp == tenspham).ToList();
            listView1.Items.Clear();
           

            
                foreach (var sp in timkiem)
                {
                    ListViewItem lv = new ListViewItem(sp.masp);
                    lv.SubItems.Add(sp.tensp);
                    lv.SubItems.Add(sp.ngaynhap.ToString());
                    lv.SubItems.Add(sp.maloai);
                    listView1.Items.Add(lv);
                }

          
         
        }


        private void btnxoa_Click(object sender, EventArgs e)
        {
            try
            {
                string masanpham = txtmasp.Text;
                Sanpham delete = context.Sanphams.FirstOrDefault(s => s.masp == masanpham);

                if (delete != null)
                {
                   context.Sanphams.Remove(delete);
                    context.SaveChanges();
                    danhsachsanpham(context.Sanphams.ToList());
                    MessageBox.Show("xóa thành công !");
                }
                else
                {
                    MessageBox.Show("dữ liệu không tồn tại !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:" + ex.Message);
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            timkiem();
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            
        }

        private void btnkluu_Click(object sender, EventArgs e)
        {
           
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult ketqua = MessageBox.Show("bạn có muốn thoát không !", "Thông báo ", MessageBoxButtons.OKCancel);
            if (ketqua == DialogResult.OK) 
            {
                this.Close();
            }
        }
    }
}

