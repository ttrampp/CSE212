using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueque 3 items with different priorities and dequeue once. 
    // Expected Result: Itme with the highest priority should be returned.
    // Defect(s) Found: Original code failed to remove item from queue after returning it.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);        //priority 1
        priorityQueue.Enqueue("Medium", 5);     //priority 5
        priorityQueue.Enqueue("High", 10);      //priority 10

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Enqueue two items with same priority and dequeue once.
    // Expected Result: First item inserted should be returned --FIFO among equals.
    // Defect(s) Found: Original code didn't properly break ties by insertion order.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 7);
        priorityQueue.Enqueue("Second", 7);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("First", result);
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Dequeue from an empty queue
    // Expected Result: Exceptions should be thrown
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }
}