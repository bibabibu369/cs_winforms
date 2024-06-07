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
            DateTime nSinh = dtpNS.Value;
            string mKhoa = cbKhoa.Text.Trim();
            string gioiTinh = rbtnNam.Checked ? "Nam" : (rbtnNu.Checked ? "Nữ" : null);

            // kiểm tra xem các biến đó có rỗng hay ko
            if (string.IsNullOrEmpty(maSv) || string.IsNullOrEmpty(tenSv) || string.IsNullOrEmpty(hoSv) || string.IsNullOrEmpty(mKhoa) || gioiTinh == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 

            // đặt vào try-catch
            try
            {
                // Thêm mới 1 kết nối
                using(conn = new SqlConnection(connectString))
                {
                    conn.Open();
                    // Tạo câu lệnh sql thêm
                    string sql = "INSERT INTO SINHVIEN ([Mã SV], [Họ SV], [Tên SV], [Ngày sinh], [Giới tính],[Mã Khoa]) VALUES (@maSv, @hoSv, @tenSv, @nSinh, @gioiTinh, @mKhoa)";

                    // Kết nối câu lệnh sql với chuỗi kết nối
                    using(cmd = new SqlCommand(sql, conn))
                    {
                        // Thêm biến tương ứng
                        cmd.Parameters.Add("@maSv", SqlDbType.NVarChar).Value = maSv;
                        cmd.Parameters.Add("@hoSv", SqlDbType.NVarChar).Value = hoSv;
                        cmd.Parameters.Add("@tenSv", SqlDbType.NVarChar).Value = tenSv;
                        cmd.Parameters.Add("@nSinh", SqlDbType.Date).Value = nSinh;
                        cmd.Parameters.Add("@gioiTinh", SqlDbType.NVarChar).Value = gioiTinh;
                        cmd.Parameters.Add("@mKhoa", SqlDbType.NVarChar).Value = mKhoa;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm mới sinh viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            rf();
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
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
