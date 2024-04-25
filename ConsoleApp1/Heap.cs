namespace ConsoleApp1;
class MaxHeapComparer : IComparer<int>
{
    public int Compare(int x, int y)
    {
        return y.CompareTo(x);
    }
}


public class Leetcode1046 {
    public int LastStoneWeight(int[] stones) {
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

public class Leetcode2208 {
    public int HalveArray(int[] nums) {
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