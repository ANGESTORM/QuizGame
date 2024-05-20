using QuizGame.Models;

namespace QuizGame.Interfaces;

public interface IQuestion
{
    public Question Create(Question question);
    public Question Update(long id, Question question);
    public bool Delete(long id);
    public Question Get(long id);
    public List<Question> GetAll();
}
