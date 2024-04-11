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