using AthleteDBUI.Library.DataAccess;
using AthleteDBUI.Models;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AthleteDBUI.WinForms
{
    public partial class ProfileForm : Form
    {
        private List<ProfileDisplayModel> _profiles;
        private List<AthleteDisplayModel> _athletes;
        private int _athleteId;

        public ProfileForm()
        {
            InitializeComponent();
            _profiles = GetAllProfiles();
            _athletes = GetAllAthletes();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                return;
            }

            _athleteId = _athletes.Where(x => x.FullName == comboBox1.Text).FirstOrDefault().Id;


            SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ADBData;Integrated Security=True");            
            SqlCommand cmd = new SqlCommand("spResult_GetAllByAthleteId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlParameter athleteId = cmd.Parameters.AddWithValue("@AthleteId", _athleteId);

            //athleteId.Direction = ParameterDirection.Input;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();

            ReportDataSource rds = new ReportDataSource("DataSet2", dt);
            reportViewer1.LocalReport.ReportPath = @"C:\WPF Projects\AthleteDB\AthleteDBUI\Reports\AthleteReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        private List<ProfileDisplayModel> GetAllProfiles()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<ProfileDisplayModel, dynamic>("dbo.spResult_GetAllProfiles", new { }, "ADBData");
            return output;
        }

        private List<AthleteDisplayModel> GetAllAthletes()
        {
            SqlDataAccess sql = new SqlDataAccess();
            var output = sql.LoadData<AthleteDisplayModel, dynamic>("dbo.spAthlete_GetAll", new { }, "ADBData");
            return output;
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            var cbBindingSource = new BindingSource();
            cbBindingSource.DataSource = _athletes;
            comboBox1.DataSource = cbBindingSource.DataSource;
            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "FullName";

        }
    }
}
