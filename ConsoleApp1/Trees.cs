public class Trees
{
  public void PrintAllNodes(TreeNode root)
  {
    Queue<TreeNode> queue = new Queue<TreeNode>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
      int nodesInCurrentLevel = queue.Count;

      // do some logic here for the current level
      for (int i = 0; i < nodesInCurrentLevel; i++)
      {
        TreeNode node = queue.Dequeue();

        // do some logic here on the current node
        Console.WriteLine(node.Val);

        // put the next level onto the queue
        if (node.Left != null)
        {
          queue.Enqueue(node.Left);
        }
        if (node.Right != null)
        {
          queue.Enqueue(node.Right);
        }
      }
    }
  }
}

public class TreeNode
{
  public int Val { get; set; } 
  public TreeNode Left { get; set; }
  public TreeNode Right { get; set; }

  // Add constructor, methods if needed
}

/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Leetcode102 {
  public IList<IList<int>> LevelOrder(TreeNode root) {
    var answer = new List<IList<int>>();
    if (root == null) return answer;
    // BFS
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    while (queue.Count > 0)
    {
      var currentLevel = queue.Count;
      var levelList = new List<int>();
      for (var i = 0; i < currentLevel; i++)
      {
        var node = queue.Dequeue();
        levelList.Add(node.Val);
        if (node.Left != null) queue.Enqueue(node.Left);
        if (node.Right != null) queue.Enqueue(node.Right);
      }
      answer.Add(levelList);
    }

    return answer;
  }
}

/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Leetcode1161 {
  public int MaxLevelSum(TreeNode root) {
    // BFS
    var answer = 0;
    if (root == null) return answer;
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    var currMax = Int32.MinValue;
    var level = 1;
    while (queue.Count > 0)
    {
      var levelSize = queue.Count;
      var levelSum = 0;
      for (var i = 0; i < levelSize; i++)
      {
        var node = queue.Dequeue();
        levelSum += node.Val;
          
        if (node.Left != null) queue.Enqueue(node.Left);
        if (node.Right != null) queue.Enqueue(node.Right);
      }
      if (levelSum > currMax)
      {
        answer = level;
        currMax = levelSum;
      }
      level++;
    }

    return answer;
  }
}

/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Leetcode1325 {
  public TreeNode RemoveLeafNodes(TreeNode root, int target) {
    if (root == null) return null;
    if (root.Left != null) root.Left = RemoveLeafNodes(root.Left, target);
    if (root.Right != null) root.Right = RemoveLeafNodes(root.Right, target);
    if (root.Val == target && root.Left == null && root.Right == null) return null;
    return root;
  }
}

/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Leetcode637 {
  public IList<double> AverageOfLevels(TreeNode root) {
    // BFS
    var answer = new List<double>();
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    while (queue.Count > 0)
    {
      var levelSize = queue.Count;
      double levelSum = 0;
      for (var i = 0; i < levelSize; i++)
      {
        var node = queue.Dequeue();
        levelSum += node.Val;
        if (node.Left != null) queue.Enqueue(node.Left);
        if (node.Right != null) queue.Enqueue(node.Right);
      }
      answer.Add(levelSum / levelSize);
    }

    return answer;
  }
}

/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Leetcode1609 {
  public bool IsEvenOddTree(TreeNode root) {
    // BFS
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    var level = 0;
    while (queue.Count > 0)
    {
      var levelSize = queue.Count;
      var isEvenLevel = level % 2 == 0;
      int? prevVal = null;
      for (var i = 0; i < levelSize; i++)
      {
        var node = queue.Dequeue();
        // If we are at an even level with an even number, return false
        if (isEvenLevel && node.Val % 2 == 0) return false;
        // If we are at an odd level with an odd value, return false
        if (!isEvenLevel && node.Val % 2 != 0) return false;

        if (prevVal.HasValue)
        {
          // odd levels require even numbers in descending order
          if (!isEvenLevel && prevVal.Value <= node.Val) return false;
          // even levels require odd numbers in ascending order
          else if (isEvenLevel && prevVal.Value >= node.Val) return false;
        }
        prevVal = node.Val;
        if (node.Left != null) queue.Enqueue(node.Left);
        if (node.Right != null) queue.Enqueue(node.Right);
      }
      level++;
    }
    return true;
  }
}

public class Leetcode700 {
  public TreeNode SearchBST(TreeNode root, int val) {
    // DFS
    if (root == null) return null;
    if (root.Val == val) return root;
    var left = SearchBST(root.Left, val);
    if (left != null) return left;
    var right = SearchBST(root.Right, val);
    if (right != null) return right;
    return null;
  }
}

public class Leetcode113 {
  public List<IList<int>> result = new List<IList<int>>();

  public IList<IList<int>> PathSum(TreeNode root, int targetSum) {
    Dfs(root, targetSum, 0, new List<int>());
    return result;
  }

  public void Dfs(TreeNode root, int targetSum, int curr, IList<int> path)
  {
    if (root == null) return;
    curr += root.Val;
    path.Add(root.Val);
    // we are at a leaf
    if (root.Left == null && root.Right == null)
    {
      // if we have reached our sum, we can return our path + the current root if
      if (curr == targetSum) result.Add(new List<int>(path)); // Copy the list to prevent it from being changed in another DFS call
      // Remove the last item after we stop using it, to avoid an incorrect answer.
      path.RemoveAt(path.Count - 1);
      return;
    }
    Dfs(root.Left, targetSum, curr, path);
    Dfs(root.Right, targetSum, curr, path);
    // Remove any intermediary node after we are done with it.
    path.RemoveAt(path.Count - 1);
  }
}

public class Leetcode1305 {
  public IList<int> GetAllElements(TreeNode root1, TreeNode root2) {
    var list1 = new List<int>();
    var list2 = new List<int>();
    Dfs(root1, list1);
    Dfs(root2, list1);
    // merge

    var ans = list1.Concat(list2).ToList();
    // sort
    ans.Sort();
    return ans;
  }

  public void Dfs(TreeNode root, List<int> list)
  {
    if (root == null) return;
    Dfs(root.Left, list);
    list.Add(root.Val);
    Dfs(root.Right, list);
  }
}