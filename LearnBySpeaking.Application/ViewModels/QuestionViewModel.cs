using System.Collections.Generic;

namespace LearnBySpeaking.Application.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public char CorrectAnswer { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }
}
