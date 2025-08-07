public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else if (value > Data)
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
        else    //If the balue is equal to this node's value, do nothing
        {
            //Duplicate.. do not insert
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2
        //first we check if the number we are looking for is the same as this one
        if (value == Data)
        {
            //yep, this is it--we found the value
            return true;
        }
        //if the number we are looking for is smaller than this one...
        else if (value < Data)
        {
            //...then we need to look to the left (smaller numbers live there)
            //if there's nothing on the left, then it is not here at all
            if (Left is null)
            {
                return false;
            }
            else
            {
                //if there is something on the left, then go check over there
                return Left.Contains(value);
            }
        }
        else //so now we know value > Data
        {
            //so that means we want to go right--because bigger numbers are on the right
            //if there's nothing on the right, then we definitely didn't find it
            if (Right is null)
            {
                return false;
            }
            else
            {
                //otherwise, keep searching on the right
                return Right.Contains(value);
            }
        }
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        //if there is nothing on the left, say the left side is 0 tall
        int leftHeight = Left?.GetHeight() ?? 0;

        //if there is nothing on the right, say the right side is 0 tall
        int rightHeight = Right?.GetHeight() ?? 0;

        //our height is 1--for the current node, pluse the taller side
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}