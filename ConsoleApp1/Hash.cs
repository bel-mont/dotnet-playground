﻿using System.Text;

public class Hash
{
  public int SubarraySum(int[] nums, int k) {
    // store the amount of times a sum appears
    // if we find this sum later when doing sum - k,
    // it means that we have found a subarray whose sum matches k
    var sumFrequency = new Dictionary<int, int>();
    // an empty array's "sum" is zero
    sumFrequency[0] = 1;

    var total = 0;
    var sum = 0;
    foreach (var num in nums)
    {
      sum += num;
      if (sumFrequency.ContainsKey(sum - k))
      {
        total += sumFrequency[sum - k];
      }
      if (sumFrequency.ContainsKey(sum))
      {
        sumFrequency[sum]++; 
      }
      else
      {
        sumFrequency[sum] = 1;
      }
    }
    return total;
  }
  
  public int NumberOfSubarrays(int[] nums, int k) {
    var frequencies = new Dictionary<int, int>();
    frequencies[0] = 1;
    var total = 0;
    var oddsCount = 0;

    foreach (var num in nums)
    {
      oddsCount += num % 2;
      if (frequencies.ContainsKey(oddsCount - k))
      {
        total += frequencies[oddsCount - k];
      }
      if (frequencies.ContainsKey(oddsCount))
      {
        frequencies[oddsCount]++; 
      }
      else
      {
        frequencies[oddsCount] = 1;
      }
    }
    /**
    * This is based on the principle that the difference between two cumulative counts (or sums) in an array is equal to the count (or sum) of the subarray between the two positions.
    *
    * Now in this specific case, oddsCount is the cumulative count of odd numbers from the start of the array to the current index. The statement oddsCount - k then gives us the amount of odd numbers we have counted previously before we've counted exactly k odd numbers.
    *
    * So when the expression oddsCount - k equals a count that we have previously recorded in our frequencies dictionary (frequencies[oddsCount - k]), this means we have found a subarray from the recorded count to our current oddsCount which contains exactly k odd numbers.
    *
    * The reason we then increment our total by frequencies[oddsCount - k] is because frequencies[oddsCount - k] is the amount of equivalent counts (or sums) we have previously encountered, which also means the amount of end indices for our subarrays which contain exactly k odd numbers.
    *
    * num is an odd number so oddsCount increments from 3 to 4.
    * We then check if we have an entry frequencies[4 - 3] or frequencies[1] – this is equivalent to checking if there was a previous cumulative count of 1.
    * We do have frequencies[1], meaning there was a previous cumulative count of 1, indicating that there was a point in the array where we had counted one odd number.
    * Since we now have a cumulative count of 4 odd numbers (oddsCount) in our current point in the array, we can say we have a subarray from the previous cumulative count of 1 to the current cumulative count of 4 which contains exactly 4 - 1 = 3 or k odd numbers.
    * We've found a valid subarray that contains k odd numbers! We increase total by 1 (the frequency of the previous cumulative count 1 held in frequencies[1]) to record this valid subarray.
    * Finally, we increment the frequency frequencies[4] to record that we have encountered a cumulative count of 4 once.
    */
    return total;
  }
 
  public IList<IList<int>> FindWinners(int[][] matches) {
    var defeats = new Dictionary<int, int>();
    var undefeatedPlayers = new List<int>();
    var singleLossPlayers = new List<int>();
      

    // build the defeats hash
    foreach (var pair in matches)
    {
      // pair[1] is the defeated player, add it to the defeats count.
      // add / increment 
      defeats.TryGetValue(pair[1], out int count);
      defeats[pair[1]] = count + 1;

      if (!defeats.ContainsKey(pair[0]))
      {
        defeats[pair[0]] = 0;
      }
    }
      
    foreach (var pair in defeats)
    {
      if (pair.Value == 1)
      {
        singleLossPlayers.Add(pair.Key);
      }
      if (pair.Value == 0)
      {
        undefeatedPlayers.Add(pair.Key);
      }
    }
      
    var results = new int[2][];
    undefeatedPlayers.Sort();
    singleLossPlayers.Sort();
    results[0] = undefeatedPlayers.ToArray();
    results[1] = singleLossPlayers.ToArray();
    return results;
  }
  
  public int MaxNumberOfBalloons(string text) {
    var answer = 0;
    var frequencies = new Dictionary<char, int>{
      {'b', 0},
      {'a', 0},
      {'l', 0}, // need 2
      {'o', 0}, // need 2
      {'n', 0},
    };
      
    // 
    foreach (var ch in text)
    {
      if (frequencies.ContainsKey(ch))
      {
        frequencies[ch]++;
      }
    }
      
    frequencies['l'] = frequencies['l'] / 2;
    frequencies['o'] = frequencies['o'] / 2;
    foreach (var ch in frequencies)
    {
      answer = Math.Min(answer, frequencies[ch.Key]);
    }
      
    return answer;
  }
  
  public int FindMaxLength(int[] nums) {
    var frequenciesIndex = new Dictionary<int, int>();
    frequenciesIndex[0] = -1;
    var prefixSum = 0;
    var maxLength = 0;
      
    for (var i = 0; i < nums.Length; i++)
    {
      var increaseBy = 1;
      if (nums[i] == 0) increaseBy = -1;
      prefixSum += increaseBy;

      if (frequenciesIndex.ContainsKey(prefixSum))
      {
        var previousIndex = frequenciesIndex[prefixSum];
        maxLength = Math.Max(maxLength, i - previousIndex);
      }
      else
      {
        frequenciesIndex[prefixSum] = i;
      }
    }

    return maxLength;
  }
  
  public StringBuilder sb = new StringBuilder();

  public int EqualPairs(int[][] grid) {
    var rows = new Dictionary<string, int>();
    var cols = new Dictionary<string, int>();

    // create the hashmaps
    foreach (var row in grid)
    {
      var rowKey = ArrayToStr(row);
      rows.TryGetValue(rowKey, out int count);
      rows[rowKey] = count + 1;
    }

    for (var col = 0; col < grid[0].Length; col++)
    {
      var newCol = new int[grid.Length];
      for (var row = 0; row < grid.Length; row++)
      {
        newCol[row] = grid[row][col];
      }
      var colKey = ArrayToStr(newCol);
      cols.TryGetValue(colKey, out int count);
      cols[colKey] = count + 1;
    }

    // iterate all rows, and add their count to our answer IF they have a col equivalent
    var totalPairs = 0;
    foreach (var rowPair in rows)
    {
      if (cols.ContainsKey(rowPair.Key))
      {
        // multiply because we need to know how many rows
        // can pair with how many cols
        // ex: 2 rows and 2 pairs make 4 combinations.
        totalPairs += rowPair.Value * cols[rowPair.Key];
      }
    }
    return totalPairs;
  }

  public string ArrayToStr(int[] nums)
  {
    foreach (var number in nums)
    {
      sb.Append($"{number}-");
    }
    var result = sb.ToString();
    sb.Clear();
    return result;
  }
  
  public bool CanConstruct(string ransomNote, string magazine) {
    // keep track of the available letters in magazine
    // then loop the note, return false if we run out of letters for it
    var magChars = new Dictionary<char, int>();
    foreach (var c in magazine)
    {
      magChars.TryGetValue(c, out int count);
      magChars[c] = count + 1;
    }
    
    foreach(var c in ransomNote)
    {
      if (!magChars.ContainsKey(c))
      {
        return false;
      }
      if (magChars[c] <= 0)
      {
        return false;
      }
      magChars[c]--;
    }

    return true;
  }
}