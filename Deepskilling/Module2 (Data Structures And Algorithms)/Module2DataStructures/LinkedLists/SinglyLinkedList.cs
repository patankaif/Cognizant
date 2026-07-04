namespace Module2DataStructures.LinkedLists;


public class SinglyLinkedList<T> where T : notnull
{
    private class Node
    {
        public T Value;
        public Node? Next;
        public Node(T value) => Value = value;
    }

    private Node? _head;
    private Node? _tail;
    public int Count { get; private set; }

    public void InsertAtEnd(T value)
    {
        var node = new Node(value);
        if (_head is null)
        {
            _head = node;
            _tail = node;
        }
        else
        {
            _tail!.Next = node;
            _tail = node;
        }
        Count++;
    }

    public void InsertAtBeginning(T value)
    {
        var node = new Node(value) { Next = _head };
        _head = node;
        _tail ??= node;
        Count++;
    }
    public bool Search(T value)
    {
        var current = _head;
        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
                return true;
            current = current.Next;
        }
        return false;
    }

    public bool Delete(T value)
    {
        Node? previous = null;
        var current = _head;

        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
            {
                if (previous is null)
                    _head = current.Next;      
                else
                    previous.Next = current.Next;

                if (current == _tail)
                    _tail = previous;         

                Count--;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }

    public List<T> Traverse()
    {
        var result = new List<T>();
        var current = _head;
        while (current is not null)
        {
            result.Add(current.Value);
            current = current.Next;
        }
        return result;
    }

    public override string ToString() => string.Join(" -> ", Traverse());
}

public static class SinglyLinkedListDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Singly Linked List ---");

        var list = new SinglyLinkedList<int>();
        list.InsertAtEnd(10);
        list.InsertAtEnd(20);
        list.InsertAtEnd(30);
        list.InsertAtBeginning(5);

        Console.WriteLine($"List: {list} (Count={list.Count})");
        Console.WriteLine($"Search(20) => {list.Search(20)}");
        Console.WriteLine($"Search(99) => {list.Search(99)}");

        list.Delete(20);
        Console.WriteLine($"After Delete(20): {list} (Count={list.Count})");

        list.Delete(5); 
        Console.WriteLine($"After Delete(5) [head]: {list} (Count={list.Count})");
    }
}
