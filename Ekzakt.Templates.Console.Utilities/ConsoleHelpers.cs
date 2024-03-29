﻿using System.Runtime.Versioning;
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
    /// <param name="obj">Generic</param>
    public void WriteError<T>(T obj) where T : class
    {
        var originalColor = System.Console.ForegroundColor;

        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(WriteJson(obj));
        System.Console.WriteLine();
        System.Console.ForegroundColor = originalColor;
    }


    /// <summary>
    /// Writes a Exception in red, followed by an empty line.
    /// </summary>
    /// <param name="error">string</param>
    public void WriteError(string error)
    {
        var originalColor = System.Console.ForegroundColor;

        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(error);
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
    public bool ConfirmYesNo(string? message = "", bool clearConsole = false)
    {
        while (true)
        {
            System.Console.WriteLine($"{message} (Y)es (N)o");
            ConsoleKeyInfo yesNoCancel = System.Console.ReadKey(true);

            if (clearConsole)
            {
                Clear();
            }

            if (yesNoCancel.Key == ConsoleKey.N || yesNoCancel.Key == ConsoleKey.Escape || yesNoCancel.Key == ConsoleKey.Y)
            {
                return yesNoCancel.Key == ConsoleKey.Y;
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
    /// <param name="obj">Generic</param>
    /// <param name="writeInteded"></param>
    /// <returns></returns>
    public string? WriteJson<T>(T obj, bool? writeInteded = true) where T : class
    {
        var jsonResult = JsonSerializer.Serialize(obj, new JsonSerializerOptions
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
    /// Writes a list of menu items (taks) prefixed by follow-up letter from the 
    /// alpabet.  Q is preserved for quitting or going to the previous menu.
    /// </summary>
    /// <param name="menuItems"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    public ConsoleKeyInfo WriteMenu(List<string> menuItems, string? title = null)
    {
        if (menuItems.Count > 26)
        {
            WriteError("A maximum of 26 menu items are allowed.");
        }

        System.Console.Clear();

        if (title != null)
        {
            var previousColor = System.Console.ForegroundColor;

            System.Console.ForegroundColor= ConsoleColor.Blue;
            System.Console.WriteLine(title);
            System.Console.WriteLine();
            System.Console.ForegroundColor = previousColor;
        }

        var alphabet = "ABCDEFGHIJKLMOPQRSTUVWXYZ";
        var counter = 0;

        foreach (var task in menuItems)
        {
            System.Console.WriteLine($"{alphabet.Substring(counter, 1)} = {task}");
            counter++;
        };

        var output = System.Console.ReadKey(true);
        return output;
    }


    /// <summary>
    /// Writes a list of taks prefixed by follow-up letter from the 
    /// alpabet.  Q is preserved for quitting.
    /// </summary>
    /// <param name="taskList"></param>
    /// <returns></returns>
    [Obsolete("Use write menu instead.")]
    public ConsoleKeyInfo WriteTaskList(List<string> taskList)
    {
        if (taskList.Count > 25)
        {
            WriteError("Tasklist can contain upto 25 taks maximum.");
        }

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
}
