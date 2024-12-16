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
using System.Xml.Linq;


namespace De01
{
    public partial class FrmSinhVien : Form
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        string connString = @"Data Source=.;Initial Catalog=QuanLySV;Integrated Security=True";

        public FrmSinhVien()
        {
            InitializeComponent();
        }
        private void frmSinhvien_Load(object sender, EventArgs e)
        {
            LoadLop();
            LoadSinhVien();

        }
        private void LoadLop()
        {
            conn = new SqlConnection(connString);
            string query = "SELECT * FROM Lop";
            da = new SqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);

            cboLop.DataSource = dt;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
        }
        private void LoadSinhVien()
        {
            dgvSinhVien.Rows.Clear();

            conn = new SqlConnection(connString);
            string query = "SELECT * FROM Sinhvien";
            da = new SqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                int index = dgvSinhVien.Rows.Add();
                dgvSinhVien.Rows[index].Cells["MaSV"].Value = row["MaSV"].ToString();
                dgvSinhVien.Rows[index].Cells["HotenSV"].Value = row["HotenSV"].ToString();
                dgvSinhVien.Rows[index].Cells["NgaySinh"].Value = Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy");
                dgvSinhVien.Rows[index].Cells["MaLop"].Value = row["MaLop"].ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvSinhVien.SelectedRows[0];
                txtMaSV.Text = row.Cells["MaSV"].Value.ToString();
                txtHotenSV.Text = row.Cells["HotenSV"].Value.ToString();
                dtNgaysinh.Value = DateTime.Parse(row.Cells["NgaySinh"].Value.ToString());
                cboLop.SelectedValue = row.Cells["MaLop"].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                string maSV = dgvSinhVien.SelectedRows[0].Cells["MaSV"].Value.ToString();

                conn = new SqlConnection(connString);
                string query = "DELETE FROM Sinhvien WHERE MaSV = @MaSV";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Xóa thành công!");
                LoadSinhVien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtHotenSV.Text) || cboLop.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
               
                conn = new SqlConnection(connString);
                string query = "INSERT INTO Sinhvien (MaSV, HotenSV, NgaySinh, MaLop) VALUES (@MaSV, @HotenSV, @NgaySinh, @MaLop)";
                cmd = new SqlCommand(query, conn);

             
                cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text);
                cmd.Parameters.AddWithValue("@HotenSV", txtHotenSV.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", dtNgaysinh.Value);
                cmd.Parameters.AddWithValue("@MaLop", cboLop.SelectedValue);

                
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                LoadSinhVien();

                
                txtMaSV.Show();
                txtHotenSV.Show();
                dtNgaysinh.Value = DateTime.Now;
                cboLop.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi thêm sinh viên: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtHotenSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            try
            {
                conn = new SqlConnection(connString);
                string query = "INSERT INTO Sinhvien (MaSV, HotenSV, NgaySinh, MaLop) VALUES (@MaSV, @HotenSV, @NgaySinh, @MaLop)";
                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MaSV", txtMaSV.Text);
                cmd.Parameters.AddWithValue("@HotenSV", txtHotenSV.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", dtNgaysinh.Value);
                cmd.Parameters.AddWithValue("@MaLop", cboLop.SelectedValue);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Thêm thành công!");

                LoadSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnKhong_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string searchMSSV = txtMaSV.Text.Trim(); 

                if (string.IsNullOrWhiteSpace(searchMSSV))
                {
                    MessageBox.Show("Vui lòng nhập MSSV cần tìm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                conn = new SqlConnection(connString);
                string query = "SELECT * FROM Sinhvien WHERE MaSV = @MaSV"; 
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", searchMSSV);

             
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                   
                    DataRow row = dt.Rows[0];
                    txtMaSV.Text = row["MaSV"].ToString();
                    txtHotenSV.Text = row["HotenSV"].ToString();
                    dtNgaysinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                    cboLop.SelectedValue = row["MaLop"].ToString();

                    MessageBox.Show("Tìm thấy sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên với MSSV này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm sinh viên: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

       
