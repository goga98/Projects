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
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeesRepository repository = new EmployeesRepository();
            try
            {
                repository.Insert(
                    new SqlParameter("FirstName", txtFirstName.Text),
                    new SqlParameter("LastName", txtLastName.Text),
                    new SqlParameter("Role", txtRole.Text),
                    new SqlParameter("Salary", txtSalary.Text)
                    );
                
                DialogResult = DialogResult.OK;
                MessageBox.Show("მონაცემები დაემატა");
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
