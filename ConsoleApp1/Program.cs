//
// Console.WriteLine(DuplicateVal.CheckIfExist(new int[] { 7, 1, 14, 11 }));
// Console.WriteLine(DuplicateVal.CheckIfExist(new int[] { 3, 1, 7, 11 }));
// Console.WriteLine(DuplicateVal.CheckIfExist(new int[] { -2,0,10,-19,4,6,-8 }));
//
// var checker = new HeightChecker();
// Console.WriteLine(checker.Run(new int[]{1,1,4,2,1,3}));

using System.Text.Json;

var problem = new DisappearedNums();
Console.WriteLine(JsonSerializer.Serialize(problem.FasterSolution(new int[]{4,3,2,7,8,2,3,1})));
