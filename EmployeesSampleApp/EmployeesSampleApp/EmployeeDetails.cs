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
using Employee.Repository;
using DataBaseHelper;

namespace EmployeesSampleApp
{
    public partial class EmployeeDetails : Form
    {
        public int RecordID { get; private set; }
        public EmployeeDetails(int recordID=0)
        {
            InitializeComponent();
            RecordID = recordID;
        }
        private void EmployeeDetails_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EmployeesRepository repository = new EmployeesRepository();
            try
            {
                repository.Delete(new SqlParameter("FirstName", txtFirstName.Text),
                    new SqlParameter("LastName", txtLastName.Text),
                    new SqlParameter("Role", txtRole.Text),
                    new SqlParameter("Salary", txtSalary.Text)
                    );
                DialogResult = DialogResult.OK;

                MessageBox.Show("მონაცემები წაშლილია");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            EmployeesRepository repository = new EmployeesRepository();
            try
            {
                repository.Update(new SqlParameter("EmployeeID", RecordID),
                new SqlParameter("FirstName", txtFirstName.Text),
                new SqlParameter("LastName", txtLastName.Text),
                new SqlParameter("Role", txtRole.Text),
                new SqlParameter("Salary", txtSalary.Text)
                    );
                DialogResult = DialogResult.OK;
                MessageBox.Show("მონაცემები შეცვლილია");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
