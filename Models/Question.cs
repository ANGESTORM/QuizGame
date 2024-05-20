namespace QuizGame.Models;

public class Question
{
    public long Id { get; set; }
    public long SubjectId { get; set; }
    public string Name { get; set; }
    public Dictionary<string, bool> Answers { get; set; }
}
