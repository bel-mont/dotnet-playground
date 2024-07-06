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

public class Leetcode881 {
    public int NumRescueBoats(int[] people, int limit) {
        // the simplest approach is pairing the lightest person with the heaviest person.
        // if the heaviest person cannot be paired with the lightest one, it means they need their own boat.

        // sort the array and then check the pairs with 2 pointers
        Array.Sort(people);
        var ans = 0;
        var lightIndex = 0;
        var heavyIndex = people.Length - 1;
        while (lightIndex <= heavyIndex)
        {
            var heaviest = people[heavyIndex];
            var lightest = people[lightIndex];
            // if the combined weight is equal or less than the limit, we pair them both.
            // otherwise we send the heavy person on their own
            if (lightest + heaviest <= limit)
            {
                lightIndex++;
            }
            heavyIndex--;
            ans++;
        }
        
        return ans;
    }
}

public class Leetcode1323 {
    public int Maximum69Number (int num) {
        var ans = num;

        var chars = num.ToString();
        for (var i = 0; i < chars.Length; i++)
        {
            var newArr = chars.ToCharArray();
            newArr[i] = newArr[i] == '6' ? '9' : '6';
            var newNumb = int.Parse(new string(newArr));
            if (newNumb > ans) ans = newNumb;
        }
        return ans;
    }
}

public class Leetcode1323Greediest {
    public int Maximum69Number (int num) {
        var ans = num;
        var chars = num.ToString();
        for (var i = 0; i < chars.Length; i++)
        {
            var newArr = chars.ToCharArray();
            // there is no point in changing 9 to 6, it automatically means the number will be smaller
            if (newArr[i] == '6')
            {
                newArr[i] = '9';
                var newNumb = int.Parse(new string(newArr));
                if (newNumb > ans) ans = newNumb;
                // whatever the result of this comparison is, we should break out since there
                // is no way for any other number to be bigger.
                break;
            }
        }
        return ans;
    }
}

public class Leetcode1323GreediestWizardry {
    public int Maximum69Number (int num) {
        // taken from the editorial because it blew my mind

        // Since we start with the lowest digit, initialize currDigit = 0.
        var numCopy = num;
        var indexSix = -1;
        var currDigit = 0;

        // Check every digit of 'numCopy' from low to high.
        while (numCopy > 0)
        {
            // If the current digit is '6', record it as the highest digit of 6.
            if (numCopy % 10 == 6) 
                indexSix = currDigit;
            // Move to next digit
            numCopy /= 10;
            currDigit++;
        }

        // If we don't find any digit of '6', return the original number,
        // otherwise, increment 'num' by the difference made by the first '6'.
        return indexSix == -1 ? num : num + 3 * (int)Math.Pow(10, indexSix);
    }
}