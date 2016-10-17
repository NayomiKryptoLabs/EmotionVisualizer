using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmotionVisualizer
{
    public partial class Graphs : System.Web.UI.Page
    {
        MySqlConnection connection;
        string queryString;
        string studentId;
        string start;
        string end;

        protected void Page_Load(object sender, EventArgs e)
        {
            studentId = Request.QueryString["stuid"];
            DateTime date = DateTime.Parse(Request.QueryString["date"]);

            DateTime startDateTime = date;
            TimeSpan time = new TimeSpan(0, 23, 59, 59, 999);
            DateTime endDateTime = date.Add(time);

            start = "'" + startDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            end = "'" + endDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";

            loadEmotionGraph();
            loadSummaryPieChart();
            loadValenceGraph();
            loadEmotionHappyGraph();
            loadEmotionSadGraph();
            loadEmotionNeutralGraph();
            loadEmotionSurprisedGraph();
        }

        private void loadEmotionGraph()
        {
            queryString = "select Neutral,Happy,Sad,Angry,Surprised,Scared,Disgusted,Timestamp from alef.emotions where Student_id= "
                + studentId + " and Timestamp BETWEEN " + start + " AND " + end + "; ";

            DataTable data = loadDatafromDB(queryString);
            Object[] neutralIntensities = new Object[data.Rows.Count];
            Object[] happyIntensities = new Object[data.Rows.Count];
            Object[] sadIntensities = new Object[data.Rows.Count];
            Object[] angryIntensities = new Object[data.Rows.Count];
            Object[] surprisedIntensities = new Object[data.Rows.Count];
            Object[] scaredIntensities = new Object[data.Rows.Count];
            Object[] disgustedIntensities = new Object[data.Rows.Count];
            string[] timestamps = new string[data.Rows.Count];

            int index = 0;

            foreach (DataRow dr in data.Rows)
            {
                neutralIntensities[index] = (dr["Neutral"].ToString() != "") ? dr["Neutral"] : null;
                happyIntensities[index] = (dr["Happy"].ToString() != "") ? dr["Happy"] : null;
                sadIntensities[index] = (dr["Sad"].ToString() != "") ? dr["Sad"] : null;
                angryIntensities[index] = (dr["Angry"].ToString() != "") ? dr["Angry"] : null;
                surprisedIntensities[index] = (dr["Surprised"].ToString() != "") ? dr["Surprised"] : null;
                scaredIntensities[index] = (dr["Scared"].ToString() != "") ? dr["Scared"] : null;
                disgustedIntensities[index] = (dr["Disgusted"].ToString() != "") ? dr["Disgusted"] : null;
                timestamps[index] = dr["Timestamp"].ToString();
                index++;
            }

            Series[] serieses = new Series[7];

            Series neutralDataSeries = new Series { Data = new Data(neutralIntensities) };
            neutralDataSeries.Name = "Neutral";

            Series happyDataSeries = new Series { Data = new Data(happyIntensities) };
            happyDataSeries.Name = "Happy";

            Series sadDataSeries = new Series { Data = new Data(sadIntensities) };
            sadDataSeries.Name = "Sad";

            Series angryDataSeries = new Series { Data = new Data(angryIntensities) };
            angryDataSeries.Name = "Angry";

            Series surprisedDataSeries = new Series { Data = new Data(surprisedIntensities) };
            surprisedDataSeries.Name = "Surprised";

            Series scaredDataSeries = new Series { Data = new Data(scaredIntensities) };
            scaredDataSeries.Name = "Scared";

            Series disgustedDataSeries = new Series { Data = new Data(disgustedIntensities) };
            disgustedDataSeries.Name = "Disgusted";

            serieses[0] = neutralDataSeries;
            serieses[1] = happyDataSeries;
            serieses[2] = sadDataSeries;
            serieses[3] = angryDataSeries;
            serieses[4] = surprisedDataSeries;
            serieses[5] = scaredDataSeries;
            serieses[6] = disgustedDataSeries;

            Title title = new DotNet.Highcharts.Options.Title();
            title.Text = "Emotion Summary";

            XAxis xAxis = new XAxis();
            xAxis.Categories = timestamps;
            xAxis.TickInterval = 200;

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart1").SetXAxis(xAxis).SetSeries(serieses).SetTitle(title);
            ltrChart.Text = chart.ToHtmlString();
        }

        private void loadSummaryPieChart()
        {
            queryString = "select count(*) as count, CASE greatest(Neutral,Happy,Sad,Angry,Surprised,Scared,Disgusted)" +
                " WHEN Neutral THEN 'Neutral'" +
                " WHEN Happy THEN 'Happy'" +
                " WHEN Sad THEN 'Sad'" +
                " WHEN Angry THEN 'Angry'" +
                " WHEN Surprised THEN 'Surprised'" +
                " WHEN Scared THEN 'Scared'" +
                " WHEN Disgusted THEN 'Disgusted'" +
                " else 'Unknown'" +
                " END AS maxColumn from alef.emotions where Student_id=" + studentId +
                " and Timestamp BETWEEN " + start + " AND " + end + " group by maxColumn;";

            DataTable data = loadDatafromDB(queryString);
            var percentages = new List<object[]>();
            double totalNumOfRecords = 0;

            foreach (DataRow dr in data.Rows)
            {
                totalNumOfRecords += Convert.ToDouble(dr["count"]);
            }

            foreach (DataRow dr in data.Rows)
            {
                double percentage = Math.Round((((Convert.ToDouble(dr["count"])) / totalNumOfRecords) * 100.0), 2);
                percentages.Add(new object[] { dr["maxColumn"].ToString(), percentage.ToString() });
            }

            Title title = new DotNet.Highcharts.Options.Title();
            title.Text = "";

            DotNet.Highcharts.Highcharts chart2 = new DotNet.Highcharts.Highcharts("chart2").SetTitle(title);
            chart2.SetSeries(new Series
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Pie,
                Data = new Data(percentages.ToArray())
            });
            ltrChart2.Text = chart2.ToHtmlString();
        }

        private void loadValenceGraph()
        {
            queryString = "select Valence,Timestamp from alef.emotions where Student_id="
                + studentId + " and Timestamp BETWEEN" + start + " AND " + end + ";";

            ConfigDAO configs = new ConfigDAO(queryString, "Valence", "Valence Variation", "chart3", Color.Tomato);
            generateGraph(configs, ltrChart3);
        }

        private void loadEmotionHappyGraph()
        {
            queryString = "select Happy,Timestamp from alef.emotions where Student_id="
                + studentId + " and Timestamp BETWEEN" + start + " AND " + end + ";";

            ConfigDAO configs = new ConfigDAO(queryString, "Happy", "Emotion Variation - Happiness", "chart4", Color.Yellow);
            generateGraph(configs, ltrChart4);
        }

        private void loadEmotionSadGraph()
        {
            queryString = "select Sad,Timestamp from alef.emotions where Student_id="
                + studentId + " and Timestamp BETWEEN" + start + " AND " + end + ";";

            ConfigDAO configs = new ConfigDAO(queryString, "Sad", "Emotion Variation - Sadness", "chart5", Color.BlueViolet);
            generateGraph(configs, ltrChart5);
        }

        private void loadEmotionNeutralGraph()
        {
            queryString = "select Neutral,Timestamp from alef.emotions where Student_id="
                + studentId + " and Timestamp BETWEEN" + start + " AND " + end + ";";

            ConfigDAO configs = new ConfigDAO(queryString, "Neutral", "Emotion Variation - Neutral", "chart6", Color.SpringGreen);
            generateGraph(configs, ltrChart6);
        }

        private void loadEmotionSurprisedGraph()
        {
            queryString = "select Surprised,Timestamp from alef.emotions where Student_id="
                + studentId + " and Timestamp BETWEEN" + start + " AND " + end + ";";

            ConfigDAO configs = new ConfigDAO(queryString, "Surprised", "Emotion Variation - Surprise", "chart7", Color.RosyBrown);
            generateGraph(configs, ltrChart7);
        }


        private void generateGraph(ConfigDAO configs, Literal literal)
        {
            DataTable data = loadDatafromDB(queryString);
            Object[] intensities = new Object[data.Rows.Count];
            string[] timestamps = new string[data.Rows.Count];
            int index = 0;

            foreach (DataRow dr in data.Rows)
            {
                intensities[index] = (dr[configs.emotion].ToString() != "") ? dr[configs.emotion] : null;
                timestamps[index] = dr["Timestamp"].ToString();
                index++;
            }

            Series[] serieses = new Series[1];
            Series dataSeries = new Series { Data = new Data(intensities) };
            dataSeries.Color = configs.color;
            dataSeries.Name = configs.emotion;
            serieses[0] = dataSeries;

            Title title = new DotNet.Highcharts.Options.Title();
            title.Text = configs.graphTitle;

            XAxis xAxis = new XAxis();
            xAxis.Categories = timestamps;
            xAxis.TickInterval = 200;

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts(configs.chartName).SetXAxis(xAxis).SetSeries(serieses).SetTitle(title);
            literal.Text = chart.ToHtmlString();
        }

        private DataTable loadDatafromDB(string queryString)
        {
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["mySqlConString"].ToString();
            connection = new MySqlConnection(conString);
            connection.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter(queryString, connection);
            adapter.SelectCommand.CommandType = CommandType.Text;
            DataTable data = new DataTable();
            adapter.Fill(data);
            connection.Close();

            return data;
        }
    }
}