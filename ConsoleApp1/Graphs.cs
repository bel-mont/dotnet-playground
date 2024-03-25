public class Graphs {
  public Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
  public bool[] seen;

  public int FindCircleNum(int[][] isConnected) {
// build the graph first
    int n = isConnected.Length;
// each group of connections representr a single node
    for (var i = 0; i < n; i++)
    {
      if (!graph.ContainsKey(i)) graph[i] = new List<int>();
      for (var j = i + 1; j < n; j++) //??
      {
        if (!graph.ContainsKey(j)) graph[j] = new List<int>();
        if (isConnected[i][j] == 1)
        {
          graph[i].Add(j);
          graph[j].Add(i);
        }
      }
    }

    seen = new bool[n];
    int ans = 0;
    for (var i = 0; i < n; i++)
    {
      if (!seen[i])
      {
        ans++;
        seen[i] = true;
        Dfs(i);
      }
    }
    
    return ans;
  }

  public void Dfs(int node)
  {
// each item in the graph contains all of its neighbors
    foreach (var neighbor in graph[node])
    {
// mark them all as seen
      if (!seen[neighbor])
      {
        seen[neighbor] = true;
        Dfs(neighbor);
      }
    }
  }
  
  public int CountComponents(int n, int[][] edges) {
    // build adjacency graph
    var graph = new Dictionary<int, List<int>>();
    foreach (var e in edges)
    {
      var a = e[0];
      var b = e[1];
      if (!graph.ContainsKey(a)) graph[a] = new List<int>();
      if (!graph.ContainsKey(b)) graph[b] = new List<int>();
      graph[a].Add(b);
      graph[b].Add(a);
    }

    var seen = new bool[n];
    var ans = 0;
    for (var i = 0; i < n; i++)
    {
      if (!seen[i])
      {
        ans++;
        Dfs(i, graph, seen); 
      }
    }

    return ans;
  }

  public void Dfs(int node, Dictionary<int, List<int>> graph, bool[] seen)
  {
    seen[node] = true;
    if (!graph.ContainsKey(node)) return;
    foreach (var neighbor in graph[node])
    {
      if (!seen[neighbor]) Dfs(neighbor, graph, seen);
    }
  }
}

public class Leetcode1971
{
  public Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
  public HashSet<int> seen = new HashSet<int>();
  
  public bool ValidPath(int n, int[][] edges, int source, int destination) {
    if (edges.Length == 0) return source == 0 && destination == 0;
    // input type: array of edges
    // the question is: are the source and destination in the same component?
    
    for (var i = 0; i < n; i++)
    {
      graph[i] = new List<int>();
    }

    foreach (var e in edges)
    {
      graph[e[0]].Add(e[1]);
      graph[e[1]].Add(e[0]);
    }
    return Dfs(source, destination);
  }

  public bool Dfs(int node, int destination)
  {
    if (node == destination) return true;
    if (seen.Contains(node)) return false;
    seen.Add(node);
    foreach (var neighbor in this.graph[node])
    {
      if (Dfs(neighbor, destination)) return true;
    }
    return false;
  }
}

public class Leetcode695 {
  int gridRow;
  int gridCol;
  int[][] directions = new int[][] 
    {new int[] {0, 1}, new int[] {1, 0}, new int[] {0, -1}, new int[] {-1, 0}};
  bool[,] seen;

  public int MaxAreaOfIsland(int[][] grid) {
    int ans = 0;
    gridRow = grid.Length;
    gridCol = grid[0].Length;
    seen = new bool[gridRow, gridCol];
    var currentComponentArea = 0;

    // biggest component area
    for (var row = 0; row < gridRow; row++)
    {
      for (var col = 0; col < gridCol; col++)
      {
        if (grid[row][col] == 1 && !seen[row, col])
        {
          seen[row, col] = true;
          currentComponentArea = Dfs(row, col, grid);
          // Dfs returns the neighboring count
          // need to include the ++ for the current node
          currentComponentArea++;
        }
        ans = Math.Max(ans, currentComponentArea);
        currentComponentArea = 0;
      }
    }
    return ans;
  }

  public int Dfs(int row, int col, int[][] grid)
  {
    var area = 0;
    for (var i = 0; i < directions.Length; i++)
    {
      var dir = directions[i];
      var nextRow = row + dir[0];
      var nextCol = col + dir[1];

      if (isConnected(nextRow, nextCol, grid) && !seen[nextRow, nextCol])
      {
        seen[nextRow, nextCol] = true;
        area += Dfs(nextRow, nextCol, grid);
        area++;
      }
    }
    return area;
  }

  public bool isConnected(int row, int col, int[][] grid)
  {
    return row >= 0 && row < gridRow && col >= 0 && col < gridCol && grid[row][col] == 1;
  }
}

public class Leetcde1091 {
  int n;
  int[][] directions = new int[][]
  {
    new int[]{-1,-1}, new int[]{-1,0}, new int[]{-1,1}, new int[]{0,-1}, new int[]{0,1}, new int[]{1, -1}, new int[]{1,0}, new int[]{1,1}
  };
  public int ShortestPathBinaryMatrix(int[][] grid) {
    // in case we are blocked from the beginning
    if (grid[0][0] == 1) return -1;
    // BFS

    n = grid.Length;
    var seen = new bool[n][];
    for (var i = 0; i < n; i++)
    {
      seen[i] = new bool[n];
    }
    seen[0][0] = true;
    var queue = new Queue<State>();
    queue.Enqueue(new State(0, 0, 1));

    while (queue.Count != 0)
    {
      var state = queue.Dequeue();
      var row = state.row;
      var col = state.col;
      var steps = state.steps;
      // if we reach the end, bail
      if (row == n - 1 && col == n - 1) return steps;

      foreach (var dir in directions)
      {
        var nextRow = row + dir[0];
        var nextCol = col + dir[1];
        if (!valid(nextRow, nextCol, grid) || seen[nextRow][nextCol]) continue;
        seen[nextRow][nextCol] = true;
        queue.Enqueue(new State(nextRow, nextCol, steps + 1));
      }
    }
    return -1;
  }

  public bool valid(int row, int col, int[][] grid)
  {
    return row >= 0 && row < n && col >= 0 && col < n && grid[row][col] == 0;
  }
}

public class State {
  public int row;
  public int col;
  public int steps;
  public State(int _row, int _col, int _steps)
  {
    this.row = _row;
    this.col = _col;
    this.steps = _steps;
  }
}


public class Leetcode863 {
  public class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int x) { val = x; }
  }
  public Dictionary<TreeNode, TreeNode> parents = new Dictionary<TreeNode, TreeNode>();
  public IList<int> DistanceK(TreeNode root, TreeNode target, int k) {
    // transform the tree to a graph
    Dfs(root, null);
    // while iterating, store the target node
    // do BFS from the target node up until k
    var queue = new Queue<TreeNode>();
    var seen = new HashSet<TreeNode>();
    queue.Enqueue(target); // NOT the root! we want to be k away from target
    seen.Add(target);
    var distance = 0;
    while (queue.Count > 0 && distance < k)
    {
      var currLength = queue.Count;
      for (var i = 0; i < currLength; i++)
      {
        var node = queue.Dequeue();
        foreach(var neighbor in new TreeNode[] {node.left, node.right, parents[node]})
        {
          if (neighbor == null || seen.Contains(neighbor)) continue;
          seen.Add(neighbor);
          queue.Enqueue(neighbor);
        }
      }
      distance++;
    }

    var ans = new List<int>();
    while (queue.Count > 0)
    {
      var node = queue.Dequeue();
      ans.Add(node.val);
    }
    return ans;
  }

  public void Dfs(TreeNode node, TreeNode parent)
  {
    // assign node as the parent of left and right
    if (node == null) return;
    this.parents[node] = parent;
    Dfs(node.left, node);
    Dfs(node.right, node);
  }
}

public class Leetcode542 {
  public int rowL;
  public int colL;
  public int[][] directions = new int[][]
  {
    new int[]{0, 1}, new int[]{ 1, 0}, new int[]{0, -1}, new int[]{-1, 0}
  };

  public int[][] UpdateMatrix(int[][] mat) {
    rowL = mat.Length;
    colL = mat[0].Length;

    // BFS
    var queue = new Queue<State>();
    bool[][] seen = new bool[rowL][];
    for (var i = 0; i < rowL; i++) seen[i] = new bool[colL];
    // store all 0s first, as the initial level
    for (var row = 0; row < rowL; row++)
    {
      for (var col = 0; col < colL; col++)
      {
        if (mat[row][col] == 0)
        {
          queue.Enqueue(new State(row, col, 1));
          seen[row][col] = true;
        }
      }
    }
    // store the current level on each ans as we iterate
    while (queue.Count > 0)
    {
      var state = queue.Dequeue();
      var row = state.row;
      var col = state.col;
      var steps = state.steps;
      foreach (var dir in directions)
      {
        var nextRow = row + dir[0];
        var nextCol = col + dir[1];
        if (isValid(nextRow, nextCol, mat) && !seen[nextRow][nextCol])
        {
          seen[nextRow][nextCol] = true;
          queue.Enqueue(new State(nextRow, nextCol, steps + 1));
          mat[nextRow][nextCol] = steps;
        }
      }
    }

    return mat;
  }

  public bool isValid(int row, int col, int[][] mat)
  {
    return row >= 0 && row < rowL && col >= 0 && col < colL && mat[row][col] == 1;
  }
  
  public class State
  {
    public int row;
    public int col;
    public int steps;
    public State(int _row, int _col, int _steps)
    {
      row = _row;
      col = _col;
      steps = _steps;
    }
  }
}
public class Leetcode1293 {
  public int rowL;
  public int colL;
  public int[][] directions = new int[][]
  {
    new int[]{0, 1}, new int[]{ 1, 0}, new int[]{0, -1}, new int[]{-1, 0}
  };

  public int ShortestPath(int[][] grid, int k) {
    rowL = grid.Length;
    colL = grid[0].Length;

    var queue = new Queue<State>();
    queue.Enqueue(new State(0, 0, k, 0));
    var seen = new bool[rowL][][];
    for (var i = 0; i < rowL; i++)
    {
      seen[i] = new bool[colL][];
      for (var j = 0; j < colL; j++) seen[i][j] = new bool[k + 1];
    }
    seen[0][0][k] = true;
    while (queue.Count > 0)
    {
      var state = queue.Dequeue();
      var row = state.row;
      var col = state.col;
      var remain = state.remain;
      var steps = state.steps;
      if (row == rowL - 1 && col == colL - 1) return steps;

      foreach (var dir in directions)
      {
        var nextRow = row + dir[0];
        var nextCol = col + dir[1];
        if (!valid(nextRow, nextCol)) continue;
        if (grid[nextRow][nextCol] == 0)
        {
          if (seen[nextRow][nextCol][remain]) continue;
          seen[nextRow][nextCol][remain] = true;
          queue.Enqueue(new State(nextRow, nextCol, remain, steps + 1));
        }
        else if (remain > 0 && !seen[nextRow][nextCol][remain - 1])
        {
          seen[nextRow][nextCol][remain - 1] = true;
          queue.Enqueue(new State(nextRow, nextCol, remain - 1, steps + 1));
        }
      }
    }
    return -1;
  }

  public bool valid(int row, int col)
  {
    return row >= 0 && row < rowL && col >= 0 && col < colL;
  }

  
  public class State {
    public int row;
    public int col;
    public int remain;
    public int steps;
    public State(int _row, int _col, int _remain, int _steps)
    {
      this.row = _row;
      this.col = _col;
      this.remain = _remain;
      this.steps = _steps;
    }
  }
}

public class Leetcode1129 {
  int RED = 0;
  int BLUE = 1;
  public Dictionary<int, Dictionary<int, List<int>>> graph = new Dictionary<int, Dictionary<int, List<int>>>();
  public int[] ShortestAlternatingPaths(int n, int[][] redEdges, int[][] blueEdges) {
    // make a graph with the neighbors for each color
    graph.Add(RED, new Dictionary<int, List<int>>());
    graph.Add(BLUE, new Dictionary<int, List<int>>());

    AddToGraph(RED, redEdges, n);
    AddToGraph(BLUE, blueEdges, n);

    var ans = new int[n];
    for (var i = 0; i < n; i++) ans[i] = int.MaxValue;
    var queue = new Queue<State>();
    queue.Enqueue(new State(0, RED, 0));
    queue.Enqueue(new State(0, BLUE, 0));

    var seen = new bool[n][];
    for (var i = 0; i < n; i++) seen[i] = new bool[2];
    seen[0][RED] = true;
    seen[0][BLUE] = true;

    while (queue.Count > 0)
    {
      var state = queue.Dequeue();
      var node = state.node;
      var color = state.color;
      var steps = state.steps;
      ans[node] = Math.Min(ans[node], steps);

      foreach (var neighbor in graph[color][node])
      {
        // 1 - color switches the color of the NEXT node,
        // so we can check for validity of the alternating pattern
        // 1 - 1 = 0, 1 - 0 = 1
        // would probably be easier as bool
        if (seen[neighbor][1 - color]) continue;
        seen[neighbor][1 - color] = true;
        queue.Enqueue(new State(neighbor, 1 - color, steps + 1));
      }
    }

    for (var i = 0; i < n; i++)
    {
      if (ans[i] == int.MaxValue) ans[i] = -1;
    }

    return ans;
  }

  public void AddToGraph(int color, int[][] edges, int n)
  {
    for (var i = 0; i < n; i++) graph[color].Add(i, new List<int>());
    
    foreach (var e in edges)
    {
      var x = e[0];
      var y = e[1];
      graph[color][x].Add(y);
    }
  }
  
  public class State {
    public int node;
    public int color;
    public int steps;
    public State(int _node, int _color, int _steps)
    {
      this.node = _node;
      this.color = _color;
      this.steps = _steps;
    }
  }
}

public class Leetcode1926 {
  public int rowL;
  public int colL;
  public int[][] directions = new int[][]
  {
    new int[]{0, 1}, new int[]{ 1, 0}, new int[]{0, -1}, new int[]{-1, 0}
  };

  public int NearestExit(char[][] maze, int[] entrance) {
    // the graph is already built
    rowL = maze.Length;
    colL = maze[0].Length;
    // graph BFS, start from entrance
    var queue = new Queue<State>();
    queue.Enqueue(new State(entrance[0], entrance[1], 0));
    var seen = new bool[rowL][];
    for (var i = 0; i < rowL; i++) seen[i] = new bool[colL];
    seen[entrance[0]][entrance[1]] = true;
    // find nearest empty node at edge
    while (queue.Count > 0)
    {
      var state = queue.Dequeue();
      var row = state.row;
      var col = state.col;
      var steps = state.steps;

      foreach (var dir in directions)
      {
        var nextRow = dir[0] + row;
        var nextCol = dir[1] + col;
        if (isExit(row, col, nextRow, nextCol, maze, entrance)) return steps;
        if (!isValid(nextRow, nextCol, maze) || seen[nextRow][nextCol]) continue; 
        seen[nextRow][nextCol] = true;
        queue.Enqueue(new State(nextRow, nextCol, steps + 1));
      }
    }
    return -1;
  }

  public bool isValid(int row, int col, char[][] maze)
  {
    return row >= 0 && row < rowL && col >= 0 && col < colL && maze[row][col] == '.';
  }

  public bool isExit(int row, int col, int nextRow, int nextCol, char[][] maze, int[] entrance)
  {
    return maze[row][col] == '.'
           && !isEntrance(row, col, entrance)
           && (nextRow < 0 || nextRow == rowL || nextCol < 0 || nextCol == colL);
  }

  public bool isEntrance(int row, int col, int[] entrance)
  {
    return entrance[0] == row && entrance[1] == col;
  }

  public class State 
  {
    public int row;
    public int col;
    public int steps;
    public State(int _row, int _col, int _steps)
    {
      this.row = _row;
      this.col = _col;
      this.steps = _steps;
    }
  }
}

public class Leetcode752 {
    public int OpenLock(string[] deadends, string target) {
      var queue = new Queue<Pair>();
      var seen = new HashSet<string>();

      foreach (var s in deadends)
      {
        if (s == "0000") return -1;
        seen.Add(s);
      }

      queue.Enqueue(new Pair("0000", 0));
      seen.Add("0000");

      while (queue.Count > 0)
      {
        var pair = queue.Dequeue();
        var node = pair.node;
        var steps = pair.steps;
        if (node == target) return steps;
        foreach (var neighbor in Neighbors(node))
        {
          if (seen.Contains(neighbor)) continue;
          seen.Add(neighbor);
          queue.Enqueue(new Pair(neighbor, steps + 1));
        }
      }
      return -1;
    }

    public List<string> Neighbors(string node)
    {
      var ans = new List<string>();
      for (var i = 0; i < 4; i++)
      {
        var num = node[i] - '0';
        var changes = new int[]{-1, 1};
        foreach (var ch in changes)
        {
          var x = (num + ch + 10) % 10;
          ans.Add(node[..i] + x + node[(i + 1)..]);
        }
      }
      return ans;
    }

    public class Pair
    {
      public string node;
      public int steps;
      public Pair(string _node, int _steps)
      {
        node = _node;
        steps = _steps;
      }
    }
}