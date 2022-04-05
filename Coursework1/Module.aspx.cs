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
    public partial class Module : System.Web.UI.Page
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
            cmd.CommandText = @"SELECT Module_Code ""ID"", Module_Name as ""Name"", Credit_Hours ""Credit Hours"" FROM Module";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("module");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();


            moduleGv.DataSource = dt;
            moduleGv.DataBind();

        }
        protected void SubmitHandler(object sender, EventArgs e)
        {
            string name = nameTxt.Text.ToString();
            string creditHrs = creditHrsTxt.Text.ToString();
            string moduleCode = IDTxt.Text.ToString();

            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleConnection con = new OracleConnection(constr);

            if ((sender as Button).Text == "Add")
            {

                OracleCommand cmd = new OracleCommand($"Insert into MODULE (Module_Code, Module_Name, Credit_Hours) Values ('{moduleCode}', '{name}',{creditHrs})");
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
                //get ID for the Update
                string ID = IDTxt.Text.ToString();
                OracleCommand cmd = new OracleCommand($"update Module set Module_Name = '{name}', Credit_Hours = {creditHrs} where Module_Code = '{ID}' ");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                moduleGv.EditIndex = -1;
                updateBtn.Visible = false;
                submitBtn.Visible = true;
                IDTxt.Visible = true;
                IDLbl.Visible = true;
            }

            nameTxt.Text = "";
            creditHrsTxt.Text = "";
            IDTxt.Text = "";

            this.BindGrid();
        }


        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {

            this.BindGrid();
            moduleGv.EditIndex = -1;
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string ID = moduleGv.DataKeys[e.RowIndex].Values[0].ToString();
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand($"DELETE FROM Module WHERE Module_Code = '{ID}'"))
                {

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            this.BindGrid();
            moduleGv.EditIndex = -1;

        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != moduleGv.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
            moduleGv.EditIndex = -1;

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {

            IDTxt.Text = this.moduleGv.Rows[e.NewEditIndex].Cells[1].Text;
            nameTxt.Text = this.moduleGv.Rows[e.NewEditIndex].Cells[2].Text.ToString().TrimStart().TrimEnd(); // (row.Cells[2].Controls[0] as TextBox).Text;
            creditHrsTxt.Text = this.moduleGv.Rows[e.NewEditIndex].Cells[3].Text;
            updateBtn.Visible = true;
            submitBtn.Visible = false;
            IDTxt.Visible = false;
            IDLbl.Visible = false;

        }


    }
}