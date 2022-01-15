using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using MLDemo.WebApi.DataModels;
using MLDemo.WebApi.Requests;

namespace MLDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentimentAnalysisController : ControllerBase
    {
        private readonly PredictionEnginePool<SentimentAnalysisModelInput, SentimentAnalysisModelOutput> _predictionEnginePool;

        public SentimentAnalysisController(PredictionEnginePool<SentimentAnalysisModelInput, SentimentAnalysisModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpPost("Predict")]
        public ActionResult Post([FromBody] BaseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var input = new SentimentAnalysisModelInput
            {
                CleanText = request.Message
            };

            var predictionResult = _predictionEnginePool.Predict(modelName: "MLDemo.Models.SentimentAnalysisMLModel", example: input);

            var sentiment = "Neutral";
            if (predictionResult.PredictedLabel == 1)
            {
                sentiment = "Positive";
            }
            else if (predictionResult.PredictedLabel == -1)
            {
                sentiment = "Negative";
            }

            var probability = Math.Round(predictionResult.Score.Max() * 100, 2);

            return Ok(new
            {
                Text = request.Message,
                Sentiment = sentiment,
                Probability = probability
            });
        }
    }
}