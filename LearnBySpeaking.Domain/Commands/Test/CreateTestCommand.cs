using LearnBySpeaking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnBySpeaking.Domain.Commands.Test
{
    public class CreateTestCommand
    {
        public int TopicId { get; set; }

        public List<Question> Questions { get; set; }
    }
}
