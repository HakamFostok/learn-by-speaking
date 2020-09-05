using System.Collections.Generic;

namespace LearnBySpeaking.Application.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public int Text { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}
