﻿namespace ConsoleApp1;

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

        var heap = new PriorityQueue<int, (int Difference, int Val)>(comparer);
        foreach (var n in arr)
        {
            var entry = (Math.Abs(x - n), n);
            heap.Enqueue(n, entry);
            if (heap.Count > k) heap.Dequeue(); 
        }

        var ans = new List<int>();
        while (heap.Count > 0)
        {
            ans.Add(heap.Dequeue());
        }
        ans.Sort();
        return ans;

    }
}

// C#'s PrioritiQueue defaults to a minHeap. Check the priority queue md file for more information.
public class Leetcode215 {
    public int FindKthLargest(int[] nums, int k) {
        // put items in a maxHeap, dequeue if we go past k items
        var maxHeapComparer = Comparer<int>.Create((item1, item2) => item1.CompareTo(item2)); // this comparer is not needed, remove
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


public class Leetcode973 {
    public int[][] KClosest(int[][] points, int k) {
        // The distance between two points on the X-Y plane is the Euclidean distance
        // (i.e., √(x1 - x2)2 + (y1 - y2)2).
    
        // try a maxHeap to remove any big items beyond k items
        // the weight should be based on the above formula
        var comparer = Comparer<double>.Create((item1, item2) => item2.CompareTo(item1));
        var heap = new PriorityQueue<int[], double>(comparer);
        foreach (var p in points)
        {
            var difference = CalculateDistance(p[0], p[1], 0, 0);
            heap.Enqueue(p, difference);
            if (heap.Count > k) heap.Dequeue();
        }
        var ans = new int[k][];
        for (var i = 0; i < k; i++) ans[i] = heap.Dequeue();
        return ans;
    }

    double CalculateDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }
}

public class KthLargest {
    private PriorityQueue<int, int> heap = new PriorityQueue<int, int>();
    private int _k;
  
    public KthLargest(int k, int[] nums) {
        _k = k;
        foreach (var n in nums)
        {
            heap.Enqueue(n, n);
            if (heap.Count > k) heap.Dequeue();
        }
    }
  
    public int Add(int val) {
        heap.Enqueue(val, val);
        if (heap.Count > _k) heap.Dequeue();
        return heap.Peek();
    }
}

/**
 * Your KthLargest object will be instantiated and called as such:
 * KthLargest obj = new KthLargest(k, nums);
 * int param_1 = obj.Add(val);
 */

public class Leetcode692 {
  public IList<string> TopKFrequent(string[] words, int k) {
    // count word frequency, then maxHeap to remove the least frequent.
    var frequency = new Dictionary<string, int>();
    foreach (var s in words)
    {
      frequency.TryGetValue(s, out int count);
      frequency[s] = count + 1;
    }
    // Note the use of 2 comparers, and how they sort of "combine" the min/max heap by doing 2 different
    // potential comparisons. These 2 comparisons must be reverted, because we first get only K items
    // then we take those k items and sort them through the opposite operation so they are ordered properly.

    var minHeapComparer = Comparer<(int Freq, string Word)>.Create((x, y) => {
      // if the frequency is the same, sort by word.
      // a word comparison is already using a lexographical comparison
      if (x.Freq == y.Freq) return y.Word.CompareTo(x.Word);
      return x.Freq.CompareTo(y.Freq);
    });
    var heap = new PriorityQueue<string, (int Freq, string Word)>(minHeapComparer);
    foreach (var pair in frequency)
    {
      heap.Enqueue(pair.Key, (Freq: pair.Value, Word: pair.Key));
      if (heap.Count > k) heap.Dequeue();
    }
    
    // put the leftover k items in a maxHeap to get the appropriate order
    var maxHeapComparer = Comparer<(int Freq, string Word)>.Create((x, y) => {
      if (x.Freq == y.Freq) return x.Word.CompareTo(y.Word); // the lexographical checker is a minHeap
      // notice the difference here compared to the previous comparer
      return y.Freq.CompareTo(x.Freq); // the frequency checker is a maxHeap
    });
    var maxHeap = new PriorityQueue<string, (int Freq, string Word)>(maxHeapComparer);
    while (heap.Count > 0)
    {
      var item = heap.Dequeue();
      maxHeap.Enqueue(item, (Freq: frequency[item], Word: item));
    }
    
    var list = new List<string>();
    // when pulling data, the smaller item is retrieved first so we need to insert in the opposite direction
    while (maxHeap.Count > 0) list.Add(maxHeap.Dequeue());
    return list;
  }
}

public class SeatManager {
    private PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
    public SeatManager(int n) {
        for (var i = 1; i <= n; i++) minHeap.Enqueue(i, i);
    }
    
    public int Reserve() {
        return minHeap.Dequeue();
    }
    
    public void Unreserve(int seatNumber) {
        minHeap.Enqueue(seatNumber, seatNumber);
    }
}

/**
 * Your SeatManager object will be instantiated and called as such:
 * SeatManager obj = new SeatManager(n);
 * int param_1 = obj.Reserve();
 * obj.Unreserve(seatNumber);
 */