using dynamic.questionnaire.Models;
using System.Text.Json;

namespace dynamic.questionnaire.Repositories
{
    public class JsonQuestionnaireRepo : IQuestionnaireRepo
    {
        private readonly string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "questionnaire.json");

        public async Task<List<Question>> GetRandomQuestions()
        {
            try
            {
                string json = await File.ReadAllTextAsync(jsonFilePath);

                var questions = JsonSerializer.Deserialize<List<Question>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return questions ?? new List<Question>();
            }
            catch(FileNotFoundException)
            {
                throw new Exception($"File not found with the path {jsonFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
