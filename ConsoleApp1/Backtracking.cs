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