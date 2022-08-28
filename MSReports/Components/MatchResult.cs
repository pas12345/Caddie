using System;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;

namespace MSReports.Components
{
    public class MatchResult : ReportGenerator
    {
        public MatchResult(string connectionStringKey) : base (connectionStringKey)
        {
            ;
        }
        public MatchResult(string connectionStringKey, string binPath) : base(connectionStringKey, binPath)
        {
            ;
        }
        public byte[] GeneratePDF(int matchId)
        {
            using (Stream stream = GetResourceStream("MatchResult.rdlc"))
            using (Stream streamOverall = GetResourceStream("MatchResultOverall.rdlc"))
            using (Stream streamGroup = GetResourceStream("MatchResultGroup.rdlc"))
            using (Stream streamBirdies = GetResourceStream("MatchResultBirdies.rdlc"))
            using (Stream streamNearestPin = GetResourceStream("MatchResultNearestPin.rdlc"))
            using (Stream streamOther = GetResourceStream("MatchResultOther.rdlc"))
            {
                MyLocalReport.LoadReportDefinition(stream);
                MyLocalReport.LoadSubreportDefinition("MatchResultOverall", streamOverall);
                MyLocalReport.LoadSubreportDefinition("MatchResultGroup", streamGroup);
                MyLocalReport.LoadSubreportDefinition("MatchResultBirdies", streamBirdies);
                MyLocalReport.LoadSubreportDefinition("MatchResultNearestPin", streamNearestPin);
                MyLocalReport.LoadSubreportDefinition("MatchResultOther", streamOther);

                DataTable dtReportData = new DataTable();
                using (var cmd = new SqlCommand("[ms].[MatchSelectById]", MyConnection))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("MatchId", SqlDbType.Int).Value = matchId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dtReportData);
                    ReportName = string.Format("MatchResult_{0}", matchId);

                    ReportDataSource x = new ReportDataSource("MsReports_MsEvent", dtReportData);
                    MyLocalReport.DataSources.Add(x);
                }

                MyLocalReport.SubreportProcessing +=
                    new Microsoft.Reporting.WebForms.SubreportProcessingEventHandler(MatchResult_SubreportProcessing);

                return RenderReport();
            }
        }
        private void MatchResult_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            string dataSourceName = e.DataSourceNames[0];
            int matchId = Int32.Parse(e.Parameters["EventId"].Values[0]);

            switch (e.ReportPath)
            {
                case "MatchResultOverall":
                {
                    DataTable dtReportData = new DataTable();
                    using (var cmd = new SqlCommand("[ms].[MatchResultSelectWinner]", MyConnection))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.Add("MatchId", SqlDbType.Int).Value = matchId;

                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dtReportData);
                        ReportDataSource rds = new ReportDataSource(dataSourceName, dtReportData);
                        e.DataSources.Add(rds);
                    }
                }
                break;
                case "MatchResultGroup":
                    {
                        int hcpGroup = Int32.Parse(e.Parameters["HcpGroup"].Values[0]);
                        DataTable dtReportData = new DataTable();
                        using (var cmd = new SqlCommand("[ms].[MatchResultSelectWinners]", MyConnection))
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.Add("MatchId", SqlDbType.Int).Value = matchId;
                            cmd.Parameters.Add("HcpGroup", SqlDbType.Int).Value = hcpGroup;

                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(dtReportData);
                            ReportDataSource rds = new ReportDataSource(dataSourceName, dtReportData);
                            e.DataSources.Add(rds);
                        }
                    }
                    break;
                case "MatchResultBirdies":
                    {
                        DataTable dtReportData = new DataTable();
                        using (var cmd = new SqlCommand("[ms].[MatchResultSelectBirdieString]", MyConnection))
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.Add("MatchId", SqlDbType.Int).Value = matchId;

                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(dtReportData);
                            ReportDataSource rds = new ReportDataSource(dataSourceName, dtReportData);
                            e.DataSources.Add(rds);
                        }
                    }
                    break;
                case "MatchResultNearestPin":
                    {
                        DataTable dtReportData = new DataTable();
                        using (var cmd = new SqlCommand("[ms].[MatchResultNearestPin]", MyConnection))
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.Add("MatchId", SqlDbType.Int).Value = matchId;
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(dtReportData);
                            ReportDataSource rds = new ReportDataSource(dataSourceName, dtReportData);
                            e.DataSources.Add(rds);
                        }
                    }
                    break;
                case "MatchResultOther":
                    {
                        DataTable dtReportData = new DataTable();
                        using (var cmd = new SqlCommand("[ms].[CompetitionResults]", MyConnection))
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.Parameters.Add("MatchId", SqlDbType.Int).Value = matchId;

                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(dtReportData);
                            ReportDataSource rds = new ReportDataSource(dataSourceName, dtReportData);
                            e.DataSources.Add(rds);
                        }
                    }
                    break;
            }
        }

    }

}
