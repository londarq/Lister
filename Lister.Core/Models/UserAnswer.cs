﻿namespace Lister.Core.Models;

public class UserAnswer
{
    public int UserAnswerID { get; set; }
    public int UserID { get; set; }
    public int SelectedAnswerID { get; set; }

    public User User { get; set; }
    public Answer SelectedAnswer { get; set; }
}
