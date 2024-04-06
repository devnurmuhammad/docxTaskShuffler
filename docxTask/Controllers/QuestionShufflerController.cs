using docxTask.DTOs;
using docxTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace docxTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionShufflerController : ControllerBase
    {
        private readonly QuestionShufflerService _questionShuffler;

        public QuestionShufflerController(QuestionShufflerService questionShuffler)
        {
            _questionShuffler = questionShuffler;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuestionDto>> RetrieveQuestionsAndAnswers([FromQuery] string filePath)
        {
            try
            {
                var questions = _questionShuffler.ShuffleQuestions(filePath);
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Xato sodir bo'ldi: {ex.Message}");
            }
        }
    }
}
