namespace ConsoleApp1;

public class Leetcode74 {
    public bool SearchMatrix(int[][] matrix, int target) {
        var rows = matrix.Length;
        var cols = matrix[0].Length;

        var left = 0;
        // "flatten" the array length
        var right = rows * cols - 1;
    
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            // we can find the row by diving our current mid by the number of columns
            // ex: in a 6 row x 3 cols matrix, on a first iteration where left = 0 and right = 18, mid = 9
            // 9 / 3 = 3, so we are in the 3rd row
            var row = mid / cols;
            // we can find the column by taking the remainder of the division
            // ex: 9 % 3 = 0, so we are in the 0th column
            var col = mid % cols;
            var num = matrix[row][col];

            if (num == target) return true;

            // when the target is greater than our current number, we move the left pointer to the right
            if (target > num) left = mid + 1;
            // otherwise we move the right pointer to the left
            else right = mid - 1;
        }

        return false;
    }
}

public class Leetcode704 {
    public int Search(int[] nums, int target) {
        var left = 0;
        var right = nums.Length - 1;
        while (left <= right)
        {
            // the right - left operation is to prevent an overflow
            var mid = left + (right - left) / 2;
            if (nums[mid] == target) return mid;
      
            // move the search space
            // if we are too far to the right, move the right edge to the middle - 1
            if (nums[mid] > target) right = mid - 1;
            else left = mid + 1; // otherwise move our left edge
        }

        return -1;
    }
}

public class Leetcode2300 {
    public int[] SuccessfulPairs(int[] spells, int[] potions, long success) {
        Array.Sort(potions);
        // the answer must contain the amount of potions that each spell can be successful with
        // so, we must maintain the same order for our answer
        var ans = new int[spells.Length];
        var pLen = potions.Length;

        for (var i = 0; i < spells.Length; i++)
        {
            // given the sorted potions, do a binary search for the minimum required value
            // for example, if our current spell is 7, and our success is 9,
            // our minimum required value is 9 / 7 = 1.2857
            // some issues may occur with floating point numbers, so we use double.
            var minRequiredVal = success / (double)spells[i];
            var insertIndex = binarySearch(potions, minRequiredVal);
            // the index is the first element that is greater or equal to minRequiredVal
            // so, the total amount of potions is that index - the length of the potions array
            // ex: if the potions array is [1, 2, 3, 4, 5], and the minRequiredVal is 2.5
            // the index is 3, and the total amount of potions is 5 - 3 = 2
            ans[i] = pLen - insertIndex;
        }

        return ans;
    }

    public int binarySearch(int[] arr, double target)
    {
        // standard binary search
        var left = 0;
        var right = arr.Length - 1;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            if (target > arr[mid]) left = mid + 1;
            else right = mid - 1;
        }
        // we keep going until the left pointer is greater than the right one
        // by that point, the left one is either the correct index or the insertion point
        return left;
    }
}

public class Leetcode35 {
    public int SearchInsert(int[] nums, int target) {
        var left = 0;
        var right = nums.Length - 1;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            if (nums[mid] == target) return mid; // found
            else if (target > nums[mid]) left = mid + 1; // target is greater
            else right = mid - 1; // target is smaller
        }

        return left;
    }
}

public class Leetcode2389 {
    public int[] AnswerQueries(int[] nums, int[] queries) {
        var ans = new int[queries.Length];
        // sort the nums
        Array.Sort(nums);
        // prefix sum them
        var prefixSum = new int[nums.Length];
        prefixSum[0] = nums[0];
        for (var i = 1; i < nums.Length; i++)
        {
            prefixSum[i] = nums[i] + prefixSum[i - 1];
        }

        for (var i = 0; i < queries.Length; i++)
        {
            ans[i] = BinarySearch(prefixSum, queries[i]);
        }

        return ans;
    }

    public int BinarySearch(int[] sums, int target)
    {
        // if binary search finds the item, return the left index + 1
        // if not found, return the left index
        var left = 0;
        var right = sums.Length - 1;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            // mid + 1 because the "insertion" point indicates the size of the array
            if (sums[mid] == target) return mid + 1;
            else if (sums[mid] > target) right = mid - 1; // current mid is too big, move right pointer
            else left = mid + 1; // too small, move left pointer
        }

        return left;
    }
}

/**
 * There is a more creative way to use binary search - on a solution space/answer. A very common type of problem is "what is the max/min that something can be done". Binary search can be used if the following criteria are met:

    You can quickly (in O(n)O(n) or better) verify if the task is possible for a given number x.
    If the task is possible for a number x, and you are looking for:
        A maximum, then it is also possible for all numbers less than x.
        A minimum, then it is also possible for all numbers greater than x.
    If the task is not possible for a number x, and you are looking for:
        A maximum, then it is also impossible for all numbers greater than x.
        A minimum, then it is also impossible for all numbers less than x.

 */

// Quite confusing...
public class Leetcode875 {
    public int MinEatingSpeed(int[] piles, int h) {
        // the description is very convoluted
        // we need to get the smallest amount of "bananas" that can be
        // eaten per hour, such that after eating everything we are still
        // under "h" hours. Each "eating session" takes 1 hour

        /// Initialize the left boundary of binary search to 1
        // (minimum possible eating speed)
        var left = 1;
    
        // Initialize the right boundary of binary search to 0
        // (this will be updated to the maximum value in the piles array)
        var right = 0;
    
        // Find the maximum pile size to set as the initial right boundary
        foreach (var pile in piles)
        {
            right = Math.Max(right, pile);
        }

        // Perform binary search
        while (left <= right)
        {
            // Calculate the midpoint of the current search range
            var mid = left + (right - left) / 2;
      
            // Check if Koko can finish eating all bananas at speed 'mid' within 'h' hours
            if (Check(mid, piles, h)) {
                // If feasible, try a smaller speed by moving the right boundary to 'mid - 1'
                right = mid - 1;
            } else {
                // If not feasible, try a larger speed by moving the left boundary to 'mid + 1'
                left = mid + 1;
            }
        }

        // After the loop, 'left' will be the minimum feasible eating speed
        return left;
    }

    
    // Helper method to check if Koko can finish all bananas at speed 'k' within 'hoursLimit'
    public bool Check(int k, int[] piles, int hoursLimit)
    {
        // Initialize total hours required to 0
        long hours = 0;
    
        // Calculate the total hours required for each pile
        foreach (var pile in piles) {
            // For each pile, calculate the hours needed at speed 'k' and add to the total
            // Math.Ceiling is used to round up since even a partial pile takes an additional hour
            hours += (long)Math.Ceiling(pile / (double)k);
        }
    
        // Return true if total hours required is less than or equal to hours limit
        return hours <= hoursLimit;
    }
}

public class Leetcode1631 {
  // Number of rows and columns in the heights matrix
  int Rows;
  int Cols;
  // Directions array for moving right, down, left, and up
  int[][] Directions = 
      {new int[] {0, 1}, new int[] {1, 0}, new int[] {0, -1}, new int[] {-1, 0}};

  // Main function to find the minimum effort path
  public int MinimumEffortPath(int[][] heights) {
    // Initialize rows and columns based on the input matrix
    Rows = heights.Length;
    Cols = heights[0].Length;

    // Define the search space for the binary search
    var left = 0;
    var right = 0;

    // Find the maximum value in the matrix to set the upper bound of the binary search
    foreach (var row in heights)
      foreach(var val in row)
        right = Math.Max(val, right);

    // Binary search loop to find the minimum effort
    while (left <= right)
    {
      // Calculate the middle value of the current search space
      var mid = left + (right - left) / 2;
      // Perform a depth-first search (DFS) to check if a path with the current mid value is feasible
      if (Check(mid, heights)) 
        right = mid - 1; // If feasible, decrease the upper bound
      else 
        left = mid + 1;  // If not feasible, increase the lower bound
    }

    // Return the minimum effort required
    return left;
  }

  // Function to check if a path exists with the given effort
  public bool Check(int effort, int[][] heights)
  {
    // Boolean matrix to mark visited cells
    var seen = new bool[Rows, Cols];
    // Stack for iterative DFS
    var stack = new Stack<Pair>();
    // Mark the starting cell as seen and push it onto the stack
    seen[0, 0] = true;
    stack.Push(new Pair(0, 0));

    // DFS loop
    while (stack.Count > 0)
    {
      // Pop the top cell from the stack
      var pair = stack.Pop();
      var row = pair.Row;
      var col = pair.Col;
      // If we reach the bottom-right cell, return true
      if (row == Rows - 1 && col == Cols - 1) return true;

      // Explore all 4 possible directions
      foreach (var dir in Directions)
      {
        var nextRow = row + dir[0];
        var nextCol = col + dir[1];
        // Skip invalid or already seen cells
        if (!IsValid(nextRow, nextCol) || seen[nextRow, nextCol]) continue;
        // Skip cells where the effort required exceeds the given effort
        if (Math.Abs(heights[nextRow][nextCol] - heights[row][col]) > effort) continue;
        // Mark the next cell as seen and push it onto the stack
        seen[nextRow, nextCol] = true;
        stack.Push(new Pair(nextRow, nextCol));
      }
    }

    // Return false if no path is found
    return false;
  }

  // Helper function to check if a cell is within matrix bounds
  public bool IsValid(int row, int col)
  {
    return row >= 0 && row < Rows && col >= 0 && col < Cols;
  }
}

// Class to represent a cell in the matrix
class Pair {
  public int Row;
  public int Col;
  public Pair(int row, int col) {
    this.Row = row;
    this.Col = col;
  }
}
