namespace Lister.Core.Models;

public class CorrectAnswer
{
    public int CorrectAnswerID { get; set; }
    public int QuestionID { get; set; }
    public int AnswerID { get; set; }

    public Question Question { get; set; }
    public Answer Answer { get; set; }
}
