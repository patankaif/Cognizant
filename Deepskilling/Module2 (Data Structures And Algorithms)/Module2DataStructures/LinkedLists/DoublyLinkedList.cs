namespace Module2DataStructures.LinkedLists;

public class DoublyLinkedList<T> where T : notnull
{
    private class Node
    {
        public T Value;
        public Node? Next;
        public Node? Previous;
        public Node(T value) => Value = value;
    }

    private Node? _head;
    private Node? _tail;
    public int Count { get; private set; }

    public void InsertAtEnd(T value)
    {
        var node = new Node(value);
        if (_tail is null)
        {
            _head = node;
            _tail = node;
        }
        else
        {
            node.Previous = _tail;
            _tail.Next = node;
            _tail = node;
        }
        Count++;
    }

    public void InsertAtBeginning(T value)
    {
        var node = new Node(value);
        if (_head is null)
        {
            _head = node;
            _tail = node;
        }
        else
        {
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }
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
        var current = _head;
        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
            {
                if (current.Previous is not null)
                    current.Previous.Next = current.Next;
                else
                    _head = current.Next; 

                if (current.Next is not null)
                    current.Next.Previous = current.Previous;
                else
                    _tail = current.Previous; 

                Count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public List<T> TraverseForward()
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

    public List<T> TraverseBackward()
    {
        var result = new List<T>();
        var current = _tail;
        while (current is not null)
        {
            result.Add(current.Value);
            current = current.Previous;
        }
        return result;
    }

    public override string ToString() => string.Join(" <-> ", TraverseForward());
}

public static class DoublyLinkedListDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Doubly Linked List ---");

        var list = new DoublyLinkedList<int>();
        list.InsertAtEnd(1);
        list.InsertAtEnd(2);
        list.InsertAtEnd(3);
        list.InsertAtBeginning(0);

        Console.WriteLine($"Forward:  {string.Join(" -> ", list.TraverseForward())}");
        Console.WriteLine($"Backward: {string.Join(" -> ", list.TraverseBackward())}");

        list.Delete(2);
        Console.WriteLine($"After Delete(2): {list} (Count={list.Count})");
    }
}
