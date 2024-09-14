using Microsoft.AspNetCore.Mvc;
using QuizTzy.Models;
using System.Linq;

namespace QuizTzy.Controllers
{
    public class QuizController : Controller
    {
        private static readonly Question[] Questions = new[]
        {
            new Question { Id = 1, Text = "What is 2 + 2?", Options = new[] { "3", "4", "5" }, CorrectOption = 1 },
            new Question { Id = 2, Text = "What is the capital of France?", Options = new[] { "Berlin", "Madrid", "Paris" }, CorrectOption = 2 }
        };

        public IActionResult Index()
        {
            return View(Questions);
        }

        [HttpPost]
        public IActionResult SubmitAnswers(int[] answers)
        {
            var correctAnswers = Questions
                .Select((q, i) => new
                {
                    Question = q,
                    UserAnswer = answers[i],
                    IsCorrect = q.CorrectOption == answers[i]
                })
                .ToArray();

            var correctCount = correctAnswers.Count(a => a.IsCorrect);
            var wrongAnswers = correctAnswers
                .Where(a => !a.IsCorrect)
                .Select(a => new ResultViewModel.WrongAnswer
                {
                    Question = a.Question,
                    UserAnswer = a.UserAnswer
                })
                .ToArray();

            var resultViewModel = new ResultViewModel
            {
                CorrectCount = correctCount,
                TotalCount = Questions.Length,
                WrongAnswers = wrongAnswers
            };

            return View("Result", resultViewModel);
        }
    }
}
