using System;
using System.IO;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;

namespace MSReports.Components
{
    public class MatchRegistration : ReportGenerator
    {
        public int myExtra = -1;

        public MatchRegistration(string connectionStringKey) : base (connectionStringKey)
        {
            ;
        }

        public byte[] GeneratePDF(int matchId, int extra)
        {
            myExtra = extra;

            using (Stream stream = GetResourceStream("MatchDetail.rdlc"))
            using (Stream streamSub = GetResourceStream("MatchPlayerList.rdlc"))
            {
                MyLocalReport.LoadReportDefinition(stream);
                MyLocalReport.LoadSubreportDefinition("MatchPlayerList", streamSub);

                DataTable dtReportData = new DataTable();
                using (var cmd = new SqlCommand("[ms].[MatchSelectById]", MyConnection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("MatchId", SqlDbType.Int).Value = matchId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dtReportData);
                    //ReportName = string.Format("MatchTilmelding_{0}", matchId);
                    ReportName = "MatchTilmelding";

                    ReportDataSource x = new ReportDataSource("MsReports_MsEvent", dtReportData);
                    MyLocalReport.DataSources.Add(x);
                }

                MyLocalReport.SubreportProcessing +=
                    new Microsoft.Reporting.WebForms.SubreportProcessingEventHandler(MatchRegistration_SubreportProcessing);

                return RenderReport();
            }
        }
        private void MatchRegistration_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            string dataSourceName = e.DataSourceNames[0];

            int matchId = Int32.Parse(e.Parameters["MatchId"].Values[0]);
            DataTable dtReportData = new DataTable();
            using (var cmd = new SqlCommand("[ms].[MatchResultSelectForRegistration]", MyConnection))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("MatchId", SqlDbType.Int).Value = matchId;
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
