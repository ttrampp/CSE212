using System.Collections;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        // TODO Problem 1

        //basically if the list is empty, it starts the list. if not, it connects the new node at the end and updates the tail pointer

        //Create a new node with the given value
        Node newNode = new(value);
        //Check to see if the list is empty(both head & tail will be null)
        if (_tail is null)
        {
            //when the list is empty, it is the only node, so it becomes both the head and the tail
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            //list is not empty, link the current tail to the new tail
            _tail.Next = newNode;
            //link the new back to the current tail
            newNode.Prev = _tail;
            //update the tail reference to the new node
            _tail = newNode;
        }
    }


    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // TODO Problem 2

        //Remove the last node(tail) of the doubly-linked list

        //check if list is empty or has only one item
        if (_head == _tail)
        {
            //set both head and tail to null--empty the list
            _head = null;
            _tail = null;
        }
        //if there are multiple nodes...
        else if (_tail is not null)
        {
            //move the tail pointer one node back
            _tail = _tail.Prev;
            //disconnect the old tail
            _tail!.Next = null;
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        // TODO Problem 3
        //start at the head of the list
        Node? curr = _head;
        //traverse through the list to find the value
        while (curr is not null)
        {
            //if current node holds the value we are looking for
            if (curr.Data == value)
            {
                //it's in the head
                if (curr == _head)
                {
                    RemoveHead();
                }
                //if it it's in the tail
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                //If it's in the middle
                else
                {
                    //link the previous node to the next node
                    curr.Prev!.Next = curr.Next;
                    //link the next node back to the previous node
                    curr.Next!.Prev = curr.Prev;
                }
                //stop after removing the first matching node
                return;
            }
            //move to the next node
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        // TODO Problem 4

        //start at the beginning of the list
        Node? curr = _head;

        //walk through the entire list
        while (curr is not null)
        {
            //if we find a node with the value to replace
            if (curr.Data == oldValue)
            {
                //then replace the value with the new one
                curr.Data = newValue;
            }
            //move to the next node
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse()
    {
        // TODO Problem 5
        //yield return 0; // replace this line with the correct yield return statement(s)

        //start at the end of the list
        Node? curr = _tail;
        //keep going backward until there are no more nodes
        while (curr is not null)
        {
            //give the value to the caller
            yield return curr.Data;
            //move to the previous node
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}