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