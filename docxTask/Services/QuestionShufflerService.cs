using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using docxTask.DTOs;

#nullable disable
namespace docxTask.Services
{
    public class QuestionShufflerService
    {
        private readonly Random _random;
        public QuestionShufflerService()
        {
            _random = new Random();
        }
        public List<QuestionDto> ShuffleQuestions(string filePath)
        {
            List<QuestionDto> questions = new List<QuestionDto>();

            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, false))
            {
                Body body = doc.MainDocumentPart.Document.Body;

                string currentQuestion = null;
                List<string> currentAnswers = null;

                foreach (var paragraph in body.Elements<Paragraph>())
                {
                    string text = paragraph.InnerText.Trim();

                    // savollikka tekshirish
                    if (text.Any() && char.IsDigit(text.First()) && text.Contains('.'))
                    {
                        // oldingi savolni saqlab ketish
                        if (currentQuestion is not null && currentAnswers is not null)
                        {
                            questions.Add(new QuestionDto
                            {
                                Question = currentQuestion,
                                Answers = currentAnswers
                            });
                        }

                        // yangi savol tuzish
                        currentQuestion = text;
                        currentAnswers = new List<string>();
                    }
                    else if (!string.IsNullOrWhiteSpace(text))
                    {
                        // variantlarni qo'shish
                        currentAnswers.Add(text);
                    }
                }

                if (currentQuestion is not null && currentAnswers is not null)
                {
                    questions.Add(new QuestionDto
                    {
                        Question = currentQuestion,
                        Answers = currentAnswers
                    });
                }
            }

            questions.Shuffle();

            foreach (var question in questions)
            {
                question.Answers.Shuffle();
            }

            return questions;
        }
    }
}