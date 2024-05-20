using QuizGame.Models;

namespace QuizGame.Interfaces;

public interface ISubject
{
    public Subject Create(Subject subject);
    public Subject Update(long id, Subject subject);
    public bool Delete(long id);
    public Subject Get(long id);
    public List<Subject> GetAll();
}
