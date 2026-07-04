namespace Module1DesignPatterns.Patterns.Behavioral.Command;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class TextDocument
{
    public System.Text.StringBuilder Content { get; } = new();

    public void Append(string text) => Content.Append(text);

    public void RemoveLast(int length)
    {
        if (length > 0 && length <= Content.Length)
            Content.Remove(Content.Length - length, length);
    }

    public override string ToString() => Content.ToString();
}

public class AppendTextCommand : ICommand
{
    private readonly TextDocument _document;
    private readonly string _textToAppend;

    public AppendTextCommand(TextDocument document, string textToAppend)
    {
        _document = document;
        _textToAppend = textToAppend;
    }

    public void Execute() => _document.Append(_textToAppend);

    public void Undo() => _document.RemoveLast(_textToAppend.Length);
}

public class CommandInvoker
{
    private readonly Stack<ICommand> _history = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_history.Count == 0)
        {
            Console.WriteLine("Nothing to undo.");
            return;
        }

        var command = _history.Pop();
        command.Undo();
    }
}

public static class CommandDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Command Pattern ---");

        var document = new TextDocument();
        var invoker = new CommandInvoker();

        invoker.ExecuteCommand(new AppendTextCommand(document, "Hello"));
        invoker.ExecuteCommand(new AppendTextCommand(document, ", World"));
        invoker.ExecuteCommand(new AppendTextCommand(document, "!"));
        Console.WriteLine($"Document: \"{document}\"");

        invoker.UndoLastCommand(); 
        Console.WriteLine($"After undo: \"{document}\"");

        invoker.UndoLastCommand();
        Console.WriteLine($"After undo: \"{document}\"");
    }
}
