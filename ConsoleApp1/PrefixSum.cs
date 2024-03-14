public class PrefixSum
{
  public int MinStartValue(int[] nums) {
    // the validity condition is sum > 0
    // the first case of our sum never reaching 0 or less is our result
    var startValue = 1;
    bool sumBelowOne = true;
    while (sumBelowOne)
    {
      var sum = startValue;
      sumBelowOne = false;
      for (var i = 0; i < nums.Length; i++)
      {
        sum += nums[i];
        if (sum < 1)
        {
          // we break out and go into the next startValue
          startValue++;
          sumBelowOne = true;
          break;
        }
      }
    }
      
    return startValue;
  }
  
  public int NumSubarraysWithSum(int[] nums, int goal) {
    var total = 0;
    var curr = 0;
    var freq = new Dictionary<int, int>();

    for (var i = 0; i < nums.Length; i++)
    {
      curr += nums[i];
      if (curr == goal)
      {
        total++;
      }
      if (freq.ContainsKey(curr - goal))
      {
        total += freq[curr - goal];
      }
      freq.TryGetValue(curr, out int newVal);
      freq[curr] = newVal + 1;
    }
    return total;
  }
}