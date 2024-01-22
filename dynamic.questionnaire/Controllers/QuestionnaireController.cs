using dynamic.questionnaire.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dynamic.questionnaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        private readonly Random random = new();
        private readonly IQuestionnaireRepo questionnaireRepo;
        private readonly ILogger<QuestionnaireController> logger;
        private readonly int totalQuestions = 6;

        public QuestionnaireController(
                IQuestionnaireRepo questionnaireRepo,
                ILogger<QuestionnaireController> logger
            )
        {
            this.questionnaireRepo = questionnaireRepo;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomQuestionnaire()
        {
            try
            {
                var questions = await questionnaireRepo.GetRandomQuestions();

                if (questions.Any())
                {
                    var randomQuestions = questions.OrderBy(q => random.Next()).Take(totalQuestions).ToList();
                    return Ok(randomQuestions);
                }

                return NotFound("No questions found in the questionnaire.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured during the process, error message : {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }



    }
}