using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coursework1
{
    public partial class Department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT Department_ID as ""ID"", Department_Name as ""Department"" FROM Department";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("department");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();


            departmentGv.DataSource = dt;
            departmentGv.DataBind();

        }
        protected void SubmitHandler(object sender, EventArgs e)
        {
            string department = departmentTxt.Text.ToString();

            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleConnection con = new OracleConnection(constr);

            if ((sender as Button).Text == "Add")
            {

                OracleCommand cmd = new OracleCommand("Insert into DEPARTMENT(Department_Name) Values('" + department + "')");
                cmd.Connection = con;
                con.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    ErrorLbl.Text = exc.Message;
                    ErrorLbl.Visible = true;
                }
                con.Close();

            }

            else if ((sender as Button).Text == "Update")
            {
                string ID = IDTxt.Text.ToString();
                OracleCommand cmd = new OracleCommand("update DEPARTMENT set Department_Name = '" + department + "' where Department_ID = " + ID);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                departmentGv.EditIndex = -1;
                updateBtn.Visible = false;
                submitBtn.Visible = true;
            }

            departmentTxt.Text = "";

            this.BindGrid();
        }


        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {

            this.BindGrid();
            departmentGv.EditIndex = -1;
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int ID = Convert.ToInt32(departmentGv.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("DELETE FROM Department WHERE Department_ID =" + ID))
                {

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            this.BindGrid();
            departmentGv.EditIndex = -1;

        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != departmentGv.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
            departmentGv.EditIndex = -1;

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            IDTxt.Text = this.departmentGv.Rows[e.NewEditIndex].Cells[1].Text;
            departmentTxt.Text = this.departmentGv.Rows[e.NewEditIndex].Cells[2].Text.ToString();
            updateBtn.Visible = true;
            submitBtn.Visible = false;

        }


    }
}