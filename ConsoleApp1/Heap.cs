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