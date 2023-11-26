namespace Lister.Core.Models;

public class UserTestHistory
{
    public int UserTestHistoryID { get; set; }
    public int UserID { get; set; }
    public int? TestID { get; set; }
    public DateTime StartTimestamp { get; set; }
    public DateTime FinishTimestamp { get; set; }
    public int Score { get; set; }

    public User User { get; set; }
    public Test? Test { get; set; }
}
