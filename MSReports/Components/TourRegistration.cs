using System;
using System.IO;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;

namespace MSReports.Components
{
    public class TourRegistration : ReportGenerator
    {
        public int myExtra = -1;

        public TourRegistration(string connectionStringKey) : base(connectionStringKey)
        {
            ;
        }
        public TourRegistration(string connectionStringKey, string binPath) : base (connectionStringKey, binPath)
        {
            ;
        }

        public byte[] GeneratePDF(int tourId, int extra)
        {
            myExtra = extra;

            try
            {
                using (Stream stream = GetResourceStream("TourDetail.rdlc"))
                using (Stream streamSub = GetResourceStream("TourPlayerList.rdlc"))
                {
                    MyLocalReport.LoadReportDefinition(stream);
                    MyLocalReport.LoadSubreportDefinition("TourPlayerList", streamSub);

                    DataTable dtReportData = new DataTable();
                    using (var cmd = new SqlCommand("[ms].[TourSelectByID]", MyConnection))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("tourId", SqlDbType.Int).Value = tourId;
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dtReportData);
                        ReportName = "TurTilmelding";
                        ReportDataSource x = new ReportDataSource("MsReports_Tour", dtReportData);
                        MyLocalReport.DataSources.Add(x);
                    }

                    MyLocalReport.SubreportProcessing +=
                        new Microsoft.Reporting.WebForms.SubreportProcessingEventHandler(TourRegistration_SubreportProcessing);

                    return RenderReport();
                }

            }
            catch (Exception e)
            {
                string s = e.ToString();
                throw;
            }        }
        private void TourRegistration_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            string dataSourceName = e.DataSourceNames[0];

            int tourId = Int32.Parse(e.Parameters["tourId"].Values[0]);
            DataTable dtReportData = new DataTable();
            using (var cmd = new SqlCommand("[ms].[TourPlayerSelect]", MyConnection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("tourId", SqlDbType.Int).Value = tourId;
                if (myExtra > 0)
                    cmd.Parameters.Add("extra", SqlDbType.Int).Value = myExtra;
                cmd.CommandType = CommandType.StoredProcedure;
                da.Fill(dtReportData);
                ReportDataSource rds = new ReportDataSource(dataSourceName, dtReportData);
                e.DataSources.Add(rds);
            }
        }
    }
}
