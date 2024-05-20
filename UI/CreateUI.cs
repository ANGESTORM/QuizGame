using QuizGame.Models;
using QuizGame.Services;
using Spectre.Console;

namespace QuizGame.UI;
public class CreateUI
{
    private SubjectService subjectService;
    private QuestionService questionService;
    public CreateUI(SubjectService subjectService, QuestionService questionService)
    {
        this.subjectService = subjectService;
        this.questionService = questionService;

    }
    public void Display()
    {
        var name = AnsiConsole.Ask<string>("Enter QUIZ name: ");
        var subject = new Subject();
        subject.Name = name;
        try
        {
            subjectService.Create(subject);
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup(ex.ToString());
            AnsiConsole.MarkupLine("");
            Thread.Sleep(2500);
            return;
        }
        int i = 1;
        bool cont = true;
        while (cont)
        {
            AnsiConsole.Clear();
            var question = AnsiConsole.Ask<string>($"{i} - question: ");
            var answers = new Dictionary<string, bool>();
            int countTrues = 0;
            for (int j = 0; j < 3; j++)
            {
                var answer = AnsiConsole.Ask<string>($"{j + 1} - answer: ");
                var isTrue = AnsiConsole.Ask<bool>("Is this answer true: (true, false)");
                if (isTrue == true)
                    countTrues++;

                answers.Add(answer, isTrue);
            }
            if (countTrues == 0 || countTrues > 1)
                AnsiConsole.MarkupLine("You have entered more than 1 true answers");
            else
            {
                var newQuestion = new Question();
                newQuestion.Name = question;
                newQuestion.Answers = answers;
                newQuestion.SubjectId = subject.Id;
                questionService.Create(newQuestion);
                subjectService.Update(subject.Id, subject);
            }
            cont = AnsiConsole.Ask<bool>("Do you want to add question? (true, false): ");
            i++;
        }
    }
}
