public class Solution {
  public int[] DailyTemperatures(int[] temperatures) {
    var answer = new int[temperatures.Length];
    var monotonicStack = new Stack<int>();

    for (var i = 0; i < temperatures.Length; i++)
    {
      while (monotonicStack.Count > 0 && temperatures[monotonicStack.Peek()] < temperatures[i])
      {
        var answerIndex = monotonicStack.Pop();
        answer[answerIndex] = i - answerIndex;
      }
      monotonicStack.Push(i);
    }

    return answer;
  }
  
  
  // When we see a number, we no longer care about any numbers in the window smaller than it, because they have no chance of ever being the maximum. 
  // double-ended queue (deque)
  
  
  // monotonic dequeue
  
  public int[] MaxSlidingWindow(int[] nums, int k) {
    var answer = new List<int>();
    // store the indexes
    var maxNums = new LinkedList<int>();

    for (var i = 0; i < nums.Length; i++)
    {
      // monotonic non-increasing deque
      // current number is greater than top number in dequeue
      while (maxNums.Count > 0 && nums[i] > nums[maxNums.Last.Value])
      {
        // remove smaller items
        maxNums.RemoveLast();
      }
      maxNums.AddLast(i);

      // index at the beginning is the one belonging to the
      // max element in the window
      // if we are past the appropriate window size, we remove the first items
      if (maxNums.First.Value + k == i)
      {
        maxNums.RemoveFirst();
      }

      // add to the answers array if our window size is correct
      if (i >= k - 1)
      {
        answer.Add(nums[maxNums.First.Value]);
      }
    }

    return answer.ToArray();
  }
  
  public int LongestSubarray(int[] nums, int limit) {
    var answer = 0;
    // store indexes, not values in the queues
    var increasingDequeue = new LinkedList<int>();
    var decreasingDequeue = new LinkedList<int>();
    var left = 0;
    //window size constraint: maxVal - minVal > limit
    
    for (var right = 0; right < nums.Length; right++)
    {
      // update minValue dequeue. the first item is the min valued one
      while (increasingDequeue.Count > 0 && nums[right] < increasingDequeue.Last.Value)
      {
        // remove bigger items
        increasingDequeue.RemoveLast();
      }
      increasingDequeue.AddLast(nums[right]);

      // same, but opposite check for decreasingDequeue
      while (decreasingDequeue.Count > 0 && nums[right] > decreasingDequeue.Last.Value)
      {
        // remove smaller items
        decreasingDequeue.RemoveLast();
      }
      decreasingDequeue.AddLast(nums[right]);

      // update window size depending on min max constraint
      while (decreasingDequeue.First.Value - increasingDequeue.First.Value > limit)
      {
        // remove the left side
        if (nums[left] == decreasingDequeue.First.Value)
        {
          decreasingDequeue.RemoveFirst();
        }
        if (nums[left] == increasingDequeue.First.Value)
        {
          increasingDequeue.RemoveFirst();
        }
        left++;
      }
      var absDiff = Math.Abs(right - left) + 1;
      answer = Math.Max(answer, absDiff);
    }

    return answer;
  }
}