﻿public class RecentCounter {
  public Queue<int> queue = new Queue<int>();
    
  public int Ping(int t) {
    // remove items from the start of the queue when its value is less
    // than the last item value - 3000
    while (queue.Count > 0 && queue.Peek() < t - 3000)
    {
      queue.Dequeue();
    }

    queue.Enqueue(t);
    return queue.Count;
  }
}

/**
 * Your RecentCounter object will be instantiated and called as such:
 * RecentCounter obj = new RecentCounter();
 * int param_1 = obj.Ping(t);
 */
 
public class MovingAverage {
  public Queue<int> queue = new Queue<int>();
  public int queueSize;
  public int sum = 0;

  public MovingAverage(int size) {
    queueSize = size;
  }

  public double Next(int val) {
    queue.Enqueue(val);
    while (queue.Count > queueSize)
    {
      var removedVal = queue.Dequeue();
      sum -= removedVal;
    }
    sum += val;    
    return (double)sum / (double)queue.Count;
  }
}