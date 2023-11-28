namespace Lister.Core.Models;

public class User
{
    public int UserID { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }

    public List<UserAnswer> UserAnswers { get; set; }
    public List<UserTestHistory> UserTests { get; set; }
}
