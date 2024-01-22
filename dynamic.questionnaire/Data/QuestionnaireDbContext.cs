using dynamic.questionnaire.Models;
using Microsoft.EntityFrameworkCore;

namespace dynamic.questionnaire.Data
{
    public class QuestionnaireDbContext : DbContext
    {
        public QuestionnaireDbContext(DbContextOptions<QuestionnaireDbContext> options) : base(options)
        {

        }

        public DbSet<Question> Questions  => Set<Question>();
    }
}
