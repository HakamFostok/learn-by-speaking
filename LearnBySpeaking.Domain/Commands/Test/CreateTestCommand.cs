using LearnBySpeaking.Domain.Models;
using System.Collections.Generic;

namespace LearnBySpeaking.Domain.Commands.Test
{
    public class CreateTestCommand
    {
        public int TopicId { get; set; }

        public List<Question> Questions { get; set; }
    }
}
