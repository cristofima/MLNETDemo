using Microsoft.ML.Data;

namespace MLDemo.WebApi.DataModels
{
    public class SentimentAnalysisModelInput
    {
        [ColumnName(@"clean_text")]
        public string CleanText { get; set; }

        [ColumnName(@"category")]
        public float Category { get; set; }
    }

    public class SentimentAnalysisModelOutput
    {

        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")]
        public float[] Score { get; set; }

    }
}
