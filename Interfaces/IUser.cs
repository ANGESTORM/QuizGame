using QuizGame.Models;

namespace QuizGame.Interfaces;

public interface IUser
{
    public User Create(User user);
    public User Update(long id, User user);
    public bool Delete(long id);
    public User Get(long id);
    public List<User> GetAll();
}
