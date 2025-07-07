public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Create an array of doubles with size equal to count
        double[] result = new double[length];

        // Step 2: Loop from 0 to count - 1
        for (int i = 0; i < length; i++)
        {
            // Step 3: For each index i, compute (start * (i + 1)) and store in the array
            result[i] = number * (i + 1);
        }

            // Step 4: Return the array
            return result; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Get the last 'amount' elements from the list using GetRange()
        List<int> toMove = data.GetRange(data.Count - amount, amount);

        // Step 2: Remove those 'amount' elements from the end of the list using RemoveRange()
        data.RemoveRange(data.Count - amount, amount);

        // Step 3: Insert the saved elements at the beginning using InsertRange()
        data.InsertRange(0, toMove);
        
        // Step 4: The list should now be rotated to the right

    }
}
