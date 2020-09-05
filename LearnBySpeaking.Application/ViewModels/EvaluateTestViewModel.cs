using System.Collections.Generic;

namespace LearnBySpeaking.Application.ViewModels
{
    public class EvaluateTestViewModel
    {
        public int TestId { get; set; }

        public List<QuestionAnswerViewModel> Answers { get; set; }
    }
}
