using LearnBySpeaking.Domain.Models;
using System.Collections.Generic;

namespace LearnBySpeaking.Application.ViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class CreateTestViewModel
    {
        public int TopicId { get; set; }
        public List<TopicViewModel> Topics { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
