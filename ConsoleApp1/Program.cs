//
// Console.WriteLine(DuplicateVal.CheckIfExist(new int[] { 7, 1, 14, 11 }));
// Console.WriteLine(DuplicateVal.CheckIfExist(new int[] { 3, 1, 7, 11 }));
// Console.WriteLine(DuplicateVal.CheckIfExist(new int[] { -2,0,10,-19,4,6,-8 }));
//
// var checker = new HeightChecker();
// Console.WriteLine(checker.Run(new int[]{1,1,4,2,1,3}));

using System.Text.Json;

var graphs = new Leetcode695();
var grid = new[]
{
  // new [] {0,1},
  // new [] {1,2},
  // new [] {3,4},
  new [] {0,1,1},
  new [] {1,1,0},
};

var res = graphs.MaxAreaOfIsland(grid);

Console.WriteLine("RESULT");
Console.WriteLine(JsonSerializer.Serialize(res));
