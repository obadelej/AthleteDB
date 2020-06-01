using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AthleteDBUI.Views
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        private bool _isReportViewerLoaded;

        public ReportWindow()
        {
            InitializeComponent();
            _reportViewer1.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                AthleteView dataset = new AthleteView();
                dataset.BeginInit();
                reportDataSource1.Name = "Athlete_DS";
                reportDataSource1.Value = dataset.Athletes;
                this._reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this._reportViewer1.LocalReport.ReportEmbeddedResource = "AthleteDB.AthleteReport.rdlc";

                dataset.EndInit();

                _reportViewer1.RefreshReport();

                _isReportViewerLoaded = true;


            }
        }
    }
}
