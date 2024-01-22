using dynamic.questionnaire.Models;

namespace dynamic.questionnaire.Repositories
{
    public interface IQuestionnaireRepo
    {
        public Task<List<Question>> GetRandomQuestions();
    }
}
