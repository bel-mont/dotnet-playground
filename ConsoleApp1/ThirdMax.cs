using System.Text.Json;

public class ThirdMax
{
  public int Run(int[] nums) {
    // edge cases
    if (nums.Length == 1)
    {
      return nums[0];
    }
    if (nums.Length == 2)
    {
      var result = nums[0] > nums[1] ? nums[0] : nums[1];
      return result;
    }

    
    // initialize return value and array used to store top 3 items
    int finalValue;
    var top3Arr = new int?[3];
    top3Arr[0] = nums[0];
      
    // loop from 1 till < .length
    for (var i = 1; i < nums.Length; i++)
    {
      // loop through top3arr
      for (var j = 0; j < top3Arr.Length; j++)
      {
        // if the top3 it is not null
        if (top3Arr[j].HasValue)
        {
          // break loop if number already exists
          if (nums[i] == top3Arr[j].Value)
          {
            break;
          }
            
          // if the it val > top3 it
          if (nums[i] > top3Arr[j].Value)
          {
            // loop through remaining items from last to > j
            for (var k = 2; k > j; k--)
            {
              top3Arr[k] = top3Arr[k - 1];
            }
            top3Arr[j] = nums[i];
            break;
          }
        }
        else
        {
          // When no value is available, just insert it
          // break to avoid inserting into other indexes
          top3Arr[j] = nums[i];
          break;
        }
      }
    }
      
    if (!top3Arr[2].HasValue)
    {
      finalValue = top3Arr[0].Value;
    }
    else
    {
      finalValue = top3Arr[2].Value;
    }
    Console.WriteLine(JsonSerializer.Serialize(top3Arr));
    return finalValue;
  }
}