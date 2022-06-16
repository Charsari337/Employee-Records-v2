using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace EmployeeRecords
{
    public partial class mainForm : Form
    {
        List<Employee> employeeDB = new List<Employee>();

        public mainForm()
        {
            InitializeComponent();
            loadDB();
        }

        private void addButton_Click(object sender, EventArgs e)
        { 
            //Get all your inputs
            string id = idInput.Text;
            string firstName = fnInput.Text;
            string lastName = lnInput.Text;
            string date = dateInput.Text;
            string salary = salaryInput.Text;

            //create an employee object with the input values
            Employee employee = new Employee(id, firstName, lastName, date, salary);
            employeeDB.Add(employee);

            ClearLabels();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
           
        }

        private void listButton_Click(object sender, EventArgs e)
        {
            outputLabel.Text = "";

            foreach(Employee emp in employeeDB)
            {
                outputLabel.Text += $"{emp.id} {emp.firstName} {emp.lastName} {emp.date} {emp.salary} \n";
            }
        }

        private void ClearLabels()
        {
            idInput.Text = "";
            fnInput.Text = "";
            lnInput.Text = "";
            dateInput.Text = "";
            salaryInput.Text = "";
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveDB();
        }

        public void loadDB()
        {
            XmlReader reader = XmlReader.Create("Resources/employeeData.xml");

            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Text)
                {
                    string id = reader.ReadString();

                    reader.ReadToNextSibling("firstname");
                    string firstName = reader.ReadString();

                    reader.ReadToNextSibling("lastname");
                    string lastName = reader.ReadString();

                    reader.ReadToNextSibling("date");
                    string date = reader.ReadString();

                    reader.ReadToNextSibling("pay");
                    string salary = reader.ReadString();

                    Employee employee = new Employee(id, firstName, lastName, date, salary);
                    employeeDB.Add(employee);
                }
            }

            reader.Close();
        }

        public void saveDB()
        {
            XmlWriter writer = XmlWriter.Create("Resources/employeeData.xml", null);

            writer.WriteStartElement("Employees");

            foreach(Employee emp in employeeDB)
            {
                writer.WriteStartElement("Employee");

                writer.WriteElementString("id", emp.id);
                writer.WriteElementString("firstname", emp.firstName);
                writer.WriteElementString("lastname", emp.lastName);
                writer.WriteElementString("date", emp.date);
                writer.WriteElementString("pay", emp.salary);

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Close();
        }
    }
}
