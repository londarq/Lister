namespace Lister.Core.Models;

public class Test
{
    public int TestId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageSrc { get; set; }
    public int TimeLimitSec { get; set; }

    public List<Question> Questions { get; set; }
    public List<UserTestHistory> UserTestHistory { get; set; }
}
