using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chiayin_Yang_MidTerm
{
    public partial class Validate : Form
    {
        public Validate()
        {
            InitializeComponent();
        }
        private void Validate_Load(object sender, EventArgs e)
        {
            txtInput.Select();
        }

        // Q1
        private void btnValidate1_Click(object sender, EventArgs e)
        {
            string testString = txtInput.Text;
            string pattern1 = @"^\d{2}-\d{3}-\d{3}-\d{4}$";

            if (Regex.IsMatch(testString, pattern1))
            {
                richTextBox1.Text = "The input matches the pattern ##-###-###-####.";
                richTextBox1.ForeColor = Color.Green;
                txtInput.ForeColor = Color.Green;
            }
            else
            {
                richTextBox1.Text = "The input does not match the pattern ##-###-###-####.";
                richTextBox1.ForeColor = Color.Red;
                txtInput.ForeColor = Color.Red;
            }

        }

        private void btnValidate2_Click(object sender, EventArgs e)
        {
            string testString = txtInput.Text;
            string pattern2 = @"^\d{2}\(\d{3}\)\d{3}-\d{4}$";

            if (Regex.IsMatch(testString, pattern2))
            {
                richTextBox1.Text = "The input matches the pattern ##(###)###-####.";
                richTextBox1.ForeColor = Color.Green;
                txtInput.ForeColor = Color.Green;
            }
            else
            {
                richTextBox1.Text = "The input does not match the pattern ##(###)###-####.";
                richTextBox1.ForeColor = Color.Red;
                txtInput.ForeColor = Color.Red;
            }
        }
        // Q3
        private void btnPalindrome_Click(object sender, EventArgs e)
        {
            string input = txtPalindrome.Text.Trim();
            richTextBox1.Text = "";

            if (IsValidNumber(input, out string errorMessage))
            {
                if (IsPalindrome(input))
                {
                    richTextBox1.Text = "The number is a palindrome.";
                    richTextBox1.ForeColor = Color.Green;
                    txtPalindrome.ForeColor = Color.Green;
                }
                else
                {
                    richTextBox1.Text = "The number is not a palindrome.";
                    richTextBox1.ForeColor = Color.Red;
                    txtPalindrome.ForeColor = Color.Red;
                }
            }
            else
            {
                richTextBox1.Text = errorMessage;
                richTextBox1.ForeColor = Color.Red;
                txtPalindrome.ForeColor = Color.Red;
            }
        }
        private bool IsValidNumber(string input, out string errorMessage)
        {
            errorMessage = "";

            if (input.Length < 3 || input.Length > 9)
            {
                errorMessage = "The number must be between 3 and 9 digits long.";
                return false;
            }

            if (!long.TryParse(input, out long number))
            {
                errorMessage = "The input must be a valid non-negative number.";
                return false;
            }

            if (number < 0)
            {
                errorMessage = "The number cannot be negative.";
                return false;
            }

            return true;
        }

        private bool IsPalindrome(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            string reversedInput = new string(charArray);

            return input == reversedInput;
        }

        // Q4
        private int[] studentIDs = new int[5];
        private string[] names = new string[5];
        private int[] marks = new int[5];
        private int studentCount = 0;

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;

            string errorMessages = ValidateInputs();
            if (errorMessages != string.Empty)
            {
                richTextBox1.Text = errorMessages;
                return;
            }

            if (studentCount < 5)
            {
                int studentID = int.Parse(txtStudentID.Text);
                string name = txtName.Text;
                int mark = int.Parse(txtMark.Text);

                studentIDs[studentCount] = studentID;
                names[studentCount] = name;
                marks[studentCount] = mark;

                studentCount++;
                ClearInputs();
                richTextBox1.Text = "Student added successfully.";
                richTextBox1.ForeColor = Color.Green;

            }
            else
            {
                richTextBox1.Text = "Maximum number of students reached.";
                richTextBox1.ForeColor = Color.Red;
            }

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            for (int i = 0; i < studentCount; i++)
            {
                richTextBox1.ForeColor = Color.Black;
                richTextBox1.AppendText($"{studentIDs[i]}, {names[i]}, {marks[i]}\r\n");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;

            if (!int.TryParse(txtStudentID.Text, out int studentID) || studentID <= 0)
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Please enter a valid Student ID.";
                return;
            }

            int index = Array.IndexOf(studentIDs, studentID);
            if (index >= 0 && index < studentCount)
            {
                richTextBox1.ForeColor = Color.Black;
                richTextBox1.Text = $"Student ID: {studentID}, Name: {names[index]}, Mark: {marks[index]}";
            }
            else
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Student ID not found.";
            }

        }

        private void btnUpdateRecord_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;

            if (!int.TryParse(txtStudentID.Text, out int studentID) || studentID <= 0)
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Please enter a valid Student ID.";
                return;
            }

            if (!int.TryParse(txtMark.Text, out int newMark) || newMark < 0 || newMark > 100)
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Please enter a valid Mark (0-100).";
                return;
            }

            int index = Array.IndexOf(studentIDs, studentID);
            if (index >= 0 && index < studentCount)
            {
                richTextBox1.ForeColor = Color.Green;
                marks[index] = newMark;
                richTextBox1.Text = "Mark updated successfully.";
            }
            else
            {
                richTextBox1.ForeColor = Color.Red;
                richTextBox1.Text = "Student ID not found.";
            }
        }
        private string ValidateInputs()
        {
            string errors = string.Empty;

            if (!int.TryParse(txtStudentID.Text, out int studentID) || studentID <= 0 || studentID.ToString().Length > 5 || studentIDs.Contains(studentID))
            {
                richTextBox1.ForeColor = Color.Red;
                txtStudentID.BackColor = Color.Red;
                errors += "- Invalid or duplicate Student ID (must be unique, numeric, and 1-5 digits).\r\n";
            }
            else
            {
                txtStudentID.BackColor = Color.White;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) || txtName.Text.Length > 20)
            {
                richTextBox1.ForeColor = Color.Red;
                txtName.BackColor = Color.Red;
                errors += "- Name must be 1-20 characters and not just spaces.\r\n";
            }
            else
            {
                txtName.BackColor = Color.White;
            }

            if (!int.TryParse(txtMark.Text, out int mark) || mark < 0 || mark > 100)
            {
                richTextBox1.ForeColor = Color.Red;
                txtMark.BackColor = Color.Red;
                errors += "- Mark must be numeric and between 0 and 100.\r\n";
            }
            else
            {
                txtMark.BackColor = Color.White;
            }

            return errors;
        }

        private void ClearInputs()
        {
            txtStudentID.Clear();
            txtName.Clear();
            txtMark.Clear();
        }
    }
}
