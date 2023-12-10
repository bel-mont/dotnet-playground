namespace ConsoleApp1;

public class Squares
{
  public int[] SortedSquares(int[] nums) {
    var sortedSquared = new int[nums.Length];
    // Loop until the pointers have the same index.
    // Start indexes at beginning and end of array
    var startIndex = 0;
    var lastIndex = nums.Length - 1;
    var currentSortedSquaredIndex = lastIndex;
    while (true) {
      if (Math.Abs(nums[startIndex]) > Math.Abs(nums[lastIndex])) {
        sortedSquared[currentSortedSquaredIndex] = nums[startIndex] * nums[startIndex];
        startIndex++;
        currentSortedSquaredIndex--;
      } else {
        sortedSquared[currentSortedSquaredIndex] = nums[lastIndex] * nums[lastIndex];
        lastIndex--;
        currentSortedSquaredIndex--;
      }
      if (startIndex >= lastIndex) {
        sortedSquared[Math.Max(currentSortedSquaredIndex, 0)] = nums[startIndex] * nums[startIndex];
        break;
      }
    }
    return sortedSquared;
  }
}