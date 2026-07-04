namespace Module2DataStructures.LinkedLists;

public class CircularSinglyLinkedList<T> where T : notnull
{
    private class Node
    {
        public T Value;
        public Node? Next;
        public Node(T value) => Value = value;
    }

    private Node? _tail; 
    public int Count { get; private set; }

    public void InsertAtEnd(T value)
    {
        var node = new Node(value);
        if (_tail is null)
        {
            node.Next = node;   
            _tail = node;
        }
        else
        {
            node.Next = _tail.Next;
            _tail.Next = node;      
            _tail = node;           
        }
        Count++;
    }

    public void InsertAtBeginning(T value)
    {
        var node = new Node(value);
        if (_tail is null)
        {
            node.Next = node;
            _tail = node;
        }
        else
        {
            node.Next = _tail.Next; 
            _tail.Next = node;      
        }
        Count++;
    }

    public bool Search(T value)
    {
        if (_tail is null) return false;

        var current = _tail.Next; 
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(current!.Value, value))
                return true;
            current = current.Next;
        }
        return false;
    }

    public bool Delete(T value)
    {
        if (_tail is null) return false;

        Node? previous = _tail;
        var current = _tail.Next;

        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(current!.Value, value))
            {
                if (current == _tail && current == previous)
                {
                    _tail = null;
                }
                else
                {
                    previous!.Next = current.Next;
                    if (current == _tail)
                        _tail = previous;
                }
                Count--;
                return true;
            }
            previous = current;
            current = current!.Next;
        }
        return false;
    }
    public List<T> Traverse()
    {
        var result = new List<T>();
        if (_tail is null) return result;

        var current = _tail.Next; 
        for (int i = 0; i < Count; i++)
        {
            result.Add(current!.Value);
            current = current.Next;
        }
        return result;
    }

    public override string ToString() => Count == 0
        ? "(empty)"
        : string.Join(" -> ", Traverse()) + " -> (back to head)";
}

public static class CircularSinglyLinkedListDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Circular Singly Linked List ---");

        var list = new CircularSinglyLinkedList<string>();
        list.InsertAtEnd("Alice");
        list.InsertAtEnd("Bob");
        list.InsertAtEnd("Carol");
        list.InsertAtBeginning("Zoe");

        Console.WriteLine($"List: {list} (Count={list.Count})");
        Console.WriteLine($"Search(\"Bob\") => {list.Search("Bob")}");

        list.Delete("Bob");
        Console.WriteLine($"After Delete(\"Bob\"): {list} (Count={list.Count})");

        Console.WriteLine("(In a real round-robin scheduler you'd keep walking");
        Console.WriteLine(" `current = current.Next` forever, cycling through turns.)");
    }
}
