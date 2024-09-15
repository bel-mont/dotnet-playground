using System.Text;

namespace ConsoleApp1;

public class Leetcode46 {
    public IList<IList<int>> Permute(int[] nums) {
        var ans = new List<IList<int>>();
        Backtrack(new List<int>(), ans, nums);
        return ans;
    }

    public void Backtrack(List<int> curr, List<IList<int>> ans, int[] nums)
    {
        if (curr.Count == nums.Length)
        {
            ans.Add(new List<int>(curr));
            return;
        }

        foreach(var n in nums)
        {
            if (curr.Contains(n)) continue;
            curr.Add(n);
            Backtrack(curr, ans, nums);
            curr.RemoveAt(curr.Count - 1);
        }
    }
}

public class Leetcode78 {
    public IList<IList<int>> Subsets(int[] nums) {
        var ans = new List<IList<int>>();
        Backtrack(new List<int>(), 0, ans, nums);
        return ans;
    }

    public void Backtrack(List<int> curr, int i, List<IList<int>> ans, int[] nums)
    {
        if (i > nums.Length) return;
        ans.Add(new List<int>(curr));
        for (var j = i; j < nums.Length; j++)
        {
            curr.Add(nums[j]);
            Backtrack(curr, j + 1, ans, nums);
            curr.RemoveAt(curr.Count - 1);
        }
    }
}

public class Leetcode77 {
    public IList<IList<int>> Combine(int n, int k) {
        var ans = new List<IList<int>>();
        Backtrack(new List<int>(), ans, 1, n, k);
        return ans;
    }

    public void Backtrack(List<int> curr, List<IList<int>> ans, int i, int n, int k)
    {
        if (curr.Count == k) 
        {
            ans.Add(new List<int>(curr));
            return;
        }
    
        for (var j = i; j <= n; j++)
        {
            curr.Add(j);
            // the + 1 skips the same number, to avoid repeats in the next recursion
            Backtrack(curr, ans, j + 1, n, k);
            curr.RemoveAt(curr.Count - 1);
        }
    }
}

public class Leetcode797 {
    public IList<IList<int>> AllPathsSourceTarget(int[][] graph) {
        var ans = new List<IList<int>>();
        Backtrack(new List<int>(), graph, 0, ans);
        return ans;
    }

    public void Backtrack(List<int> curr, int[][] graph, int i, List<IList<int>> ans)
    {
        if (i == graph.Length - 1)
        {
            curr.Add(i);
            ans.Add(new List<int>(curr));
            curr.RemoveAt(curr.Count - 1);
            return;
        }

        curr.Add(i);
        for (var j = 0; j < graph[i].Length; j++) Backtrack(curr, graph, graph[i][j], ans);
        curr.RemoveAt(curr.Count - 1);
    }
}

public class Leetcode17 {
    public Dictionary<char, HashSet<char>> dict = new()
    {
        { '2', new HashSet<char> { 'a', 'b', 'c' } },
        { '3', new HashSet<char> { 'd', 'e', 'f' } },
        { '4', new HashSet<char> { 'g', 'h', 'i' } },
        { '5', new HashSet<char> { 'j', 'k', 'l' } },
        { '6', new HashSet<char> { 'm', 'n', 'o' } },
        { '7', new HashSet<char> { 'p', 'q', 'r', 's' } },
        { '8', new HashSet<char> { 't', 'u', 'v' } },
        { '9', new HashSet<char> { 'w', 'x', 'y', 'z' } },
    };

    public IList<string> LetterCombinations(string digits) {
        if (digits.Length == 0) return new List<string>();
        var ans = new List<string>();
        var curr = new StringBuilder();
        Backtrack(ans, digits, curr, 0);
        return ans;
    }

    public void Backtrack(List<string> ans, string digits, StringBuilder sb, int digitIndex)
    {
        if (sb.Length == digits.Length)
        {
            ans.Add(sb.ToString());
            return;
        }

        for(var i = digitIndex; i < digits.Length; i++)
        {
            var num = digits[i];
            foreach(var ch in dict[num])
            {
                // if (sb.Contains(ch)) continue;
                sb.Append(ch);
                Backtrack(ans, digits, sb, i + 1);
                sb.Remove(sb.Length - 1, 1);
            }
        }
    }
}

public class Leetcode39 {
    public IList<IList<int>> CombinationSum(int[] candidates, int target) {
        var ans = new List<IList<int>>();
        Backtrack(new List<int>(), 0, 0, ans, candidates, target);
        return ans;
    }

    public void Backtrack(List<int> path, int start, int curr, List<IList<int>> ans, int[] candidates, int target)
    {
        if (curr == target)
        {
            ans.Add(new List<int>(path));
            return;
        }

        for (var i = start; i < candidates.Length; i++)
        {
            var num = candidates[i];
            if (curr + num <= target)
            {
                path.Add(num);
                Backtrack(path, i, curr + num, ans, candidates, target);
                path.RemoveAt(path.Count - 1);
            }
        }
    }
}

public class Leetcode52 {
    private int Size;

    public int TotalNQueens(int n) {
        Size = n;
        return Backtrack(0, new HashSet<int>(), new HashSet<int>(), new HashSet<int>());
    }

    public int Backtrack(int row, HashSet<int> diagonals, HashSet<int> antiDiagonals, HashSet<int> cols)
    {
        // base case occurs when N queens have been placed
        if (row == Size) return 1;

        var solutions = 0;
        for (var col = 0; col < Size; col++)
        {
            var currDiagonal = row - col;
            var currAntiDiagonal = row + col;

            // if we cannot place the queen, we skip
            if (cols.Contains(col) || diagonals.Contains(currDiagonal) || antiDiagonals.Contains(currAntiDiagonal)) continue;

            // add the current item to our board state
            cols.Add(col);
            diagonals.Add(currDiagonal);
            antiDiagonals.Add(currAntiDiagonal);

            // move to next row with new board state
            solutions += Backtrack(row + 1, diagonals, antiDiagonals, cols);

            // remove the queen from the board after we
            // have explored all paths
            cols.Remove(col);
            diagonals.Remove(currDiagonal);
            antiDiagonals.Remove(currAntiDiagonal);
        }

        return solutions;
    }
}

// Uses DFS with backtracking
public class Leetcode79
{
    public int Rows;
    public int Cols;
    public string Target;
    public int[][] Directions = { new[] { 0, 1 }, new[] { 1, 0 }, new[] { 0, -1 }, new[] { -1, 0 } };
    public bool[][] Seen;

    public bool Exist(char[][] board, string word)
    {
        Rows = board.Length;
        Cols = board[0].Length;
        Target = word;
        Seen = new bool[Rows][];
        for (var i = 0; i < Rows; i++) Seen[i] = new bool[Cols];

        for (var row = 0; row < Rows; row++)
        {
            for (var col = 0; col < Cols; col++)
            {
                if (board[row][col] != word[0]) continue;
                Seen[row][col] = true;
                if (Backtrack(row, col, 1, board)) return true;
                // If we do not find the word, we need to reset the board state/
                // We remove the latest character and move on to another path.
                Seen[row][col] = false;
            }
        }

        return false;
    }

    public bool Backtrack(int row, int col, int i, char[][] board)
    {
        if (i == Target.Length) return true;

        foreach (var dir in Directions)
        {
            var nextRow = row + dir[0];
            var nextCol = col + dir[1];
            if (!IsValid(nextRow, nextCol) || Seen[nextRow][nextCol]) continue;
            if (board[nextRow][nextCol] != Target[i]) continue;
            Seen[nextRow][nextCol] = true;
            if (Backtrack(nextRow, nextCol, i + 1, board)) return true;
            Seen[nextRow][nextCol] = false;
        }

        return false;
    }

    public bool IsValid(int row, int col)
    {
        return row >= 0 && row < Rows && col >= 0 && col < Cols;
    }
}

public class Leetcode22 {
    public IList<string> GenerateParenthesis(int n) {
        var ans = new List<string>();
        Backtrack(ans, n, new StringBuilder(), 0, 0);
        return ans;
    }

    public void Backtrack(IList<string> ans, int n, StringBuilder sb, int open, int close)
    {
        if (sb.Length == n * 2)
        {
            // we have added all possible pairs
            ans.Add(sb.ToString());
            return;
        }

        if (open < n)
        {
            sb.Append("(");
            Backtrack(ans, n, sb, open + 1, close);
            sb.Remove(sb.Length - 1, 1);
        }
        if (close < open)
        {
            sb.Append(")");
            Backtrack(ans, n, sb, open, close + 1);
            sb.Remove(sb.Length - 1, 1);
        }
    }
}

public class Leetcode967 {
    public int[] NumsSameConsecDiff(int n, int k)
    {
        var ans = new List<int>();
        var sb = new StringBuilder();
        for (var i = 1; i <= 9; i++)
        {
            sb.Append(i.ToString());
            Backtrack(ans, n, k, i, sb);
            sb.Remove(sb.Length - 1, 1);
        }
        return ans.ToArray();
    }

    public void Backtrack(List<int> ans, int n, int k, int curr, StringBuilder sb)
    {
        if (sb.Length == n)
        {
            ans.Add(int.Parse(sb.ToString()));
            return;
        }
    
        // If we can "go up" from curr, by k
        if (curr + k <= 9)
        {
            sb.Append((curr + k).ToString());
            Backtrack(ans, n, k, curr + k, sb);
            sb.Remove(sb.Length - 1, 1);
        }
        // check if we can go down
        if (curr - k >= 0 && k > 0) // k == 0 edge case
        {
            sb.Append((curr - k).ToString());
            Backtrack(ans, n, k, curr - k, sb);
            sb.Remove(sb.Length - 1, 1);
        }
    }
}


public class Leetcode216 {
    public IList<IList<int>> CombinationSum3(int k, int n) {
        var ans = new List<IList<int>>();
        Backtrack(ans, k, n, 1, 0, new List<int>());
        return ans;
    }

    public void Backtrack(List<IList<int>> ans, int k, int n, int i, int sum, List<int> curr)
    {
        if (curr.Count == k)
        {
            if (sum == n) ans.Add(new List<int>(curr));
            return;
        }

        for (var j = i; j <= Math.Min(9, n); j++)
        {
            if (sum + j <= n)
            {
                sum += j;
                curr.Add(j);
                Backtrack(ans, k, n, j + 1, sum, curr);
                sum -= j;
                curr.RemoveAt(curr.Count - 1);
            }
        }
    }
}

public class Leetcode540 {
    public int SingleNonDuplicate(int[] nums) {
        if (nums.Length == 1) return nums[0];
        return Walk(nums, 0, nums.Length - 1);
    }

    public int Walk(int[] nums, int lo, int hi)
    {
        var mid = lo + (hi - lo) / 2;
        var isAtLeftEdge = mid - 1 < 0 && nums[mid + 1] != nums[mid];
        var isAtRightEdge = mid + 1 >= nums.Length && nums[mid - 1] != nums[mid];
        if (isAtLeftEdge || isAtRightEdge) return nums[mid];
        if (nums[mid - 1] != nums[mid] && nums[mid + 1] != nums[mid]) return nums[mid];

        var leftNumIsEqual = nums[mid] == nums[mid - 1];
        if (leftNumIsEqual)
        {
            var evenMid = mid % 2 == 0; // this means the single num is on the left
            if (evenMid) return Walk(nums, lo, mid - 1);
            else return Walk(nums, mid + 1, hi);
        }
        else
        {
            if (mid % 2 == 0) return Walk(nums, mid + 1, hi);
            else return Walk(nums, lo, mid - 1);
        }
    }
}