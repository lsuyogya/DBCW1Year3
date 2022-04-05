using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coursework1
{
    public partial class Teacher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
                this.BindDropDown();
            }
        }

        private void BindGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT Teacher_ID as ""ID"", Teacher_Name as ""Name"", Email, Student_ID ""Student ID"" FROM Teacher";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("teacher");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();


            teacherGv.DataSource = dt;
            teacherGv.DataBind();

        }

        private void BindDropDown()
        {
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT Student_ID as ""ID"" FROM Student";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("student");
            //dt.NewRow
            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();

            studentIDDropDown.DataSource = dt;
            studentIDDropDown.DataTextField = "ID";
            studentIDDropDown.DataValueField = "ID";
            studentIDDropDown.DataBind();
            studentIDDropDown.Items.Insert(0, "Null");
        }

        //protected void StudentHandler(object sender, EventArgs e)
        //{
        //    if ((sender as Button).Text == "Former Student")
        //        formerStudentBtn.Text = "Not Former Student";
                
        //    else
        //        formerStudentBtn.Text = "Former Student";

        //}

        protected void SubmitHandler(object sender, EventArgs e)
        {

            // insert code
            string name = nameTxt.Text.ToString();
            string email = emailTxt.Text.ToString();
            bool isStudent = int.TryParse(studentIDDropDown.SelectedValue.ToString(), out int student_ID);

            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleConnection con = new OracleConnection(constr);

            if ((sender as Button).Text == "Add")
            {
                if (isStudent)
                {
                    OracleCommand cmd = new OracleCommand($"Insert into TEACHER(Teacher_Name, Email, Student_ID) Values('{name}','{email}', '{student_ID}')");
                    cmd.Connection = con;
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }catch (Exception exc)
                    {
                        ErrorLbl.Text = exc.Message;
                        ErrorLbl.Visible = true;
                    }
                    con.Close();
                }
                else 
                { 
                    OracleCommand cmd = new OracleCommand($"Insert into TEACHER(Teacher_Name, Email) Values('{name}','{email}')");
                    cmd.Connection = con;
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception exc)
                    {
                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{exc.ToString()}')", true);
                        ErrorLbl.Text = exc.Message;
                        ErrorLbl.Visible = true;
                        //Task.Delay(TimeSpan.FromSeconds(3));
                        //System.Threading.Thread.Sleep(2000);
                        //ErrorLbl.Visible = false;

                    }
                    con.Close();

                }
                

            }

            else if ((sender as Button).Text == "Update")
            {
                //get ID for the Update
                string ID = IDTxt.Text.ToString();
                if (!isStudent)
                {
                    OracleCommand cmd = new OracleCommand($"update TEACHER set Teacher_Name = '{name}', Email = '{email}' where Teacher_ID = {ID} ");
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
                else 
                { 
                    OracleCommand cmd = new OracleCommand($"update TEACHER set Teacher_Name = '{name}', Email = '{email}', Student_ID = '{student_ID}' where Teacher_ID = {ID} ");
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

                

                teacherGv.EditIndex = -1;
                updateBtn.Visible = false;
                submitBtn.Visible = true;
            }

            nameTxt.Text = "";
            emailTxt.Text = "";

            this.BindGrid();
        }


        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {

            this.BindGrid();
            teacherGv.EditIndex = -1;
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int ID = Convert.ToInt32(teacherGv.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand("DELETE FROM Teacher WHERE Teacher_ID =" + ID))
                {

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            this.BindGrid();
            teacherGv.EditIndex = -1;

        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != teacherGv.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }
            //this.BindGrid();
            teacherGv.EditIndex = -1;

        }
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {

            // get id for data update
            IDTxt.Text = this.teacherGv.Rows[e.NewEditIndex].Cells[1].Text;
            nameTxt.Text = this.teacherGv.Rows[e.NewEditIndex].Cells[2].Text.ToString().TrimStart().TrimEnd(); // (row.Cells[2].Controls[0] as TextBox).Text;
            emailTxt.Text = this.teacherGv.Rows[e.NewEditIndex].Cells[3].Text;
            updateBtn.Visible = true;
            submitBtn.Visible = false;

        }


    }
}