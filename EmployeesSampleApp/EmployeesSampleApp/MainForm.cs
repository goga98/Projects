using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseHelper;
using Employee.Repository;
using System.Data.SqlClient;
using PagedList;

namespace EmployeesSampleApp
{
    public partial class MainForm : Form
    {     
        Database db;
        EmployeeDetails details;
        SqlDataAdapter da;
        DataSet ds;
        public MainForm()
        {
            InitializeComponent();
            DisplayData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {        
            // TODO: This line of code loads data into the 'employeeSampleAppDataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.employeeSampleAppDataSet.Employees);   
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddForm form = new AddForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
            }
        }

        public void DisplayData()
        {
            Database db = new Database();
            dataGridView1.DataSource = db.GetTable("select * from employees");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            details = new EmployeeDetails();
            details.txtFirstName.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            details.txtLastName.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            details.txtRole.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            details.txtSalary.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            details.ShowDialog();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            db = new Database();
            dataGridView1.DataSource = db.GetTable("select * from employees where firstname like'" + comboBox1.Text + "%'");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            db = new Database();
            dataGridView1.DataSource = db.GetTable("select * from employees where lastname like'" + comboBox2.Text + "%'");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            db = new Database();
            dataGridView1.DataSource = db.GetTable("select * from employees where role like'" + comboBox3.Text + "%'");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        { 
            db = new Database();
            dataGridView1.DataSource = db.GetTable("select * from employees where salary like'" + comboBox4.Text + "%'");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
    }
}
