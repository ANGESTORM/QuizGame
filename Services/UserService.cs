using QuizGame.Files;
using QuizGame.Helpers;
using QuizGame.Interfaces;
using QuizGame.Models;
namespace QuizGame.Services;

public class UserService : IUser
{
    private List<User> users;
    public UserService()
    {
        this.users = FileIO<User>.Read(Constants.USERS);
    }
    public User Create(User user)
    {
        var exist = users.FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber);
        if (exist is not null)
            throw new Exception($"User with phone number {user.PhoneNumber} is always exists");

        if (users.Count > 0)
            user.Id = users.Last().Id + 1;
        else
            user.Id = 1;
        users.Add(user);
        FileIO<User>.Write(Constants.USERS, users);
        return user;
    }

    public bool Delete(long id)
    {
        var exist = users.FirstOrDefault(u => u.Id == id);
        if (exist is null)
            throw new Exception($"User with id {id} is not exists");

        users.Remove(exist);
        FileIO<User>.Write(Constants.USERS, users);
        return true;
    }

    public User Get(long id)
    {
        var exist = users.FirstOrDefault(u => u.Id == id);
        if (exist is null)
            throw new Exception($"User with id {id} is not exists");

        return exist;
    }

    public List<User> GetAll()
    {
        return users;
    }

    public User Update(long id, User user)
    {
        var exist = users.FirstOrDefault(u => u.Id == id);
        if (exist is null)
            throw new Exception($"User with id {id} is not exists");

        exist.PhoneNumber = user.PhoneNumber;
        exist.FirstName = user.FirstName;
        exist.LastName = user.LastName;
        exist.UserName = user.UserName;
        FileIO<User>.Write(Constants.USERS, users);
        return exist;
    }
}
