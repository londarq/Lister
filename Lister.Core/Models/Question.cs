namespace Lister.Core.Models;

public class Question
{
    public int QuestionID { get; set; }
    public int TestID { get; set; }
    public string QuestionText { get; set; }
    public string? MediaLink { get; set; }

    public Test Test { get; set; }
    public List<Answer> Answers { get; set; }
    public List<UserAnswer> UserAnswers { get; set; }
    public List<CorrectAnswer> CorrectAnswers { get; set; }
}
