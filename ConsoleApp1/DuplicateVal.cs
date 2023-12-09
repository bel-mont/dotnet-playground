
public class DuplicateVal
{
  public static bool CheckIfExist(int[] arr)
  {
    // not sure how to make this O(n) :(
    // Store dupe results in dictionary? Then if it exists while looping, return true
    var dict = new Dictionary<int, bool> { { arr[0], true } };
    for (var i = 1; i < arr.Length; i++)
    {
      dict.TryGetValue(arr[i] * 2, out var doubleExists);
      if (doubleExists)
      {
        return true;
      }

      if (arr[i] % 2 == 0)
      {
        dict.TryGetValue(arr[i] / 2, out var halfExists);
        if (halfExists)
        {
          return true;
        }
      }

      dict.TryAdd(arr[i], true);
    }

    return false;
  }
}