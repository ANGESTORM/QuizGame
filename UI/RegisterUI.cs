using QuizGame.Models;
using QuizGame.Services;
using Spectre.Console;

namespace QuizGame.UI;

public class RegisterUI
{
    private UserService userService;
    private MainUI mainUI;
    private User user;
    public RegisterUI()
    {
        userService = new UserService();
    }
    public void Display()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]--- Sign In / Sign Up ---[/]")
                    .PageSize(4)
                    .AddChoices(new[]
                    {
                        "Sign In", "Sign Up",
                        "Exit",
                    }));

            switch (choice)
            {
                case "Sign In":
                    try
                    {
                        var username = AnsiConsole.Ask<string>("Enter the username: ");
                        var users = userService.GetAll();
                        var user1 = users.FirstOrDefault(u => u.UserName == username);
                        if (user1 is null)
                        {
                            AnsiConsole.MarkupLine("[bold red]This user is not found[/]");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            this.user = user1;
                            mainUI = new MainUI(user, userService);
                            var password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password").Secret());
                            if (password != user.Password)
                            {
                                AnsiConsole.Markup("Incorrect password!\n");
                                Console.WriteLine("Press any button...");
                                Console.ReadKey();
                            }
                            else
                            {
                                mainUI.Display();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");
                        Thread.Sleep(2000);
                    }
                    break;
                case "Sign Up":
                    var firstName = AnsiConsole.Ask<string>("Your first name");
                    var lastName = AnsiConsole.Ask<string>("Your Last name");
                    var phone = AnsiConsole.Ask<string>("Phone number");
                    var userName = AnsiConsole.Ask<string>("User Name");
                    var userUp = userService.GetAll().FirstOrDefault(u => u.UserName == userName);
                    while (userUp is not null)
                    {
                        AnsiConsole.Markup($"This user with username {userName} is always exists, please enter other user Name\n");
                        userName = AnsiConsole.Ask<string>("User Name");
                    }
                    var pass = AnsiConsole.Prompt(new TextPrompt<string>("Enter password").Secret());
                    var newUser = new User();
                    newUser.FirstName = firstName;
                    newUser.LastName = lastName;
                    newUser.UserName = userName;
                    newUser.PhoneNumber = phone;
                    newUser.Password = pass;
                    userService.Create(newUser);
                    mainUI.Display();
                    break;
                default:
                    return;
            }
        }
    }
}
