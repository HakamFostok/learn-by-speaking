namespace LearnBySpeaking.Application.ViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public char AnswerLetter { get; set; }
        public string Text { get; set; }

        public bool Selected { get; set; }
        public bool Wrong { get; set; }
    }
}
