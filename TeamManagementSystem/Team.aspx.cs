using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamManagementSystem.Bussiness.Interfaces;
using TeamManagementSystem.Bussiness.Logics;
using TeamManagementSystem.Bussiness.Props;

namespace TeamManagementSystem
{
    public partial class Team : Page
    {
        ITeamLogics _serviceCoach = new TeamLogics();//This is service class instance
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGrid();
                CoachList();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)//this is save method coding
        {
            TeamProps pr = new TeamProps();//Property class instance
            pr.TeamName = txtTeamName.Text.Trim();
            pr.CoachID = Convert.ToInt32(CoachID.Text);
            pr.State = State.Text.Trim();
            pr.City = City.Text.Trim();
            if (HiddenField1.Value != "")
                pr.ID = Convert.ToInt32(HiddenField1.Value);
            bool result = _serviceCoach.InsertUpdateTeamMaster(pr);//Add update method calling
            if (result)
            {
                Response.Write("<script>alert('Record saved successfully')</script>");
                Response.Redirect("Team.aspx");
            }
            bindGrid();
        }


        protected void bindGrid()//For binding the gridview
        {
            ListToDatatable lsttodt = new ListToDatatable();
            var lst = _serviceCoach.GetTeamList();
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

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)//Row command erforming the functionalities in gridview like update and delete
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string teacherid = this.grd.DataKeys[rowIndex]["ID"].ToString();

            if (e.CommandName == "updates")
            {
                ListToDatatable lsttodt = new ListToDatatable();
                var lst = _serviceCoach.GetTeamListByID(Convert.ToInt32(teacherid));
                DataTable dt = lsttodt.ToDataTable(lst);
                if (dt != null && dt.Rows.Count > 0)
                {
                    HiddenField1.Value = dt.Rows[0]["Id"].ToString();
                    CoachID.Text = dt.Rows[0]["CoachID"].ToString();
                    txtTeamName.Text = dt.Rows[0]["TeamName"].ToString();
                    State.Text = dt.Rows[0]["State"].ToString();
                    City.Text = dt.Rows[0]["City"].ToString();
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
                bool result = _serviceCoach.DeleteTeam(Convert.ToInt32(teacherid));
                if (result)
                {
                    bindGrid();

                }
            }
        }
        public DataTable ToDataTable<T>(List<T> items)//Datatable converting the values list to table
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        protected void CoachList()//Binding coach dropdown here
        {
            ICoachLogics coachLogics = new CoachLogics();
            var lst = coachLogics.GetCoachList().Select(x => new { x.CoachName, x.ID }).ToList();
            DataTable dt = ToDataTable(lst);
            if (dt != null && dt.Rows.Count > 0)
            {
                CoachID.DataSource = dt;
                CoachID.DataTextField = "CoachName";
                CoachID.DataValueField = "ID";
                CoachID.DataBind();

            }
            else
            {
                CoachID.DataBind();
            }
        }
        protected void BtnReset_Click(object sender, EventArgs e)//Reset button code
        {
            Response.Redirect("Team.aspx");
        }
    }
}