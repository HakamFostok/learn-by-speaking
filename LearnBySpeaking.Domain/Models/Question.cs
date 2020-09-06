using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnBySpeaking.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public char CorrectAnswer { get; set; }
        public List<Answer> Answers { get; set; }

        [ForeignKey(nameof(Test))]
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}