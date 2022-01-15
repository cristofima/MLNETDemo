using Microsoft.ML.Data;

namespace MLDemo.WebApi.DataModels
{
    public class SMSSpamDetectionModelInput
    {
        [ColumnName(@"Label")]
        public float Label { get; set; }

        [ColumnName(@"Text")]
        public string Text { get; set; }

    }

    public class SMSSpamDetectionModelOutput
    {
        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")]
        public float[] Score { get; set; }

    }
}
