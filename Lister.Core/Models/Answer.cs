namespace Lister.Core.Models;

public class Answer
{
    public int AnswerID { get; set; }
    public int QuestionID { get; set; }
    public string AnswerText { get; set; }

    public Question Question { get; set; }
    public CorrectAnswer? CorrectAnswer { get; set; }
    public List<UserAnswer> UserAnswers { get; set; }
}
