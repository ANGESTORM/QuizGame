using QuizGame.Models;
using QuizGame.Services;
using Spectre.Console;

namespace QuizGame.UI;

public class QUIZUI
{
    private SubjectService subjectService;
    private QuestionService questionService;
    private User user;
    private UserService userService;
    public QUIZUI(SubjectService subjectService, QuestionService questionService, User user, UserService userService)
    {
        this.subjectService = subjectService;
        this.questionService = questionService;
        this.user = user;
        this.userService = userService;
    }
    public void Display()
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]--- QUIZ---[/]")
                    .PageSize(4)
                    .AddChoices(new[]
                    {
                        "Play QUIZ", "QUIZs",
                        "Go Back"
                    }));
        switch (choice)
        {
            case "Play QUIZ":
                PlayQuiz();
                break;
            case "QUIZs":
                Show();
                break;
            default:
                break;
        }
    }

    public void PlayQuiz()
    {
        var selectSubject = AnsiConsole.Ask<long>("Enter subject Id: ");
        try
        {
            var subject = subjectService.Get(selectSubject);
            var questions = questionService.GetAll().Where(q => q.SubjectId == selectSubject).ToList();
            if (questions.Count() == 0)
            {
                Console.WriteLine("This QUIZ is empty!");
                Thread.Sleep(2000);
                return;
            }
            var falseAnswers = new Dictionary<string, string>();
            while (questions.Count() > 0)
            {
                AnsiConsole.Clear();
                Random random = new Random();
                var rng = random.Next(questions.Count());
                var question = questions.ElementAt(rng);
                AnsiConsole.MarkupLine($"{question.Name}");


                var selection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(4)
                            .AddChoices(new[]
                            {
                                $"{question.Answers.ElementAt(0).Key}", $"{question.Answers.ElementAt(1).Key}", $"{question.Answers.ElementAt(2).Key}",
                            }));

                var trueAnswer = question.Answers.FirstOrDefault(q => q.Value == true);
                if (selection == trueAnswer.Key)
                    user.trueAnswers++;
                else
                {
                    user.falseAnswers++;
                    falseAnswers.Add(question.Name, selection);
                }

                questions.Remove(question);
            }
            foreach (var answer in falseAnswers)
            {
                AnsiConsole.Markup($"[bold green]{answer.Key}[/] | [bold red]{answer.Value}[/]");
            }
            userService.Update(user.Id, user);
            AnsiConsole.Markup("\nPress any key: ");
            Console.ReadKey();
            AnsiConsole.Clear();
        }
        catch (Exception ex)
        {
            AnsiConsole.Markup(ex.ToString());
            Thread.Sleep(2500);
        }
    }
    public void Show()
    {
        var table = new Table().Centered();

        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                table.AddColumn("Id");
                ctx.Refresh();
                Thread.Sleep(600);

                table.AddColumn("Name");
                ctx.Refresh();
                Thread.Sleep(600);
                foreach (var item in subjectService.GetAll())
                {
                    table.AddRow($"{item.Id}", $"{item.Name}");
                    ctx.Refresh();
                    Thread.Sleep(500);
                }
            });
    }
}