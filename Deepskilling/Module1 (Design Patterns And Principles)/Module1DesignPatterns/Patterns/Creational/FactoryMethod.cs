namespace Module1DesignPatterns.Patterns.Creational.FactoryMethod;


public interface IDocument
{
    string Render();
}

public class WordDocument : IDocument
{
    public string Render() => "Rendering a Word (.docx) document.";
}

public class PdfDocument : IDocument
{
    public string Render() => "Rendering a PDF (.pdf) document.";
}

public class ExcelDocument : IDocument
{
    public string Render() => "Rendering an Excel (.xlsx) document.";
}

public enum DocumentType { Word, Pdf, Excel }

public abstract class DocumentCreator
{
    public abstract IDocument CreateDocument();

    public string OpenAndRender()
    {
        var document = CreateDocument();
        return document.Render();
    }
}

public class WordDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument() => new WordDocument();
}

public class PdfDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument() => new PdfDocument();
}

public class ExcelDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument() => new ExcelDocument();
}

public static class DocumentFactory
{
    public static IDocument Create(DocumentType type) => type switch
    {
        DocumentType.Word => new WordDocument(),
        DocumentType.Pdf => new PdfDocument(),
        DocumentType.Excel => new ExcelDocument(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown document type.")
    };
}

public static class FactoryMethodDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Factory Method Pattern (creator subclasses) ---");
        DocumentCreator creator = new PdfDocumentCreator();
        Console.WriteLine(creator.OpenAndRender());

        creator = new WordDocumentCreator();
        Console.WriteLine(creator.OpenAndRender());

        Console.WriteLine();
        Console.WriteLine("--- Factory Method Pattern (static factory variant) ---");
        IDocument doc = DocumentFactory.Create(DocumentType.Excel);
        Console.WriteLine(doc.Render());
    }
}
