using QuizGame.Models;
using QuizGame.Services;
using Spectre.Console;

namespace QuizGame.UI;

public class UpdatingUI
{
    private SubjectService subjectService;
    private QuestionService questionService;
    private User user;
    public UpdatingUI(SubjectService subjectService, QuestionService questionService, User user)
    {
        this.subjectService = subjectService;
        this.questionService = questionService;
        this.user = user;

    }
    public void Display()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]--- Update ---[/]")
                    .PageSize(4)
                    .AddChoices(new[]
                    {
                        "Update User", "Update Subject", "Add question",
                        "Go Back"
                    }));
            switch (choice)
            {
                case "Add question":
                    AddQuestion();
                    break;
                case "Update User":
                    UpdateUser1();
                    break;
                case "Update Subject":
                    UpdateSubject();
                    break;
                default:
                    return;
            }
        }
    }

    public void AddQuestion()
    {
        try
        {
            var select = AnsiConsole.Ask<long>("Enter subject id: ");
            var subject = subjectService.Get(select);
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
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            Thread.Sleep(2000);
        }
    }
    public void UpdateUser1()
    {
        var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(4)
                            .AddChoices(new[]
                            {
                                "First name", "Last name", "Phone Number", "User name", "Password", "Update all parametres", "Get Parametres",
                                "Go Back"
                            }));
        switch (choice)
        {
            case "Update all parametres":
                var fname = AnsiConsole.Ask<string>("Enter first name: ");
                var lname = AnsiConsole.Ask<string>("Enter last name: ");
                var phone = AnsiConsole.Ask<string>("Enter phone number: ");
                var username = AnsiConsole.Ask<string>("Enter username: ");
                var password = AnsiConsole.Ask<string>("Enter password: ");
                user.FirstName = fname;
                user.LastName = lname;
                user.PhoneNumber = phone;
                user.UserName = username;
                user.Password = password;
                UserService userService = new UserService();
                userService.Update(user.Id, user);
                break;
            case "First name":
                var firstname = AnsiConsole.Ask<string>("Enter first name: ");
                user.FirstName = firstname;
                UserService userServicefname = new UserService();
                userServicefname.Update(user.Id, user);
                break;
            case "Last name":
                var lastname = AnsiConsole.Ask<string>("Enter first name: ");
                user.FirstName = lastname;
                UserService userServicelname = new UserService();
                userServicelname.Update(user.Id, user);
                break;
            case "Phone Number":
                var phoneNumber = AnsiConsole.Ask<string>("Enter first name: ");
                user.FirstName = phoneNumber;
                UserService userServicephone = new UserService();
                userServicephone.Update(user.Id, user);
                break;
            case "User name":
                var userName = AnsiConsole.Ask<string>("Enter first name: ");
                user.FirstName = userName;
                UserService userServiceuserName = new UserService();
                userServiceuserName.Update(user.Id, user);
                break;
            case "Password":
                var pass = AnsiConsole.Ask<string>("Enter first name: ");
                user.FirstName = pass;
                UserService userServicepass = new UserService();
                userServicepass.Update(user.Id, user);
                break;
            case "Get Parametres":
                AnsiConsole.MarkupLine($"[bold green]First Name: {user.FirstName}[/]");
                AnsiConsole.MarkupLine($"[bold green]Last Name: {user.LastName}[/]");
                AnsiConsole.MarkupLine($"[bold green]Phone Number: {user.PhoneNumber}[/]");
                AnsiConsole.MarkupLine($"[bold green]User Name: {user.UserName}[/]");
                AnsiConsole.MarkupLine($"[bold green]Password: {user.Password}[/]");
                AnsiConsole.MarkupLine("\n");
                AnsiConsole.Markup("press any key ");
                Console.ReadKey();
                break;
        }
    }

    public void UpdateSubject()
    {
        var select = AnsiConsole.Ask<long>("Enter subject id: ");
        try
        {
            var subject = subjectService.Get(select);
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]--- Update Subject ---[/]")
                    .PageSize(4)
                    .AddChoices(new[]
                    {
                        "Update Subject", "Update Question in Subject", "Show Subjects",
                        "Go Back"
                    }));
            switch (choice)
            {
                case "Update Subject":
                    try
                    {
                        var name = AnsiConsole.Ask<string>("Enter new Name: ");
                        var sub = new Subject() { Name = name };
                        subjectService.Update(select, sub);
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");
                        Thread.Sleep(2000);
                    }
                    break;
                case "Update Question in Subject":
                    var questionId = AnsiConsole.Ask<long>("Enter question Id: ");
                    try
                    {
                        var question = questionService.Get(questionId);
                        var questionName = AnsiConsole.Ask<string>("Enter new name of question: ");

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
                            newQuestion.Name = questionName;
                            newQuestion.Answers = answers;
                            questionService.Update(questionId, newQuestion);
                            subjectService.Update(subject.Id, subject);
                        }
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.MarkupLine($"{ex.Message}");
                        Thread.Sleep(2000);
                        return;
                    }
                    break;
                case "Show Subjects":
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
                    AnsiConsole.Markup("\nPress any key");
                    Console.ReadKey();
                    break;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            Thread.Sleep(2000);
            return;
        }
    }
}
