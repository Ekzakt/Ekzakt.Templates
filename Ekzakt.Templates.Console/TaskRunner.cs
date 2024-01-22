using Ekzakt.Templates.Console.Utilities;

namespace EmailTemplateProvider.Console;

public class TaskRunner(ConsoleHelpers? c)
{
    public async Task DoSomething()
    {
        c.Clear();

        while (true)
        {
            c.Write($"Doing '{nameof(DoSomething)}'.");
            c.Write();

            if (!c.ConfirmYesNo("Would you like to try again?")) break;
        }
    }
}

