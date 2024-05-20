namespace QuizGame.Models;

public class User
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int trueAnswers { get; set; } = 0;
    public int falseAnswers { get; set; } = 0;
}
