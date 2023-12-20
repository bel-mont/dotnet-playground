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
}