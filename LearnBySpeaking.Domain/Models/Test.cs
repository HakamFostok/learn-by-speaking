using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnBySpeaking.Domain.Models
{
    public class Test
    {
        public int Id { get; set; }

        public DateTime CreatedTime { get; set; }

        [ForeignKey(nameof(Topic))]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public List<Question> Questions { get; set; }
    }
}