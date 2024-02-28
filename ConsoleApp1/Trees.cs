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