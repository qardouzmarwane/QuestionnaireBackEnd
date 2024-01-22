using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dynamic.questionnaire.Models
{
    public class Question
    {
        [Key]
        [JsonIgnore]
        public int QuestionId { get; set; }
        
        [Required]
        public string QuestionText { get; set; } = "";
        
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public QuestionType QuestionType { get; set; }

        [NotMapped]
        public List<string>? Options
        {
            get => ObjectOptions != null ? ObjectOptions.Select(q => q.OptionText).ToList() : _options;
            set => _options = value;
        }

        [JsonIgnore]
        public List<Option>? ObjectOptions { get; set; }

        [NotMapped]
        [JsonIgnore]
        private List<string>? _options;
    }

    public class Option
    {
        [Key]
        public int OptionId { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [JsonIgnore]
        public Question Question { get; set; } = new();

        public string OptionText { get; set; } = "";
    }

    public enum QuestionType
    {
        Checkbox,
        InputText,
        SelectList
    }
}
