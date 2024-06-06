using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connectString = "Data Source=.\\SQLEXPRESS;Initial Catalog=SINHVIEN;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection conn;
        SqlCommand cmd;

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectString);
            conn.Open();

            string sql = "SELECT * FROM SINHVIEN";
            SqlDataAdapter daSinhvien = new SqlDataAdapter(sql, conn);
            DataTable dtSinhvien = new DataTable();
            daSinhvien.Fill(dtSinhvien);
            dgvSinhvien.DataSource = dtSinhvien;

            cbKhoa.Items.Add("TOAN");
            cbKhoa.Items.Add("HOAH");
            cbKhoa.Items.Add("DIAL");
            cbKhoa.Items.Add("CNTT");

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // tạo các biến tương ứng
            string maSv = tbMSV.Text.Trim();
            string tenSv = tbTen.Text.Trim();
            string hoSv = tbHo.Text.Trim();
            string nSinh = dtpNS.Text.Trim();
            string gTinhNam;
            string gTinhNu;

            // kiểm tra xem các biến đó có rỗng hay ko

            // đặt vào try-catch
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvSinhvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhvien.Rows[e.RowIndex];

                tbHo.Text = row.Cells["Họ SV"].Value.ToString().Trim();
                tbTen.Text = row.Cells["Tên SV"].Value.ToString().Trim();
                tbMSV.Text = row.Cells["Mã SV"].Value.ToString().Trim();
                dtpNS.Text = row.Cells["Ngày sinh"].Value.ToString().Trim();

                string gioiTinh = row.Cells["Giới Tính"].Value.ToString().Trim();
                if (gioiTinh == "Nam") rbtnNam.Checked = true;
                else if (gioiTinh == "Nữ") rbtnNu.Checked = true;
                cbKhoa.Text = row.Cells["Mã Khoa"].Value.ToString().Trim();
            }
        }

        public void rf()
        {
            try
            {
                using (conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM SINHVIEN";
                    SqlDataAdapter daSinhvien = new SqlDataAdapter(sql, conn);
                    DataTable dtSinhvien = new DataTable();
                    daSinhvien.Fill(dtSinhvien);
                    dgvSinhvien.DataSource = dtSinhvien;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Loi: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            rf();
        }
    }
}
