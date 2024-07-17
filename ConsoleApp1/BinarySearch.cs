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
        var ans = new int[spells.Length];

        var pLen = potions.Length;

        for (var i = 0; i < spells.Length; i++)
        {
            var j = binarySearch(potions, success / (double)spells[i]);
            ans[i] = pLen - j;
        }

        return ans;
    }

    public int binarySearch(int[] arr, double target)
    {
        var left = 0;
        var right = arr.Length - 1;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            if (arr[mid] < target) left = mid + 1;
            else right = mid - 1;
        }

        return left;
    }
}