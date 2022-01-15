using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using MLDemo.WebApi.DataModels;
using MLDemo.WebApi.Requests;

namespace MLDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSSpamDetectionController : ControllerBase
    {
        private readonly PredictionEnginePool<SMSSpamDetectionModelInput, SMSSpamDetectionModelOutput> _predictionEnginePool;

        public SMSSpamDetectionController(PredictionEnginePool<SMSSpamDetectionModelInput, SMSSpamDetectionModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpPost("Predict")]
        public ActionResult Analyze([FromBody] BaseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var input = new SMSSpamDetectionModelInput
            {
                Text = request.Message
            };

            var predictionResult = _predictionEnginePool.Predict(modelName: "MLDemo.Models.SMSSpamDetectionMLModel", example: input);

            var result = Convert.ToBoolean(predictionResult.PredictedLabel) ? "Spam" : "Ham";
            var probability = Math.Round(predictionResult.Score.Max() * 100, 2);

            return Ok(new
            {
                Text = request.Message,
                result,
                Probability = probability
            });
        }
    }
}
