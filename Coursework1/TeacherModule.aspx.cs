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
    public partial class TeacherModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindTeacherGrid();
                this.BindModuleGrid("0");
            }
            
        }

        private void BindTeacherGrid()
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

        private void BindModuleGrid(string teacherID)
        {
            string constr = ConfigurationManager.ConnectionStrings["coursework"].ConnectionString;
            OracleCommand cmd = new OracleCommand();
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = $@"SELECT m.Module_Code as ""Module Code"", mt.Teacher_ID ""Teacher ID"", m.Module_Name as ""Module Name"", m.Credit_Hours ""Credit Hours"" 
                                FROM Module m
                                INNER JOIN Module_Teacher mt ON mt.Module_ID = m.Module_Code
                                WHERE mt.Teacher_ID = {teacherID} 
                                ";
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


        protected void onTeacherSelect(object sender, EventArgs e) {
            this.BindModuleGrid(teacherGv.SelectedRow.Cells[1].Text);
        }
    }
}