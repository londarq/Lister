namespace Lister.Services.Models.ApiModels;

public class TestHistoryApiModel
{
    public int UserID { get; set; }
    public int? TestID { get; set; }
    public DateTime StartTimestamp { get; set; }
    public DateTime FinishTimestamp { get; set; }
    public int Score { get; set; }
}
