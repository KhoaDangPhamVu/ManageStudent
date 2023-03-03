using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ManageStudent_20145393_PhamVuDangKhoa
{
    public partial class StudentListForm : Form
    {
        public StudentListForm()
        {
            InitializeComponent();
        }

        Student student = new Student();
        private void StudentListForm_Load(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student`");
            StudentListGridView.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            StudentListGridView.RowTemplate.Height = 80;
            StudentListGridView.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)StudentListGridView.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            StudentListGridView.AllowUserToAddRows = false;
           
        }

        private void StudentListGridView_DoubleClick(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm updateDeleteStdf = new UpdateDeleteStudentForm();
            updateDeleteStdf.textBoxID.Text = StudentListGridView.CurrentRow.Cells[0].Value.ToString();
            updateDeleteStdf.textBoxFirstName.Text = StudentListGridView.CurrentRow.Cells[1].Value.ToString();
            updateDeleteStdf.textBoxLastName.Text = StudentListGridView.CurrentRow.Cells[2].Value.ToString();
            updateDeleteStdf.dateTimePicker1.Value = (DateTime) StudentListGridView.CurrentRow.Cells[3].Value;
            //gender
            if (StudentListGridView.CurrentRow.Cells[4].Value.ToString() == "Female" ) {
                updateDeleteStdf.radioButtonFemale.Checked = true;

            }
            updateDeleteStdf.textBoxPhone.Text = StudentListGridView.CurrentRow.Cells[5].Value.ToString();
            updateDeleteStdf.textBoxAddress.Text = StudentListGridView.CurrentRow.Cells[6].Value.ToString();
            byte[] pic;
            pic = (byte[]) StudentListGridView.CurrentRow.Cells[7].Value;

            MemoryStream picture = new MemoryStream(pic);
            updateDeleteStdf.pictureBox.Image = Image.FromStream(picture);
            updateDeleteStdf.Show();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student`");
            StudentListGridView.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            StudentListGridView.RowTemplate.Height = 80;
            StudentListGridView.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)StudentListGridView.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            StudentListGridView.AllowUserToAddRows = false;
        }
    }
}
