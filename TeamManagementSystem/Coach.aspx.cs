using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamManagementSystem.Bussiness.Interfaces;
using TeamManagementSystem.Bussiness.Logics;
using TeamManagementSystem.Bussiness.Props;
using TeamManagementSystem.Data;

namespace TeamManagementSystem
{
    public partial class Coach : Page
    {
        ICoachLogics _serviceCoach = new CoachLogics();//This is service instance of bussiness logics class
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)//THis is save button code
        {
            CoachProps pr = new CoachProps();//this is propeties class instance from propeties
            pr.CoachName = txtCoachName.Text.Trim();//here we are putting data in coach name field
            if (HiddenField1.Value != "")
                pr.ID = Convert.ToInt32(HiddenField1.Value);//here hidden field value for editing the record
            bool result = _serviceCoach.InsertUpdateCoachMaster(pr);//here we are calling insert update method for saving and updating the data
            if (result)
            {
                Response.Write("<script>alert('Record saved successfully')</script>");
            }
            bindGrid();
        }

        
        protected void bindGrid()//This is method for binding the grid view
        {
            ListToDatatable lsttodt = new ListToDatatable();
            var lst = _serviceCoach.GetCoachList();
            DataTable dt = lsttodt.ToDataTable(lst);
            if (dt != null && dt.Rows.Count > 0)
            {
                grd.DataSource = dt;
                grd.DataBind();
            }
            else
            {
                grd.DataBind();
            }
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)//row cammand for updating method
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string teacherid = this.grd.DataKeys[rowIndex]["ID"].ToString();

            if (e.CommandName == "updates")
            {
                ListToDatatable lsttodt = new ListToDatatable();
                var lst = _serviceCoach.GetCoachListByID(Convert.ToInt32(teacherid));
                DataTable dt = lsttodt.ToDataTable(lst);
                if (dt != null && dt.Rows.Count > 0)
                {
                    HiddenField1.Value = dt.Rows[0]["ID"].ToString();
                    txtCoachName.Text = dt.Rows[0]["CoachName"].ToString();
                    btnSave.Text = "Update";
                }
                else
                {
                    //do nothing
                    btnSave.Text = "Save";
                }
            }
            else
            {
                DataTable dt = new DataTable();
                bool result = _serviceCoach.DeleteCoach(Convert.ToInt32(teacherid));
                if (result)
                {
                    bindGrid();

                }
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)//Reset button
        {
            Response.Redirect("Coach.aspx");
        }
    }
}