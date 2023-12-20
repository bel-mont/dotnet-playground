public class LinkedList
{
  public LinkedListNode? Head;
  public LinkedListNode? Tail;
  public int Count { get; private set; }

  public LinkedList()
  {
    Count = 0;
  }

  public int Get(int index)
  {
    if (index < 0 || index > Count)
    {
      return -1;
    }


    var currentNode = Head;
    for (var i = 0; i <= index; i++)
    {
      if (currentNode == null)
      {
        break;
      }

      if (i == index)
      {
        return currentNode.Value;
      }

      currentNode = currentNode.Next;
    }

    return -1;
  }

  public void AddAtHead(int val)
  {
    AddAtIndex(0, val);
  }

  public void AddAtTail(int val)
  {
    AddAtIndex(Count, val);
  }

  public void AddAtIndex(int index, int val)
  {
    // if index is less than 0 or greater than count do nothing
    if (index < 0 || index > Count)
    {
      return;
    }

    // When the head is null, set both head and tail with the new node
    if (Head == null)
    {
      var node = new LinkedListNode(val);
      Head = node;
      Tail = node;
      Count++;
      return;
    }

    // init previous and current node, both pointing to head at first
    var previousNode = Head;
    var currentNode = Head;


    // init i at 0
    // loop from 0 to <= index
    for (var i = 0; i <= index; i++)
    {
      // if i == index
      if (i == index)
      {
        var newNode = new LinkedListNode(val);
        // we are trying to insert!
        // if my current node is null, we are at the tail. checks for fully empty lists were already done above
        if (currentNode == null)
        {
          // set tail to newNode
          // set previousNode.Next = newNode
          Tail = newNode;
          previousNode.Next = newNode;
        }
        // or if my current node and previous node are the same, we are inserting in the head
        else if (currentNode == previousNode)
        {
          // set newNode.Next to previousNode
          // then Head to newNode
          newNode.Next = previousNode;
          Head = newNode;
        }
        else
        {
          // otherwise, we are in the middle of the list
          // newNode.Next set to currentNode
          newNode.Next = currentNode;
          // previousNode.Next set to newNode
          previousNode.Next = newNode;
        }

        Count++;
        break;
      }
      else
      {
        // if not inserting yet, we continue on to the next loop
        //  i++
        // if previous node is different from current node (meaning we already split em)
        if (previousNode != currentNode)
        {
          // move previousNode to next item
          previousNode = previousNode.Next;
          // we do not advance this until the first iteration passes, to track both pointers
        }

        // set currentNode to currentNode.Next
        currentNode = currentNode.Next;
      }
    }
  }

  public void DeleteAtIndex(int index)
  {
    // do nothing if there are no items, or the index exceeds our current rangegs
    if (index >= Count || index < 0 || Count == 0)
    {
      return;
    }

    // init previous and current node, both pointing to head at first
    var previousNode = Head;
    var currentNode = Head;

    for (var i = 0; i <= index; i++)
    {
      // if i == index we are deleting!
      if (i == index)
      {
        //if previous and current are the same, we are deleting the head
        if (previousNode == currentNode)
        {
          // set Head to currentNode.Next
          Head = currentNode.Next;
          // if the head is now null, it means we have deleted everything!
          if (Head == null)
          {
            // so, clear the tail as well
            Tail = null;
          }
        }
        else
        {
          // we are deleting non-head items
          previousNode.Next = currentNode.Next;
        }

        Count--;
        break;
      }
      // else we move on to the next item
      else
      {
        // if previous node is different from current node (meaning we already split em)
        if (previousNode != currentNode)
        {
          // move previousNode to next item
          previousNode = previousNode.Next;
          // we do not advance this until the first iteration passes, to track both pointers
        }

        // set currentNode to currentNode.Next
        currentNode = currentNode.Next;
      }
    }
  }
}

public class LinkedListNode
{
  public LinkedListNode? Next;
  public int Value { get; set; }

  public LinkedListNode(int value)
  {
    Value = value;
  }
  
  public LinkedListNode DetectCycle(LinkedListNode head) {
    if (head == null || head.Next == null || head.Next.Next == null)
    {
      return null;
    }
      
    // keep a slow pointer and a fast pointer
    var slowPointer = head;
    var fastPointer = slowPointer;
    // iterate until the fast pointer reaches null OR it equals the slow pointer
    var cycleFound = false;
    while (fastPointer.Next != null && fastPointer.Next.Next != null)
    {
      slowPointer = slowPointer.Next;
      fastPointer = fastPointer.Next.Next;
          
      // if both pointers point to the same object, we have a cycle
      if (fastPointer == slowPointer)
      {
        cycleFound = true;
        break;
      }
    }
    // after cycle is found
    // start a second loop, starting from the slow pointer
    // make another pointer that moves at double the speed
    // when both slow pointers meet, we found the cycle origin
    if (cycleFound)
    {
      var verifierPointer = head;
      while (true)
      {
        if (verifierPointer == slowPointer)
        {
          return slowPointer;
        }
        slowPointer = slowPointer.Next;
        verifierPointer = verifierPointer.Next;
      }
    }
    return null;
  }
}