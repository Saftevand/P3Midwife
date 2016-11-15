using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P3_Midwife
{
    public partial class LoginForm : Form
    {
        private List<Employee> _accounts;

        public LoginForm(List<Employee> accounts)
        {
            InitializeComponent();
            this._accounts = accounts;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            bool LoginVerified = false;
            LoginVerified = VerifyLogin();
            if (LoginVerified == true)
            {
                //Start form for det pågældende login(Muligvis med gemt session)
            }
        }

        private bool VerifyLogin()
        {
            if(_accounts.Contains(new Employee()))
            {
                return true;
            } 
            else return false;
        }
    }
}
