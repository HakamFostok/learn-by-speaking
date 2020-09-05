using System;

namespace LearnBySpeaking.Application.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public TopicViewModel Topic { get; set; }
    }
}