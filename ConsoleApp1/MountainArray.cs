namespace ConsoleApp1;

// [0,3,2,1]
// goes up, then down, strictly.
public class MountainArray
{
  public static bool ValidMountainArray(int[] arr)
  {
    // simple O(n)
    var hasAscended = false;
    var hasDescended = false;
    if (arr.Length < 3)
    {
      return false;
    }

    for (var i = 1; i < arr.Length; i++)
    {
      if (arr[i - 1] < arr[i])
      {
        if (!hasAscended)
        {
          hasAscended = true;
        }

        if (hasDescended)
        {
          return false;
        }
      }
      else if (arr[i - 1] > arr[i])
      {
        if (!hasAscended)
        {
          return false;
        }

        hasDescended = true;
      }
      else
      {
        return false;
      }
    }

    // At some point, we should have gone up and down
    if (!hasAscended || !hasDescended)
    {
      return false;
    }

    return true;
  }
}