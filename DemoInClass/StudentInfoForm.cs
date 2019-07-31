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

            IdDataLabel.Text = Program.student.id.ToString();
            StudentIdDataLabel.Text = Program.student.StudentID.ToString();
            FNameDataLabel.Text = Program.student.FirstName.ToString();
            LNameDataLabel.Text = Program.student.LastName.ToString();
        }


        private void StudentInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
