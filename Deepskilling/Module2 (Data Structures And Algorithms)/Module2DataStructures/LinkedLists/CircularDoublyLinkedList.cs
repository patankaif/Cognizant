namespace Module2DataStructures.LinkedLists;


public class CircularDoublyLinkedList<T> where T : notnull
{
    private class Node
    {
        public T Value;
        public Node? Next;
        public Node? Previous;
        public Node(T value) => Value = value;
    }

    private Node? _head;
    public int Count { get; private set; }

    public void InsertAtEnd(T value)
    {
        var node = new Node(value);
        if (_head is null)
        {
            node.Next = node;
            node.Previous = node;
            _head = node;
        }
        else
        {
            var tail = _head.Previous!;
            node.Previous = tail;
            node.Next = _head;
            tail.Next = node;
            _head.Previous = node;
        }
        Count++;
    }

    public void InsertAtBeginning(T value)
    {
        InsertAtEnd(value);
        _head = _head!.Previous; 
    }

    public bool Search(T value)
    {
        if (_head is null) return false;

        var current = _head;
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
        if (_head is null) return false;

        var current = _head;
        for (int i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(current!.Value, value))
            {
                if (Count == 1)
                {
                    _head = null;
                }
                else
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                    if (current == _head)
                        _head = current.Next;
                }
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
        if (_head is null) return result;

        var current = _head;
        for (int i = 0; i < Count; i++)
        {
            result.Add(current!.Value);
            current = current.Next;
        }
        return result;
    }

    public List<T> TraverseBackward()
    {
        var result = new List<T>();
        if (_head is null) return result;

        var current = _head.Previous; 
        for (int i = 0; i < Count; i++)
        {
            result.Add(current!.Value);
            current = current.Previous;
        }
        return result;
    }

    public override string ToString() => Count == 0
        ? "(empty)"
        : string.Join(" <-> ", TraverseForward()) + " <-> (back to head)";
}

public static class CircularDoublyLinkedListDemo
{
    public static void Run()
    {
        Console.WriteLine("--- Circular Doubly Linked List ---");

        var list = new CircularDoublyLinkedList<string>();
        list.InsertAtEnd("Mon");
        list.InsertAtEnd("Tue");
        list.InsertAtEnd("Wed");
        list.InsertAtBeginning("Sun");

        Console.WriteLine($"Forward:  {list}");
        Console.WriteLine($"Backward: {string.Join(" -> ", list.TraverseBackward())}");

        list.Delete("Tue");
        Console.WriteLine($"After Delete(\"Tue\"): {list} (Count={list.Count})");
    }
}
