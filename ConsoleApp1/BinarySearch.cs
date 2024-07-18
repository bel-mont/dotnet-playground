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