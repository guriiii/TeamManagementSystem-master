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
    public partial class Player : Page
    {
        IPlayerLogics _servicePlayer = new PlayerLogics();//Service class instance
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGrid();
                TeamList();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)//This is add method
        {
            PlayerProps pr = new PlayerProps();//THis is properties class instance
            pr.Name = txtName.Text.Trim();
            pr.TeamID = Convert.ToInt32(TeamID.Text);
            pr.Position = Position.Text.Trim();
            pr.Gender = GenderList.Text;
            if (HiddenField1.Value != "")
                pr.ID = Convert.ToInt32(HiddenField1.Value);
            bool result = _servicePlayer.InsertUpdatePlayerMaster(pr);//THis is insert and update method calling
            if (result)
            {
                Response.Write("<script>alert('Record saved successfully')</script>");
                Response.Redirect("Player.aspx");
            }
            bindGrid();
        }


        protected void bindGrid()//This is bind grid method for binding the gridview
        {
            ListToDatatable lsttodt = new ListToDatatable();
            var lst = _servicePlayer.GetPlayerList();
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

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)//THis is row command class to performing the the functions in gridview
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            string teacherid = this.grd.DataKeys[rowIndex]["ID"].ToString();

            if (e.CommandName == "updates")
            {
                ListToDatatable lsttodt = new ListToDatatable();
                var lst = _servicePlayer.GetPlayerListByID(Convert.ToInt32(teacherid));
                DataTable dt = lsttodt.ToDataTable(lst);
                if (dt != null && dt.Rows.Count > 0)
                {
                    HiddenField1.Value = dt.Rows[0]["Id"].ToString();
                    TeamID.Text = dt.Rows[0]["TeamID"].ToString();
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    Position.Text = dt.Rows[0]["Position"].ToString();
                    GenderList.Text = dt.Rows[0]["Gender"].ToString();
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
                bool result = _servicePlayer.DeletePlayer(Convert.ToInt32(teacherid));
                if (result)
                {
                    bindGrid();

                }
            }
        }
        public DataTable ToDataTable<T>(List<T> items)//THis is datatable class to set vaue in datatable
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
        protected void TeamList()//This is teamlist method for binding the teams in dropdown
        {
            ITeamLogics coachLogics = new TeamLogics();
            var lst = coachLogics.GetTeamList().Select(x => new { x.TeamName, x.ID }).ToList();
            DataTable dt = ToDataTable(lst);
            if (dt != null && dt.Rows.Count > 0)
            {
                TeamID.DataSource = dt;
                TeamID.DataTextField = "TeamName";
                TeamID.DataValueField = "ID";
                TeamID.DataBind();

            }
            else
            {
                TeamID.DataBind();
            }
        }
        protected void BtnReset_Click(object sender, EventArgs e)//Reset button code
        {
            Response.Redirect("Player.aspx");
        }
    }
}