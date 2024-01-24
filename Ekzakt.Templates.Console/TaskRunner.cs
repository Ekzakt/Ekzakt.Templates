using Ekzakt.Templates.Console.Utilities;

namespace Ekzakt.Templates.Console;

public class TaskRunner(ConsoleHelpers c)
{
    public async Task DoSomethingAsync()
    {
        c.Clear();

        while (true)
        {
            c.WriteError("WriteError");
            c.WriteSuccess("WriteSuccess");

            c.Write($"Doing '{nameof(DoSomethingAsync)}'.");
            c.Write();

            if (!c.ConfirmYesNo("Would you like to try again?")) break;
        }
    }
}

