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
    public Dictionary<char, HashSet<char>> dict = new Dictionary<char, HashSet<char>>{
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