using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageStudent_20145393_PhamVuDangKhoa
{
    public partial class UpdateDeleteStudentForm : Form
    {
        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
        }
        Student stu = new Student();
        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(opf.FileName);
            }
        }

        bool verify()
        {
            if ((textBoxFirstName.Text.Trim() == "") || (textBoxLastName.Text.Trim() == "") || (textBoxPhone.Text.Trim() == "") || (textBoxAddress.Text.Trim() == "") || (pictureBox.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                string fname = textBoxFirstName.Text;
                string lname = textBoxLastName.Text;
                DateTime bdate = dateTimePicker1.Value;
                string phone = textBoxPhone.Text;
                string address = textBoxAddress.Text;
                string gender = "Male";
                if (radioButtonFemale.Checked)
                {
                    gender = "Female";
                }

                MemoryStream pic = new MemoryStream();

                int bornYear = dateTimePicker1.Value.Year;
                int thisYear = DateTime.Now.Year;
                if ((thisYear - bornYear) < 18 || (thisYear - bornYear) > 100)
                {
                    MessageBox.Show("The Student Age Must be between 18 and 100 ", "Invalid BirthDate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (verify())
                {
                    pictureBox.Image.Save(pic, pictureBox.Image.RawFormat);
                    if (stu.updateStudent(id, fname, lname, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Student Information Updated", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Empty Fiels", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch  {
                MessageBox.Show("Please Enter a valid StudentID", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void buttonCancelStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                if (MessageBox.Show("Are you sure to delete this student", "Delete Sttudent", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (stu.deleteStudent(id))
                    {
                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxID.Text = "";
                        textBoxFirstName.Text = "";
                        textBoxLastName.Text = "";
                        textBoxPhone.Text = "";
                        textBoxAddress.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBox.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Student NO Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please Enter a valid StudentID", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                MySqlCommand command = new MySqlCommand("SELECT `id`, `first_name`, `last_name`, `birthdate`, `gender`, `phone`, `address`, `picture` FROM `student` WHERE 'id'=" + id);
                DataTable table = stu.getStudents(command);

                if (table.Rows.Count > 0)
                {
                    textBoxFirstName.Text = table.Rows[0]["first_name"].ToString();
                    textBoxLastName.Text = table.Rows[0]["last_name"].ToString();
                    textBoxPhone.Text = table.Rows[0]["phone"].ToString();
                    textBoxAddress.Text = table.Rows[0]["address"].ToString();

                    dateTimePicker1.Value = (DateTime)table.Rows[0]["birthdate"];
                    if (table.Rows[0]["g"].ToString() == "Female")
                    {
                        radioButtonFemale.Checked = true;

                    }
                    else
                    {
                        radioButtonMale.Checked = true;
                    }

                    //image
                    byte[] pic = (byte[])table.Rows[0]["picture"];
                    MemoryStream picture = new MemoryStream(pic);
                    pictureBox.Image = Image.FromStream(picture);
                }
                
            }catch  { 
            MessageBox.Show("Enter a valid StudentID","Invalid ID",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
        }

        private void pictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        //allow only number on key
        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
