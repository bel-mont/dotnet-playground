namespace ConsoleApp1;

public class SlidingWindow
{
  public double FindMaxAverage(int[] nums, int k) {
    if (nums.Length == 1)
    {
      return nums[0];
    }
    double currentSum = 0;
    double maxAverage = 0;
    // get average of first k items
    for (var i = 0; i < k; i++)
    {
      currentSum += nums[i];
    }
    maxAverage = currentSum / k;
      
    // Then loop the rest of the array, sliding with a k sized window
    for (var i = k; i < nums.Length; i++)
    {
      // add item on the right, remove item on the left
      // nums[i - k] to remove the item furthest to the left of the current window
      currentSum += nums[i] - nums[i - k];
      // then get average
      double average = currentSum / k;
      // then compare to previous maxAverage
      maxAverage = Math.Max(maxAverage, average);
    }
    // Check edge cases later
    return maxAverage;
  }
  
  public int LongestOnes(int[] nums, int k) {
    // create a window that increases in size until it finds more than k amount of 0s
    var maxConsecutiveOnes = 0;
    var zerosInCurrentWindow = 0;
    var j = 0;
    for (var i = 0; i < nums.Length; i++)
    {
      if (nums[i] == 0)
      {
        zerosInCurrentWindow++;
      }
      while (zerosInCurrentWindow > k)
      {
        if (nums[j] == 0)
        {
          zerosInCurrentWindow--;
        }
        j++;
      }
      maxConsecutiveOnes = Math.Max(maxConsecutiveOnes, i - j + 1);
    }
      
    return maxConsecutiveOnes;
  }
  
  public int MaxVowels(string s, int k) {
    // in a window of length k, get amount of vowels
    var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
    var answer = 0;
    var left = 0;
    var right = 0;
    var currSum = 0;
    // initialize window
    // easier to initialize the window this way, than adding conditions
    // in the second loop
    for (var i = 0; i < k; i++)
    {
      if (vowels.Contains(s[i]))
      {
        currSum++;
      }
    }
    answer = currSum;

    // start from where we left off
    for (var i = k; i < s.Length; i++)
    {
      // just need to move the window now, since the size is already correct
      if (vowels.Contains(s[i]))
      {
        currSum++;
      }
      // the window is always the same size now, so
      // the last item can be found with i - k
      if (vowels.Contains(s[i - k]))
      {
        currSum--;
      }
      answer = Math.Max(answer, currSum);
    }
    return answer;
  }
  
  public int EqualSubstring(string s, string t, int maxCost) {
    // keep increasing the window of s, as long as my currentCost <= maxCost
    // increment by the cost difference
    // can be any contiguous substring in s 
    var currentCost = 0;
    var maxLength = 0;
    var left = 0;
    for (var right = 0; right < s.Length; right++)
    {
      currentCost += Math.Abs(s[right] - t[right]);
      // move the window
      while (currentCost > maxCost)
      {
        currentCost -= Math.Abs(s[left] - t[left]);
        left++;
      }
      // do not forget the +1! off by one errors...
      maxLength = Math.Max(maxLength, right - left + 1);
    }

    return maxLength;
  }
  
  public int MaximumUniqueSubarray(int[] nums) {
    // sliding window to remove duplicates
    // prefix sum in an array to find its index with best score
    // however, the array's elements cannot be repeated
    var left = 0;
    var max = 0;
    var set = new HashSet<int>(); // add items to the set, if we see a repeat item we slide the left part of the window.
    var curr = 0; // prefix sum
    for (var right = 0; right < nums.Length; right++)
    {
      while (set.Contains(nums[right]))
      {
        curr -= nums[left];
        set.Remove(nums[left]);
        left++;
      }
      set.Add(nums[right]);
      curr += nums[right];
      max = Math.Max(max, curr);
    }

    return max;
  }
}