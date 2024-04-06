namespace docxTask.Entities
{
    public class Question
    {
        public string Text { get; }
        public List<string> Answers { get; set; }

        public Question(string text, List<string> answers)
        {
            Text = text;
            Answers = answers;
        }
    }
}
