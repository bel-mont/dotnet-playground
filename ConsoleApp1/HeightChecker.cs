// This solution requires bubble sort (or any other type of sort)
// have not seen that in leetcode at this point, but it can be checked
public class HeightChecker
{
  public int Run(int[] heights) {
    // Insert sorted data into another array.
    // we have only a few items, so bubble sort it
    // initiate heightdoesNotMatch to 0
    var heightDoesNotMatch = 0;
    var expectedHeights = new int[heights.Length];
    for (var i = 0; i < expectedHeights.Length; i++)
    {
      expectedHeights[i] = heights[i];
    }
      
    // Bubble it
    BubbleSort(expectedHeights);
    
    // After the array is sorted, iterate one final time
    // Compare the same index, and if the item is different, increment the
    // heightDoesNotMatch variable.
    for (var i = 0; i < expectedHeights.Length; i++)
    {
      if (expectedHeights[i] != heights[i])
      {
        heightDoesNotMatch++;
      }
    }
    return heightDoesNotMatch;
  }
  
  public void BubbleSort(int[] arr)
  {
    // iterate from last position
    for (var i = arr.Length; i > 0; i--)
    {
      var hasNotSwapped = true;
      // Start from beginning of array, right up till we reach the item before i
      for (var j = 0; j < i - 1; j++)
      {
        if (arr[j] > arr[j + 1])
        {
          var temp = arr[j];
          arr[j] = arr[j + 1];
          arr[j + 1] = temp;
          hasNotSwapped = false;
        }
      }
      if (hasNotSwapped)
      {
        break;
      }
    }
  }
}