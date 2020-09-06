using System;
using System.Collections.Generic;

namespace LearnBySpeaking.Application.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public TopicViewModel Topic { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }

    public class EvaluateTest
    {
        public int Id { get; set; }
        public List<EvaluateQuestion> Questions { get; set; }
    }

    public class EvaluateQuestion
    {
        public int Id { get; set; }
        public List<EvaluateAnswer> Answers { get; set; }
    }

    public class EvaluateAnswer
    {
        public int Id { get; set; }
        public bool Selected { get; set; }
        public bool Wrong { get; set; }
    }
}