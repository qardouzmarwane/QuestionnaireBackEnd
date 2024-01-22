using dynamic.questionnaire.Data;
using dynamic.questionnaire.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace dynamic.questionnaire.Repositories
{
    public class SqlQuestionnaireRepo : IQuestionnaireRepo
    {
        private readonly string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "questionnaire.json");
        private readonly QuestionnaireDbContext _dbContext;

        public SqlQuestionnaireRepo(QuestionnaireDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<List<Question>> GetRandomQuestions()
        {
            try
            {
                if(!_dbContext.Questions.ToList().Any())
                {
                    await AddQuestionsFromJson(jsonFilePath);
                }

                var questionaire = await _dbContext.Questions.Include(q => q.ObjectOptions).ToListAsync();
                return questionaire;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddQuestionsFromJson(string jsonFilePath)
        {
            var json = await File.ReadAllTextAsync(jsonFilePath);

            var questions = JsonSerializer.Deserialize<List<Question>>(json);

            questions = questions?.Select(q => 
                { 
                    q.ObjectOptions = q.Options?.Select(o => new Option { OptionText = o }).ToList(); 
                    return q; 
                }
            ).ToList();

            if (questions != null)
            {
                foreach (var q in questions)
                {
                    await _dbContext.Questions.AddAsync(q);
                }

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
