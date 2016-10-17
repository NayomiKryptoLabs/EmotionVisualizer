using System.Drawing;

namespace EmotionVisualizer
{
    internal class ConfigDAO
    {
        public string queryString;
        public Color color;
        public string emotion;
        public string graphTitle;
        public string chartName;

        public ConfigDAO(string queryString, string emotion, string graphTitle, string chartName, Color color)
        {
            this.queryString = queryString;
            this.emotion = emotion;
            this.graphTitle = graphTitle;
            this.chartName = chartName;
            this.color = color;
        }
    }
}