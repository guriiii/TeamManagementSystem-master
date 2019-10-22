using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamManagementSystem.Bussiness.Interfaces;
using TeamManagementSystem.Bussiness.Logics;
using TeamManagementSystem.Bussiness.Props;

namespace TeamManagementSystem
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Login(object sender, EventArgs e)//This is login method
        {
            LoginProps viewProperty = new LoginProps();//Properties class instance
            ILoginLogics _loginService = new LoginLogics();//Service class instance
            Response.Cookies["UserName"].Value = Username.Text.Trim();
            Response.Cookies["Password"].Value = password.Text.Trim();
            viewProperty.Username = Username.Text.Trim();
            viewProperty.Password = password.Text.Trim();
            bool msg = _loginService.ValidateCredentials(viewProperty);
            if (msg)
            {

                Response.Redirect("Coach.aspx");
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Login ID and Password is invalid.";
            }
        }
    }
}