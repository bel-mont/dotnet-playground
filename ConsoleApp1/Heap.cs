namespace ConsoleApp1;

class MaxHeapComparer : IComparer<int>
{
    public int Compare(int x, int y)
    {
        return y.CompareTo(x);
    }
}

public class Leetcode1046
{
    public int LastStoneWeight(int[] stones)
    {
        var comparer = Comparer<(int difference, int val)>.Create((x, y) =>
        {
            // if the differences are the same, return the smallest value
            if (x.difference == y.difference) return x.val.CompareTo(y.val);
            // otherwise, return the difference
            return x.difference.CompareTo(y.difference);
        });
        var heap = new PriorityQueue<int, int>(new MaxHeapComparer());
        foreach (var s in stones)
        {
            heap.Enqueue(s, s);
        }

        while (heap.Count > 1)
        {
            var first = heap.Dequeue();
            var second = heap.Dequeue();
            if (first != second) heap.Enqueue(first - second, first - second);
        }

        return heap.Count == 0 ? 0 : heap.Dequeue();
    }
}

public class Leetcode2208
{
    public int HalveArray(int[] nums)
    {
        double target = 0;
        var heap = new PriorityQueue<double, double>(new MaxHeapComparerDouble());
        foreach (var n in nums)
        {
            target += n;
            heap.Enqueue(n, n);
        }

        // need to have the sum
        target = target / 2;
        var ans = 0;
        while (target > 0)
        {
            ans++;
            var x = heap.Dequeue();
            var half = x / 2;
            target -= half;
            heap.Enqueue(half, half);
        }

        return ans;
    }

    class MaxHeapComparerDouble : IComparer<double>
    {
        public int Compare(double x, double y)
        {
            return y.CompareTo(x);
        }
    }
}

/**
 * Your MedianFinder object will be instantiated and called as such:
 * MedianFinder obj = new MedianFinder();
 * obj.AddNum(num);
 * double param_2 = obj.FindMedian();
 */
public class Leetcode295
{
    private PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
    private PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(new MaxHeapComparer());

    public void AddNum(int num)
    {
        maxHeap.Enqueue(num, num);
        var max = maxHeap.Dequeue();
        minHeap.Enqueue(max, max);
        if (minHeap.Count > maxHeap.Count)
        {
            var min = minHeap.Dequeue();
            maxHeap.Enqueue(min, min);
        }
    }

    public double FindMedian()
    {
        if (maxHeap.Count > minHeap.Count) return maxHeap.Peek();
        return (minHeap.Peek() + maxHeap.Peek()) / 2.0;
    }
}

public class Leetcode1962
{
    public int MinStoneSum(int[] piles, int k)
    {
        // take the biggest number, halve it, and put it back in the piles until k == 0
        // seems like a maxHeap problem
        var sum = 0;
        var heap = new PriorityQueue<int, int>(new MaxHeapComparer());
        foreach (var p in piles)
        {
            heap.Enqueue(p, p);
            sum += p;
        }

        while (k > 0)
        {
            var max = heap.Dequeue();
            var half = (int)Math.Ceiling((double)max / 2.0);
            sum -= max - half;
            heap.Enqueue(half, half);
            k--;
        }

        return sum;
    }
}

public class Leetcode1167 {
    public int ConnectSticks(int[] sticks) {
        // a minHeap priority queue to pull 2 numbers, add them,
        // sum that value into an answer, and return the total when we have 1 stick left
        var minHeap = new PriorityQueue<int, int>();
        foreach (var s in sticks)
        {
            minHeap.Enqueue(s, s);
        }
        var ans = 0;

        while (minHeap.Count > 1)
        {
            var first = minHeap.Dequeue();
            var second = minHeap.Dequeue();
            var combinedStick = first + second;
            ans += combinedStick;
            minHeap.Enqueue(combinedStick, combinedStick);
        }

        return ans;
    }
}

public class Leetcode347 {
    public int[] TopKFrequent(int[] nums, int k) {
        var frequency = new Dictionary<int, int>();
        foreach (var n in nums)
        {
            frequency.TryGetValue(n, out var count);
            frequency[n] = count + 1;
        }

        var heap = new PriorityQueue<int, int>();
        foreach (var pair in frequency)
        {
            heap.Enqueue(pair.Key, pair.Value);
            if (heap.Count > k) heap.Dequeue();
        }

        var ans = new int[k];
        for (var i = 0; i < k; i++) ans[i] = heap.Dequeue();

        return ans;
    }
}
public class Leetcode658 {
    public IList<int> FindClosestElements(int[] arr, int k, int x) {
        var comparer = Comparer<(int Difference, int Val)>.Create((x, y) =>
        {
            if (x.Difference == y.Difference) return y.Val.CompareTo(x.Val);
            return y.Difference.CompareTo(x.Difference);
        });

        var heap = new PriorityQueue<(int Difference, int Val), (int Difference, int Val)>(comparer);
        foreach (var n in arr)
        {
            var entry = (Math.Abs(x - n), n);
            heap.Enqueue(entry, entry);
            if (heap.Count > k) heap.Dequeue(); 
        }

        var ans = new List<int>();
        while (heap.Count > 0)
        {
            ans.Add(heap.Dequeue().Val);
        }
        ans.Sort();
        return ans;

    }
}

public class Leetcode215 {
    public int FindKthLargest(int[] nums, int k) {
        // put items in a maxHeap, dequeue if we go past k items
        var maxHeapComparer = Comparer<int>.Create((item1, item2) => item1.CompareTo(item2));
        var heap = new PriorityQueue<int, int>(maxHeapComparer);

        foreach (var n in nums)
        {
            heap.Enqueue(n, n);
            // if we have more than k items, remove one to keep only the largest ones
            if (heap.Count > k) heap.Dequeue();
        }

        // return the k item
        return heap.Dequeue();
    }
}