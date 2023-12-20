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
}