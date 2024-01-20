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
  
  public class ListNode {
      public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }
  }
  public ListNode ReverseList(ListNode head) {
    if (head == null || head.next == null)
    {
      return head;
    }

    ListNode prev = null;   
    
    while (head != null)
    {
      var nextNode = head.next;
      head.next = prev;
      prev = head;
      head = nextNode;
    }

    return prev;
  }
  
  public ListNode SwapPairs(ListNode head) {
    if (head == null || head.next == null)
    {
      return head;
    }

    // item to return at the end
    var newHead = head.next;

    ListNode prev = null;   
    
    while (head != null && head.next != null)
    {
      if (prev != null)
      {
        prev.next = head.next; // A.n -> C.n (D)
      }
      // keep track of this to be able to continue after the swap
      var nextNode = head.next.next;

      head.next.next = head; // in pair A B, make B point to A
      head.next = nextNode; // A.next is now C (will be swapped in the next loop)

      // head becomes the "previous" node, because it gets moved
      // to the front B A, so it will be the "previous" of the next node C
      prev = head; // A
      head = nextNode; // move to next pair, so the new head is C
      // to avoid odd length issues, point C.next 
    }

    return newHead;
  }
  
  public int PairSum(ListNode head) {
    // fast && slow pointer to find middle of list (fast == null)
    var middleNode = GetMiddleNode(head);

    // Reverse the second part of the list (where slowPointer is at)
    var reversedListHead = ReverseList(middleNode);
    // after reversal, A B C D -> A B D C (A + D, B + C, etc.)

    var aNode = head;
    var bNode = reversedListHead;

    // iterate with new slowPointer = head, and fastPointer = middle
    // continue until fastPointer == null
    var sum = 0;
    while (bNode != null)
    {
      sum = Math.Max(sum, aNode.val + bNode.val);
      aNode = aNode.next;
      bNode = bNode.next;
    }

    return sum;
  }

  ListNode GetMiddleNode(ListNode head)
  {
    var slowPointer = head;
    var fastPointer = head;

    while (fastPointer != null)
    {
      slowPointer = slowPointer.next;
      fastPointer = fastPointer.next.next;
    }

    return slowPointer;
  }
  
  public ListNode ReverseBetween(ListNode head, int left, int right) {
    if (head == null || head.next == null || left == right)
    {
      return head;
    }
    
    ListNode prev = null;
    var curr = 1;
    var currNode = head;
    ListNode sublistHeadPredecessor = null;
    ListNode sublistHead  = null;
    ListNode sublistTailSuccessor  = null;
    while (currNode != null && curr <= right)
    {
      if (sublistHeadPredecessor == null && curr + 1 == left)
      {
        sublistHeadPredecessor = currNode; // in A B C D F (2 4), Stores A for later use
      }
      
      if (curr >= left)
      {
        var nextNode = currNode.next; 
        if (curr + 1 > right)
        {
          sublistTailSuccessor  = nextNode; // In A B C D F (2 4) stores F to connect it later B -> F
        }
        if (sublistHead  == null)
        {
          sublistHead  = currNode; // In A B C D F (2 4) stores B, to connect it to F later B -> F
        }
        
        currNode.next = prev;

        prev = currNode; // In A B C D F, D would be the last Prev
        currNode = nextNode; 
      }
      else
      {
        currNode = currNode.next;
      }
      curr++;
    }
    if (sublistHeadPredecessor != null)
    {
      sublistHeadPredecessor.next = prev; // A -> D
    }
    sublistHead.next = sublistTailSuccessor ; // B -> F
    
    if (left == 1)
    {
      head = prev;
    }
    return head;
  }
    
  public ListNode RemoveNthFromEnd(ListNode head, int n) {
    if (head == null || head.next == null)
    {
      return null;
    }
    var curr = 0;
    var advanceNode = head;
    while (curr < n && advanceNode.next != null)
    {
      advanceNode = advanceNode.next;
      curr++;
    }

    // after being n away from head, get to end of list
    // the slow pointer should be n away from the end
    ListNode prev = null; // keep reference to link nodes later
    var nthAwayNode = head;
    while (advanceNode != null)
    {
      prev = nthAwayNode;
      nthAwayNode = nthAwayNode.next;
      advanceNode = advanceNode.next;
      // keep increasing to check later
      curr++;
    }

    if (curr == n)
    {
      // when no change occurred, we were removing the head
      // there was no movement
      return head.next;
    }

    prev.next = prev.next.next;
    return head;
  }
  
  public ListNode RemoveElements(ListNode head, int val) {
    ListNode newHead = null;
    ListNode prev = null;

    while (head != null)
    {
      if (head.val == val)
      {
        if (head.next == null && prev != null) // end!
        {
          prev.next = null;
        }
        head = head.next;
        continue;
      }
        
      if (newHead == null)
      {
        newHead = head;
      }

      if (prev != null)
      {
        prev.next = head;
      }
      prev = head;

      head = head.next;
    }

    return newHead;
  }
}