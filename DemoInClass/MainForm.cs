using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace DemoInClass
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// this is form closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
        }
        /// <summary>
        /// this is the event handler for the exitStripMenuItem Clcik event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.aboutForm.ShowDialog();
        }

        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            Program.aboutForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sectionCDatabaseDataSet.StudentTable' table. You can move, or remove it, as needed.
            this.studentTableTableAdapter.Fill(this.sectionCDatabaseDataSet.StudentTable);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var StudentList =
                from student in this.sectionCDatabaseDataSet.StudentTable
                select student;

            foreach (var student in StudentList.ToList())
            {
                Debug.WriteLine("Student ID: " + student.StudentID + "- Last name: " + student.LastName);
            }
        }



        private void StudentDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var rowIndex = StudentDataGridView.CurrentCell.RowIndex;
            var rows = StudentDataGridView.Rows;
            var columnCount = StudentDataGridView.ColumnCount;
            var cells = rows[rowIndex].Cells;
            rows[rowIndex].Selected = true;
            string outputString = string.Empty;
            for (int index = 0; index < columnCount; index++)
            {
                outputString += cells[index].Value.ToString() + " ";
            }
            SelectionLabel.Text = outputString;

            Program.student.id = int.Parse(cells[(int)StudentField.ID].Value.ToString());
            Program.student.StudentID = cells[(int)StudentField.STUDENT_ID].Value.ToString();
            Program.student.FirstName = cells[(int)StudentField.FIRST_NAME].Value.ToString();
            Program.student.LastName = cells[(int)StudentField.LAST_NAME].Value.ToString();

        }
        /// <summary>
        /// saving 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //configure the file dialog
            StudentSaveFileDialog.FileName = "Student.txt";
            StudentSaveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentSaveFileDialog.Filter = "Text File (*.txt)|*.txt| All File (*.*)|*.*";
            // open the file dialog
            var result = StudentSaveFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                using (StreamWriter outputString = new StreamWriter(File.Open(StudentSaveFileDialog.FileName, FileMode.Create)))
                {
                    outputString.WriteLine(Program.student.id);
                    outputString.WriteLine(Program.student.StudentID);
                    outputString.WriteLine(Program.student.FirstName);
                    outputString.WriteLine(Program.student.LastName);
                    //clean up
                    outputString.Close();
                    outputString.Dispose();
                    // give feedback to user that the file has been saved
                    // this is "modal" form
                    MessageBox.Show("File saved...", "Saving File...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
            //open a streamer to write

        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Program.studentinfoForm.Show();
            this.Hide();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ///configure the file dialog
            StudentopenFileDialog.FileName = "Student.txt";
            StudentopenFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentopenFileDialog.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";
            var result = StudentopenFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                //open a reader
                try
                {
                    using (StreamReader inputStream = new StreamReader(File.Open(StudentopenFileDialog.FileName, FileMode.Open)))
                    {
                        //read stuff from file into the Student object
                        Program.student.id = int.Parse(inputStream.ReadLine());
                        Program.student.StudentID = inputStream.ReadLine();
                        Program.student.FirstName = inputStream.ReadLine();
                        Program.student.LastName = inputStream.ReadLine();

                        //clean up
                        inputStream.Close();
                        inputStream.Dispose();
                    }
                    NextButton_Click(sender, e);
                }
                catch (IOException exception)
                {
                    Debug.WriteLine("ERROR: " + exception.Message);
                    MessageBox.Show("ERROR: " + exception.Message, "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void OpenBinaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///configure the file dialog
            StudentopenFileDialog.FileName = "Student.dat";
            StudentopenFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentopenFileDialog.Filter = "Text Files (*.dat|*.dat| All Files (*.*)|*.*";
            var result = StudentopenFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                //open a reader
                try
                {
                    using (BinaryReader inputStream = new BinaryReader(File.Open(StudentopenFileDialog.FileName, FileMode.Open)))
                    {
                        //read stuff from file into the Student object
                        Program.student.id = int.Parse(inputStream.ReadString());
                        Program.student.StudentID = inputStream.ReadString();
                        Program.student.FirstName = inputStream.ReadString();
                        Program.student.LastName = inputStream.ReadString();

                        //clean up
                        inputStream.Close();
                        inputStream.Dispose();
                    }
                    NextButton_Click(sender, e);
                }
                catch (IOException exception)
                {
                    Debug.WriteLine("ERROR: " + exception.Message);
                    MessageBox.Show("ERROR: " + exception.Message, "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(FormatException exception)
                {
                    Debug.WriteLine("ERROR..." + exception.Message);
                    MessageBox.Show("This is protected file", "Error...", MessageBoxButtons.OK);
                }
            }
        }

        private void SaveBinaryFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //configure the file dialog
            StudentSaveFileDialog.FileName = "Student.dat";
            StudentSaveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            StudentSaveFileDialog.Filter = "Binary File (*.dat)|*.dat| All File (*.*)|*.*";
            // open the file dialog
            var result = StudentSaveFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                using (BinaryWriter outputStream = new BinaryWriter(File.Open(StudentSaveFileDialog.FileName, FileMode.Create)))
                {
                    
                    outputStream.Write(Program.student.id);
                    outputStream.Write(Program.student.StudentID);
                    outputStream.Write(Program.student.FirstName);
                    outputStream.Write(Program.student.LastName);
                    //clean up
                    outputStream.Flush();
                    outputStream.Close();
                    outputStream.Dispose();
                    // give feedback to user that the file has been saved
                    // this is "modal" form
                    MessageBox.Show("Binary File saved...", "Saving Binary File...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
    

        }
    }
}
