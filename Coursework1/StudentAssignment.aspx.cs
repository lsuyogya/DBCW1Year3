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
    public partial class StudentAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindStudentGrid();
                this.BindStudentAssignmentGrid("0");
            }

        }

        private void BindStudentGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT Student_ID as ""ID"", Student_Name as ""Name"", Street_No ""Street No."", Street_Name ""Street Name"", State_Name ""State Name"" FROM Student";
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

        private void BindStudentAssignmentGrid(string studentID)
        {
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = $@"SELECT sa.Student_ID ""Student ID"", m.Module_Code ""Module Code"" , m.Module_Name ""Module Name"", a.Assignment_Type ""Assignment Type"" ,
                                d.Department_Name ""Department Name"" , g.Grade ""Grade"", g.Status ""Status""
                                FROM Student s
                                INNER JOIN Student_Assignment sa ON sa.Student_ID = s.Student_ID
                                INNER JOIN Grade g ON sa.Grade_ID = g.Grade_ID
                                INNER JOIN Module m ON sa.Module_Code = m.Module_Code
                                INNER JOIN Assignment a ON sa.Assignment_ID = a.Assignment_ID
                                INNER JOIN Department d ON a.Department_ID = d.Department_ID
                                WHERE sa.Student_ID = {studentID} 
                                ";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("student");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();


            studentAssignmentGv.DataSource = dt;
            studentAssignmentGv.DataBind();

        }


        protected void onStudentSelect(object sender, EventArgs e)
        {
            this.BindStudentAssignmentGrid(studentGv.SelectedRow.Cells[1].Text);
        }
    }
}