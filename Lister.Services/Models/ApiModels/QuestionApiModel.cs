namespace Lister.Services.Models.ApiModels;

public class QuestionApiModel
{
    public int QuestionID { get; set; }
    public string QuestionText { get; set; }
    public string? MediaLink { get; set; }

    public List<AnswerApiModel> Answers { get; set; }
}
