using System.Text.Json;

namespace Ekzakt.Templates.Console.Utilities;

public class ConsoleHelpers
{
    /// <summary>
    /// Write a single line with a message.
    /// </summary>
    /// <param name="message"></param>
    public void Write(string? message = "")
    {
        System.Console.WriteLine(message);
    }


    /// <summary>
    /// Write a line with a message followed by an empty line.
    /// </summary>
    /// <param name="message"></param>
    public void WriteLineAfter(string? message = "")
    {
        System.Console.WriteLine(message);
        System.Console.WriteLine();

    }


    /// <summary>
    /// Writes a Exception in red, followed by an empty line.
    /// </summary>
    /// <param name="ex"></param>
    [Obsolete("Use generic method WriteError<T> instead.")]
    public void WriteError(Exception ex)
    {
        var originalColor = System.Console.ForegroundColor;

        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(WriteJson(ex));
        System.Console.WriteLine();
        System.Console.ForegroundColor = originalColor;
    }


    public void WriteError<T>(T obj) where T : class
    {
        var originalColor = System.Console.ForegroundColor;

        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(WriteJson(obj));
        System.Console.WriteLine();
        System.Console.ForegroundColor = originalColor;
    }


    /// <summary>
    /// Write a message in green, followed by an empty line.
    /// </summary>
    /// <param name="message"></param>
    [Obsolete("Use WriteSuccess<T> instead.")]
    public void WriteResult(string? message)
    {
        var originalColor = System.Console.ForegroundColor;

        System.Console.WriteLine("Result:");
        System.Console.ForegroundColor = ConsoleColor.DarkGreen;
        System.Console.WriteLine(message);
        System.Console.WriteLine();
        System.Console.ForegroundColor = originalColor;
    }



    /// <summary>
    /// Write a message in green, followed by an empty line.
    /// </summary>
    /// <param name="obj"></param>
    public void WriteSuccess<T>(T obj) where T : class
    {
        var originalColor = System.Console.ForegroundColor;

        System.Console.ForegroundColor = ConsoleColor.DarkGreen;
        System.Console.WriteLine(WriteJson(obj));
        System.Console.WriteLine();
        System.Console.ForegroundColor = originalColor;
    }



    /// <summary>
    /// Ask for a confirmation, yes or no. 
    /// Only allows ConsoleKey.Y or ConsoleKey.N.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public bool ConfirmYesNo(string? message = "")
    {
        while (true)
        {
            System.Console.WriteLine($"{message} (Y)es (N)o");
            ConsoleKeyInfo yesNo = System.Console.ReadKey(true);

            Clear();

            if (yesNo.Key == ConsoleKey.N || yesNo.Key == ConsoleKey.Y)
            {
                return yesNo.Key == ConsoleKey.Y;
            }
        }
    }


    /// <summary>
    /// Clears the console window.
    /// </summary>
    public void Clear()
    {
        System.Console.Clear();
    }


    /// <summary>
    /// Write the serialized data of an class, 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="myClass"></param>
    /// <returns></returns>
    public string? WriteJson<T>(T myClass, bool? writeInteded = true) where T : class
    {
        var jsonResult = JsonSerializer.Serialize(myClass, new JsonSerializerOptions
        {
            WriteIndented = writeInteded ?? true
        });

        return jsonResult;
    }


    /// <summary>
    /// Reading input from the console.
    /// </summary>
    /// <returns></returns>
    public string? ReadLine()
    {
        return System.Console.ReadLine();
    }


    /// <summary>
    /// Writes a list of taks prefixed by follow-up letter from the 
    /// alpabet.  Q is preserved for quitting.
    /// </summary>
    /// <param name="taskList"></param>
    /// <returns></returns>
    public ConsoleKeyInfo WriteTaskList(List<string> taskList)
    {
        var alphabet = "ABCDEFGHIJKLMOPRSTUVWXYZ";

        var counter = 0;

        System.Console.Clear();

        foreach (var task in taskList)
        {
            System.Console.WriteLine($"{alphabet.Substring(counter, 1)} = {task}");
            counter++;
        };

        System.Console.WriteLine($"{ConsoleKey.Q} = Quit.");

        var output = System.Console.ReadKey(true);
        return output;
    }
}
