// Input: nums = [4,3,2,7,8,2,3,1]
// Output: [5,6]
// nums length = 8, should have all 8 numbers, but 5 and 6 are missing

using System.Text.Json;

public class DisappearedNums
{
  public IList<int> FindDisappearedNumbers(int[] nums)
  {
    // simple solution first
    // create a second array of n length
    // iterate nums, store each number in its index value
    // ex: i == 0, val == 3, secondArray[3 - 1] = 3
    // while doing this, keep track of the amount of items that were added
    // ex: missingItems = n, then each time we add an item to array we do -1
    // the second array can be nullable, so we can check if there is an existing item

    // create results array, length should be missingItems
    // iterate the previous array, if it is null, store it+1 in results array
    // return results.

    // however, that is not an O(n) solution.
    var orderedArray = new int?[nums.Length];
    var missingItems = nums.Length;
    for (var i = 0; i < nums.Length; i++)
    {
      // itValue - 1, to avoid invalid indexes/off by one errors
      var numsIteratorVal = nums[i];
      var orderedArrayIndex = numsIteratorVal - 1;
      if (!orderedArray[orderedArrayIndex].HasValue)
      {
        orderedArray[orderedArrayIndex] = nums[i];
        missingItems--;
      }
    }

    var results = new int[missingItems];
    var r = 0;
    var j = 0;
    while (r < results.Length && j < orderedArray.Length)
    {
      if (!orderedArray[j].HasValue)
      {
        results[r] = j + 1; // ex: orderedArray[3] would have a value of 4, if it was not null
        r++;
      }

      j++;
    }

    return results;
  }

  public IList<int> FasterSolution(int[] nums)
  {
    // better solution, hopefully
    var dupes = 0;
    var i = 0;
    while (i < nums.Length)
    {
      // it may be a bit confusing, so let's store 2 different vars
      var currentVal = nums[i];
      // when index and value match we do not swap anything, just proceed to next item
      if (i == currentVal - 1)
      {
        i++;
      }
      else
      {
        // otherwise, swap!
          
        //however, before swapping we check if the destination already has the correct value
        // if it does, it means we have a dupe.
        var temp = nums[currentVal - 1];
          
        if (nums[currentVal - 1] == currentVal)
        {
          nums[i] = -1;
          dupes++;
          // move on to next item, the -1 will be checked later in the loop
          i++;
        }
        else
        {
          // when the destination has a different number, we can swap
          nums[currentVal - 1] = currentVal; // current val is now in its correct position
          nums[i] = temp;
          // we do not increment i just yet, in case the value we put in i still
          // needs to be moved


          // after swapping, check if the value we moved into position is -1
          // -1 means that the value was removed because it was a duplicate
          if (nums[i] == -1)
          {
            //we also move on to the next item if we find a dupe
            i++;
          }
        }
      }
    }
    Console.WriteLine(dupes);
    Console.WriteLine(JsonSerializer.Serialize(nums));
    var currentResultIt = 0;
    var results = new int[dupes];
    for (var j = 0; j < nums.Length; j ++)
    {
      if (nums[j] == -1)
      {
        results[currentResultIt++] = j + 1;
      }
      if (currentResultIt == results.Length)
      {
        break;
      }
    }
    return results;
  }
}