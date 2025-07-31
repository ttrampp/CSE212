using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using NuGet.Frameworks;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // TODO Start Problem 1
        //BASE CASE
        //If the number is 0 or less, we stop here and return 0--avoiding going on forever
        if (n <= 0)
        {
            return 0;
        }

        //SMALLER PROBLEM
        //Square n, call function again using a smaller number, add them together
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // TODO Start Problem 2
        //BASE CASE
        //If the word we've built is the right length, we are done --- add it to the results list
        if (word.Length == size)
        {
            //Add the word to the list of results--save the permutation
            results.Add(word);
        }
        else
        {
            //SMALLER PROBLEM
            //try adding each of the remaining letters-- One at a time
            for (int i = 0; i < letters.Length; i++)
            {
                //remove the letter at index i so we don't reuse it in the same word; new string without the letter at i
                string remaining = letters.Remove(i, 1);

                //add that letter to the word we are building
                // recursivly call the function again with the smaller list and the longer word
                PermutationsChoose(results, remaining, size, word + letters[i]);
            }
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        //create the dictionary if it is null
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // TODO Start Problem 3
        //^comment says to start problem here but i added a CREATE DICTIONARY above the base case
        // if we have already solved this exact problem, use the save
        if (remember.ContainsKey(s))
            return remember[s];

        //SMALLER PROBLEM
        //Total ways to reach stair s = ways to get to (s-1) + (s-2) + (s-3)
        decimal ways = CountWaysToClimb(s - 1, remember) + CountWaysToClimb(s - 2, remember) + CountWaysToClimb(s - 3, remember);

        //Save the answer so we don't have to recalculate it next time
        remember[s] = ways;

        //return the total ways for this 's'
        return ways;


        // Solve using recursion
        //decimal ways = CountWaysToClimb(s - 1) + CountWaysToClimb(s - 2) + CountWaysToClimb(s - 3);
        //return ways;
        
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///	
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // TODO Start Problem 4
        //BASE CASE
        //If the pattern has no * left, then it is complete
        //meaning we have made all the replacements, so we just add it to the final
        if (!pattern.Contains('*'))
        {
            results.Add(pattern);
            return;
        }

        //RECURSIVE CASE
        //Let us find the 1st * and replace it with both '0' and '1'. Each one creates a smaller problem to solve.
        int index = pattern.IndexOf('*');   //Find the first wildcard (*)

        //Smaller version 1 -- replace * with 0
        string withZero = pattern.Substring(0, index) + '0' + pattern[(index + 1)..];
        WildcardBinary(withZero, results);  //Recursively solve this smaller problem

        //Smaller version 2 -- replace * with 1
        string withOne = pattern.Substring(0, index) + '1' + pattern[(index + 1)..];
        WildcardBinary(withOne, results);  //Recursively solve this one too

    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
        }

        // currPath.Add((1,2)); // Use this syntax to add to the current path

        // TODO Start Problem 5
        // ADD CODE HERE

        //BASE CASE

        //Make sure this move is valid--bounds, not a wall, not already visited
        if (!maze.IsValidMove(currPath, x, y))
            return;

        //add current position to the path
        currPath.Add((x, y));

        //If we have reached the end square, save the path and backtrack
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());   //save this completed path
            currPath.RemoveAt(currPath.Count - 1);  //backtrack to explore other paths
            return;
        }

        //RECURSIVE CASE
        //SMALLER PROBLEMS
        //Explore each direction one step at a time
        SolveMaze(results, maze, x + 1, y, currPath); //try moving down
        SolveMaze(results, maze, x - 1, y, currPath); //try moving up
        SolveMaze(results, maze, x, y + 1, currPath); //try moving right
        SolveMaze(results, maze, x, y - 1, currPath); //try moving left

        //Bactracking
        //Remove the last square before returning
        currPath.RemoveAt(currPath.Count - 1);
        //}

        // results.Add(currPath.AsString()); // Use this to add your path to the results array keeping track of complete maze solutions when you find the solution.
    }
}