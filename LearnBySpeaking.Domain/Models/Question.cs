using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnBySpeaking.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }

        [ForeignKey(nameof(Topic))]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}