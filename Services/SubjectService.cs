using QuizGame.Files;
using QuizGame.Helpers;
using QuizGame.Interfaces;
using QuizGame.Models;

namespace QuizGame.Services;

public class SubjectService : ISubject
{
    private List<Subject> subjects;
    public SubjectService()
    {
        subjects = FileIO<Subject>.Read(Constants.SUBJECTS);
    }
    public Subject Create(Subject subject)
    {
        var exist = subjects.FirstOrDefault(s => s.Name == subject.Name);
        if (exist is not null)
            throw new Exception($"Subject with name {subject.Name} is always exists");

        if (subjects.Count > 0)
            subject.Id = subjects.Last().Id + 1;
        else
            subject.Id = 1;
        subjects.Add(subject);
        FileIO<Subject>.Write(Constants.SUBJECTS, subjects);
        return subject;
    }

    public bool Delete(long id)
    {
        var exist = subjects.FirstOrDefault(s => s.Id == id);
        if (exist is null)
            throw new Exception($"Subject with id {id} is not exists");

        subjects.Remove(exist);
        FileIO<Subject>.Write(Constants.SUBJECTS, subjects);
        return true;
    }

    public Subject Get(long id)
    {
        var exist = subjects.FirstOrDefault(s => s.Id == id);
        if (exist is null)
            throw new Exception($"Subject with id {id} is not exists");

        return exist;
    }

    public List<Subject> GetAll()
    {
        return subjects;
    }

    public Subject Update(long id, Subject subject)
    {
        var exist = subjects.FirstOrDefault(s => s.Id == id);
        if (exist is null)
            throw new Exception($"Subject with id {subject.Id} is not exists");

        exist.Name = subject.Name;
        FileIO<Subject>.Write(Constants.SUBJECTS, subjects);
        return exist;
    }
}
