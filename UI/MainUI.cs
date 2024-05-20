using QuizGame.Models;
using QuizGame.Services;
using Spectre.Console;

namespace QuizGame.UI;

public class MainUI
{
    private CreateUI createUI;
    private QUIZUI quizUI;
    private SubjectService subjectService;
    private QuestionService questionService;
    private User user;
    private UserService userService;
    private UpdatingUI updatingUI;
    public MainUI(User user, UserService userService)
    {
        this.userService = userService;
        this.subjectService = new SubjectService();
        this.questionService = new QuestionService(subjectService);
        this.createUI = new CreateUI(subjectService, questionService);
        this.user = user;
        this.quizUI = new QUIZUI(subjectService, questionService, user, userService);
        this.updatingUI = new UpdatingUI(subjectService, questionService, user);
    }
    public void Display()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]--- QUIZ ---[/]")
                    .PageSize(4)
                    .AddChoices(new[]
                    {
                        "QUIZ list", "Create QUIZ", "Update Menu", "My Progress",
                        "Go Back"
                    }));
            switch (choice)
            {
                case "QUIZ list":
                    quizUI.Display();
                    break;
                case "Create QUIZ":
                    createUI.Display();
                    break;
                case "Update Menu":
                    updatingUI.Display();
                    break;
                case "My Progress":
                    AnsiConsole.Write(new BarChart()
                        .Width(60)
                        .Label("[blue bold underline]My Progress[/]")
                        .CenterLabel()
                        .AddItem("True Answers", user.trueAnswers, Color.Green)
                        .AddItem("False Answers", user.falseAnswers, Color.Red));
                    Console.ReadKey();
                    break;
                default:
                    break;
            }
        }
    }
}
