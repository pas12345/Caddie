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
                        using (var cmd = new SqlCommand("[ms].[MatchResultSelectOthers]", MyConnection))
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
/*
        void LocalReport_SubreportProcessing(object sender, Microsoft.Reporting.WebForms.SubreportProcessingEventArgs e)
        {
            try
            {
                if (e.ReportPath.Equals("MatchResultOverall", StringComparison.OrdinalIgnoreCase))
                {
                    int eventId = Int32.Parse(e.Parameters["EventId"].Values[0]);
                    this.ObjectDataSourceOverall.SelectParameters.RemoveAt(0);
                    Parameter p2 = new Parameter("EventId", TypeCode.Int32, eventId.ToString());
                    this.ObjectDataSourceOverall.SelectParameters.Insert(0, p2);
                    ReportDataSource rds2 = new ReportDataSource("MsReports_MsResult", this.ObjectDataSourceOverall);
                    e.DataSources.Add(rds2);
                }

                if (e.ReportPath.Equals("MatchResultGroup", StringComparison.OrdinalIgnoreCase))
                {
                    int eventId = Int32.Parse(e.Parameters["EventId"].Values[0]);
                    int hcpGroup = Int32.Parse(e.Parameters["HcpGroup"].Values[0]);
                    Parameter p3 = new Parameter("EventId", TypeCode.Int32, eventId.ToString());
                    if (1 == hcpGroup)
                    {
                        this.ObjectDataSourceA.SelectParameters.RemoveAt(0);
                        this.ObjectDataSourceA.SelectParameters.Insert(0, p3);
                        ReportDataSource rds = new ReportDataSource("MsReports_MsResult", this.ObjectDataSourceA);
                        e.DataSources.Add(rds);
                    }
                    else if (2 == hcpGroup)
                    {
                        this.ObjectDataSourceB.SelectParameters.RemoveAt(0);
                        this.ObjectDataSourceB.SelectParameters.Insert(0, p3);
                        ReportDataSource rds = new ReportDataSource("MsReports_MsResult", this.ObjectDataSourceB);
                        e.DataSources.Add(rds);
                    }
                    else if (3 == hcpGroup)
                    {
                        this.ObjectDataSourceC.SelectParameters.RemoveAt(0);
                        this.ObjectDataSourceC.SelectParameters.Insert(0, p3);
                        ReportDataSource rds = new ReportDataSource("MsReports_MsResult", this.ObjectDataSourceC);
                        e.DataSources.Add(rds);
                    }
                }
                if (e.ReportPath.Equals("MatchResultOther", StringComparison.OrdinalIgnoreCase))
                {
                    int eventId = Int32.Parse(e.Parameters["EventId"].Values[0]);
                    this.ObjectDataSourceOther.SelectParameters.RemoveAt(0);
                    Parameter p2 = new Parameter("EventId", TypeCode.Int32, eventId.ToString());
                    this.ObjectDataSourceOther.SelectParameters.Insert(0, p2);
                    ReportDataSource rds2 = new ReportDataSource("MsReports_MsEventResult", this.ObjectDataSourceOther);
                    e.DataSources.Add(rds2);
                }
                if (e.ReportPath.Equals("MatchResultNearestPin", StringComparison.OrdinalIgnoreCase))
                {
                    int eventId = int.Parse(e.Parameters["EventId"].Values[0]);
                    this.ObjectDataSourceNearestPin.SelectParameters.RemoveAt(0);
                    Parameter p3 = new Parameter("EventId", TypeCode.Int32, eventId.ToString());
                    this.ObjectDataSourceNearestPin.SelectParameters.Insert(0, p3);
                    ReportDataSource rds3 = new ReportDataSource("MsReports_MsResult", this.ObjectDataSourceNearestPin);
                    e.DataSources.Add(rds3);
                }
                if (e.ReportPath.Equals("MatchResultBirdies", StringComparison.OrdinalIgnoreCase))
                {
                    int eventId = int.Parse(e.Parameters["EventId"].Values[0]);
                    this.ObjectDataSourceBirdies.SelectParameters.RemoveAt(0);
                    Parameter p4 = new Parameter("EventId", TypeCode.Int32, eventId.ToString());
                    this.ObjectDataSourceBirdies.SelectParameters.Insert(0, p4);
                    ReportDataSource rds4 = new ReportDataSource("MsReports_MsResultBirdies", this.ObjectDataSourceBirdies);
                    e.DataSources.Add(rds4);
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        */
    }

}
