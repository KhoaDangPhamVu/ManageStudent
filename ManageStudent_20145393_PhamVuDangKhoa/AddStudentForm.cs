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

namespace ManageStudent_20145393_PhamVuDangKhoa
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(opf.FileName);
            }
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            Student stu = new Student();
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
            if((thisYear - bornYear) < 18 || (thisYear - bornYear) > 100)
            {
                MessageBox.Show("The Student Age Must be between 18 and 100 ", "Invalid BirthDate", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if (verify())
            {
                pictureBox.Image.Save(pic, pictureBox.Image.RawFormat);
                if (stu.insertStudent(fname, lname, bdate, phone, gender, address, pic))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Fiels", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        //function to verify data input
        bool verify()
        {
            if((textBoxFirstName.Text.Trim() == "") || (textBoxLastName.Text.Trim() == "") || (textBoxPhone.Text.Trim() == "") || (textBoxAddress.Text.Trim() == "") || (pictureBox.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonCancelStudent_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
