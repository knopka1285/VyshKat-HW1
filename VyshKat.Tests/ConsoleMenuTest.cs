using VyshKat.Services;

namespace VyshKat.Tests;

public class ConsoleMenuTests
{
    private static string RunMenu(string input)
    {
        var fleet = new Fleet(new ServiceCenter());
        var menu = new ConsoleMenu(fleet);
        var output = new StringWriter();
        var originalIn = Console.In;
        var originalOut = Console.Out;

        Console.SetIn(new StringReader(input));
        Console.SetOut(output);

        try
        {
            menu.Show();
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }

        return output.ToString();
    }

    [Fact]
    public void Show_DemoThenReports_PrintsDemoInventory()
    {
        // Arrange
        var input = "6\n3\n4\n5\n7\n0\n";

        // Act
        var output = RunMenu(input);

        // Assert
        Assert.Contains("Отчет", output);
        Assert.Contains("Pro Scooter", output);
    }

    [Fact]
    public void Show_AcceptScooter_ScooterAppearsInInventory()
    {
        // Arrange
        var input = "1\n1\nTestScooter\n500\n7\n1\n5\n0\n";

        // Act
        var output = RunMenu(input);

        // Assert
        Assert.Contains("TestScooter", output);
    }

    [Fact]
    public void Show_AddHelmet_HelmetAppearsInInventory()
    {
        // Arrange
        var input = "2\n1\nTestHelmet\n600\n5\n0\n";

        // Act
        var output = RunMenu(input);

        // Assert
        Assert.Contains("TestHelmet", output);
    }

    [Fact]
    public void Show_UnknownCommand_PrintsError()
    {
        // Arrange
        var input = "abc\n0\n";

        // Act
        var output = RunMenu(input);

        // Assert
        Assert.Contains("такого действия нет", output);
    }
}