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
    public partial class Student : System.Web.UI.Page
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
            cmd.CommandText = @"SELECT Student_ID as ""ID"", Student_Name as ""Name"", Street_NO as ""Street Number"", Street_Name as ""Street Name"", State_Name as ""State Name"" FROM Student";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("student");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();


            studentGv.DataSource = dt;
            studentGv.DataBind();

        }
        protected void SubmitHandler(object sender, EventArgs e)
        {

            // insert code
            string name = nameTxt.Text.ToString();
            string streetNo = streetNoTxt.Text.ToString();
            string streetName = streetNameTxt.Text.ToString();
            string stateName = stateNameTxt.Text.ToString();

            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleConnection con = new OracleConnection(constr);

            if ((sender as Button).Text == "Add")
            {

                OracleCommand cmd = new OracleCommand($"Insert into STUDENT (Student_Name, Street_No, Street_Name, State_Name) Values('{name}', {streetNo}, '{streetName}', '{stateName}' )");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

            else if ((sender as Button).Text == "Update")
            {
                //get ID for the Update
                string ID = IDTxt.Text.ToString();
                OracleCommand cmd = new OracleCommand($"update Student set Student_Name = '{name}', Street_No = '{streetNo}', Street_Name='{streetName}', State_Name= '{stateName}' where Student_ID = {ID} ");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                studentGv.EditIndex = -1;
                updateBtn.Visible = false;
                submitBtn.Visible = true;
            }

            nameTxt.Text            ="";
            streetNoTxt.Text        ="";
            streetNameTxt.Text      ="";
            stateNameTxt.Text       ="";

            this.BindGrid();
        }


        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            this.BindGrid();
            studentGv.EditIndex = -1;
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int ID = Convert.ToInt32(studentGv.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("DELETE FROM Student WHERE Student_ID =" + ID))
                {

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            this.BindGrid();
            studentGv.EditIndex = -1;

        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != studentGv.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
            studentGv.EditIndex = -1;
        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            IDTxt.Text = this.studentGv.Rows[e.NewEditIndex].Cells[1].Text;
            nameTxt.Text = this.studentGv.Rows[e.NewEditIndex].Cells[2].Text.ToString().TrimStart().TrimEnd(); // (row.Cells[2].Controls[0] as TextBox).Text;
            streetNoTxt.Text = this.studentGv.Rows[e.NewEditIndex].Cells[3].Text;
            streetNameTxt.Text = this.studentGv.Rows[e.NewEditIndex].Cells[4].Text;
            stateNameTxt.Text = this.studentGv.Rows[e.NewEditIndex].Cells[5].Text;
            updateBtn.Visible = true;
            submitBtn.Visible = false;

        }
    }
}