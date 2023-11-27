namespace Lister.Core.Models;

public class CorrectAnswer
{
    public int CorrectAnswerID { get; set; }
    public int AnswerID { get; set; }

    public Answer Answer { get; set; }
}
