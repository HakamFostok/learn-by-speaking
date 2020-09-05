using System.ComponentModel.DataAnnotations.Schema;

namespace LearnBySpeaking.Domain.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public char AnswerLetter { get; set; }
        public bool IsCorrect { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}