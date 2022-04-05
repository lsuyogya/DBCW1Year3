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
    public partial class Address : System.Web.UI.Page
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
            cmd.CommandText = @"SELECT Address_ID as ""ID"", Street_No as ""Street Number"", Street_Name as ""Street Name"", State_Name as ""State Name"" FROM Address";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("address");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();


            addressGv.DataSource = dt;
            addressGv.DataBind();

        }
        protected void SubmitHandler(object sender, EventArgs e)
        {
            string streetNo = streetNoTxt.Text.ToString();
            string streetName = streetNameTxt.Text.ToString();
            string stateName = stateNameTxt.Text.ToString();

            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleConnection con = new OracleConnection(constr);

            if ((sender as Button).Text == "Add")
            {

                OracleCommand cmd = new OracleCommand($"Insert into Address (street_no, street_name, state_name) Values('{streetNo}', '{streetName}', '{stateName}')");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

            else if ((sender as Button).Text == "Update")
            {
                string ID = IDTxt.Text.ToString();
                OracleCommand cmd = new OracleCommand($"update Address set street_no = '{streetNo}', street_name = '{streetName}', state_name = '{stateName}' where Address_ID = {ID}");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                addressGv.EditIndex = -1;
                updateBtn.Visible = false;
                submitBtn.Visible = true;
            }

            streetNoTxt.Text = "";
            streetNameTxt.Text = "";
            stateNameTxt.Text = "";

            this.BindGrid();
        }


        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {

            this.BindGrid();
            addressGv.EditIndex = -1;
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int ID = Convert.ToInt32(addressGv.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("DELETE FROM Address WHERE Address_ID =" + ID))
                {

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            this.BindGrid();
            addressGv.EditIndex = -1;

        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != addressGv.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
            addressGv.EditIndex = -1;

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            IDTxt.Text = this.addressGv.Rows[e.NewEditIndex].Cells[1].Text;
            streetNoTxt.Text = this.addressGv.Rows[e.NewEditIndex].Cells[2].Text.ToString();
            streetNameTxt.Text = this.addressGv.Rows[e.NewEditIndex].Cells[3].Text.ToString();
            stateNameTxt.Text = this.addressGv.Rows[e.NewEditIndex].Cells[4].Text.ToString();
            updateBtn.Visible = true;
            submitBtn.Visible = false;

        }


    }
}