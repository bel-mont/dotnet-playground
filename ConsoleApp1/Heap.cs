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