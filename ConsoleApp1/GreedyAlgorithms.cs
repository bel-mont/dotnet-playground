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

public class Leetcode1481 {
    public int FindLeastNumOfUniqueInts(int[] arr, int k) {
        // store frequencies in hash map
        // value -> frequency
        var frequencies = new Dictionary<int, int>();
        for (var i = 0; i < arr.Length; i++)
        {
            frequencies.TryGetValue(arr[i], out int count);
            frequencies[arr[i]] = count + 1;
        }
    
        // store the frequencies in an array to sort it later
        var sortedFrequencies = new List<int>();
        foreach (var pair in frequencies)
        {
            // we only care about the frequency (value of the hashmap)
            sortedFrequencies.Add(pair.Value);
        }
        // sort by frequency
        sortedFrequencies.Sort((x, y) => y.CompareTo(x)); // invert it so we can remove the last item later on
        while (k > 0)
        {
            var smallestFrequency = sortedFrequencies[sortedFrequencies.Count - 1];
            if (smallestFrequency <= k)
            {
                k -= smallestFrequency;
                sortedFrequencies.RemoveAt(sortedFrequencies.Count - 1);
            }
            else break;
        }

        return sortedFrequencies.Count;
    }
}

public class Leetcode1481Alternative {
    public int FindLeastNumOfUniqueInts(int[] arr, int k) {
        var frequencies = new Dictionary<int, int>();
        for (var i = 0; i < arr.Length; i++) {
            frequencies.TryGetValue(arr[i], out int count);
            frequencies[arr[i]] = count + 1;
        }
        
        var sortedFrequencies = new int[frequencies.Count][];
        var j = 0;
        foreach (var pair in frequencies) {
            sortedFrequencies[j] = new int[2] { pair.Value, pair.Key };
            j++;
        }
        Array.Sort(sortedFrequencies, (x, y) => x[0].CompareTo(y[0]));
        
        var index = 0;
        while (index < sortedFrequencies.Length) {
            if (sortedFrequencies[index][0] <= k) {
                k -= sortedFrequencies[index][0];
            } else {
                break;
            }
            index++;
        }
        
        return sortedFrequencies.Length - index;
    }
}
