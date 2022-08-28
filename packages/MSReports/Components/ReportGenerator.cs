using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace MSReports
{
    public class ReportGenerator
    {
        private string myConnectionStringKey;
        private string myBinPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
        public ReportGenerator(string connectionStringKey)
        {
            myConnectionStringKey = connectionStringKey;
            ReportName = "Report";
        }

        public ReportGenerator(string connectionStringKey, string binPath) : this(connectionStringKey)
        {
            myBinPath = binPath;
        }

        public byte[] RenderReport()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            string pdfFilename = string.Empty;

            myViewer.LocalReport.Refresh();

            return myViewer.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);
        }

        protected Stream GetResourceStream(string report)
        {
            return MyAssembly.GetManifestResourceStream("MSReports.Reports." +report);
        }

        public string ReportName { get; set; }

        #region MyLocalReport
        private Microsoft.Reporting.WebForms.ReportViewer myViewer;
        public Microsoft.Reporting.WebForms.LocalReport MyLocalReport
        {
            get
            {
                if (myViewer == null)
                {
                    myViewer = new Microsoft.Reporting.WebForms.ReportViewer();
                    myViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    myViewer.LocalReport.EnableExternalImages = true;
                }
                return myViewer.LocalReport;
            }
        }
        #endregion

        #region MyConnection
        private SqlConnection myConnection;
        public SqlConnection MyConnection
        {
            get
            {
                if (myConnection == null)
                {
                    if (ConfigurationManager.ConnectionStrings[myConnectionStringKey] == null)
                        throw new ConfigurationErrorsException(string.Format("ConnectionString not defined in configuration file: ", myConnectionStringKey));

                    myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[myConnectionStringKey].ConnectionString);
                }
                return myConnection;
            }
        }
        #endregion

        #region MyAssembly
        private Assembly myAssembly;
        private Assembly MyAssembly
        {
            get
            {
                if (myAssembly == null)
                {
                    myAssembly = Assembly.Load(System.IO.File.ReadAllBytes(myBinPath + "\\MSReports.dll"));

                }
                return myAssembly;
            }
        }
        #endregion
    }
}
