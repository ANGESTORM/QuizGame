using QuizGame.Files;
using QuizGame.Helpers;
using QuizGame.Interfaces;
using QuizGame.Models;

namespace QuizGame.Services;

public class QuestionService : IQuestion
{
    private List<Question> questions;
    private SubjectService subjectService;
    public QuestionService(SubjectService subjectService)
    {
        questions = FileIO<Question>.Read(Constants.QUESTIONS);
        this.subjectService = subjectService;

    }
    public Question Create(Question question)
    {
        if (questions.Count > 0)
            question.Id = questions.Last().Id + 1;
        else
            question.Id = 1;
        subjectService.Update(question.SubjectId, subjectService.Get(question.SubjectId));
        questions.Add(question);
        FileIO<Question>.Write(Constants.QUESTIONS, questions);
        return question;
    }

    public bool Delete(long id)
    {
        var exist = questions.FirstOrDefault(q => q.Id == id);
        if (exist is null)
            throw new Exception($"Question with id {id} is not exists");

        questions.Remove(exist);
        FileIO<Question>.Write(Constants.QUESTIONS, questions);
        return true;
    }

    public Question Get(long id)
    {
        var exist = questions.FirstOrDefault(q => q.Id == id);
        if (exist is null)
            throw new Exception($"Question with id {id} is not exists");

        return exist;
    }

    public List<Question> GetAll()
    {
        return questions;
    }

    public Question Update(long id, Question question)
    {
        var exist = questions.FirstOrDefault(q => q.Id == id);
        if (exist is null)
            throw new Exception($"Question with id {id} is not exists");

        exist.Name = question.Name;
        exist.Answers = question.Answers;
        FileIO<Question>.Write(Constants.QUESTIONS, questions);
        return exist;
    }
}
