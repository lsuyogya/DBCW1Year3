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
    public partial class StudentFees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindStudentGrid();
                this.BindStudentFeesGrid("0");
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

        private void BindStudentFeesGrid(string studentID)
        {
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = $@"SELECT sf.Student_ID ""Student ID"", se.Semester ""Semester"",  d.Department_Name as ""Department Name"", sf.Fee_Amount ""Fee Amount"", sf.Fee_Status ""Fee Status""
                                FROM Student s
                                INNER JOIN Student_Fees sf ON sf.Student_ID = s.Student_ID
                                INNER JOIN Semester se ON se.Semester_ID = sf.Semester_ID
                                INNER JOIN Department d ON d.Department_ID = sf.Department_ID
                                WHERE sf.Student_ID = {studentID} 
                                ";
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable("student");

            using (OracleDataReader sdr = cmd.ExecuteReader())
            {
                dt.Load(sdr);
            }

            con.Close();


            studentFeesGv.DataSource = dt;
            studentFeesGv.DataBind();

        }


        protected void onStudentSelect(object sender, EventArgs e)
        {
            this.BindStudentFeesGrid(studentGv.SelectedRow.Cells[1].Text);
        }
    }
}