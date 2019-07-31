using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DemoInClass
{
    public partial class StudentInfoForm : Form
    {
        public StudentInfoForm()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Program.mainForm.Show();
            this.Hide();
        }

        private void StudentInfoForm_Activated(object sender, EventArgs e)
        {
            //open a reader
            try {
                using (StreamReader inputStream = new StreamReader(File.Open("Student.txt", FileMode.Open)))
                {
                    //read stuff from file into the Student object
                    Program.student.id = int.Parse(inputStream.ReadLine());
                    Program.student.StudentID = inputStream.ReadLine();
                    Program.student.FirstName = inputStream.ReadLine();
                    Program.student.LastName = inputStream.ReadLine();

                    //clean up
                    inputStream.Close();
                    inputStream.Dispose();

                    IdDataLabel.Text = Program.student.id.ToString();
                    StudentIdDataLabel.Text = Program.student.StudentID.ToString();
                    FNameDataLabel.Text = Program.student.FirstName.ToString();
                    LNameDataLabel.Text = Program.student.LastName.ToString();
                }
            }catch(IOException exception)
            {
                Debug.WriteLine("ERROR: " + exception.Message);
                MessageBox.Show("ERROR: " + exception.Message, "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            }

        private void StudentInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
