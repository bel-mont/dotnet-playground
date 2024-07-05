namespace ConsoleApp1;

public class Leetcode502 {
    public int FindMaximizedCapital(int k, int w, int[] profits, int[] capital) {
        // very similar to the asteroid problem
        var projects = MakeSortedArray(profits, capital);
        var n = profits.Length;
        var maxComparer = Comparer<int>.Create((x, y) => y.CompareTo(x));
        var maxHeap = new PriorityQueue<int, int>(maxComparer);

        var i = 0; // to keep track of the current project

        // we bail after k is reached
        for (var j = 0; j < k; j++)
        {
            // add the projects we can afford to the maxHeap
            while (i < n && projects[i][0] <= w) // end if we cannot afford project i
            {
                maxHeap.Enqueue(projects[i][1], projects[i][1]);
                i++;
            }

            // if we are out of projects, return whatever we obtained
            if (maxHeap.Count == 0) return w;

            // otherwise add to our capital and remove from the maxHeap
            w += maxHeap.Dequeue();
        }

        return w;
    }

    public int[][] MakeSortedArray(int[] profits, int[] capital)
    {
        // store the capital and profits in a multidimensional array, so we can sort the projects by capital cost
        var n = profits.Length;
        var projects = new int[n][];
        for (var i = 0; i < n; i++)
        {
            projects[i] = new int[]{ capital[i], profits[i] };
        }
        Array.Sort(projects, (x, y) => x[0].CompareTo(y[0]));
        // We should end up with something like [[0,1], [0,2], [1,5]]
        return projects;
    }
}