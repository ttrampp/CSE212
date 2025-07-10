/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Create a queue with a valid size and an invalid size
        // Expected Result: Valid size (5), should set max_size to 5; Invalid size (0), should default max_size to 10
        Console.WriteLine("Test 1");
        var csValid = new CustomerService(5);
        Console.WriteLine(csValid); //Expect max_size = 5

        var csInvalid = new CustomerService(0);
        Console.WriteLine(csInvalid);  //Expect max_size = 10

        // Defect(s) Found: None
        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add customers to a queue with limited size.--1st two should add successfully, and 3rd should trigger max number message
        // Expected Result: Morgan and August are added & Adam is not added..gives error message
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(2);
        cs2.TestAdd("Morgan", "001", "Forgot password");
        cs2.TestAdd("August", "002", "System crash");
        cs2.TestAdd("Adam", "003", "Printer exploded! Too many Customers in Queue."); //Should not be added
        Console.WriteLine(cs2);     //should only show Morgan and August
        //Defect(s)found: Was allowing extra customer due to incorrect > condition now fixed with >=
        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below

        //Test 3
        //Scenario: Add one customer to the queue, then serve that customer
        //Expected Result: Customer should be removed from the queue & their details should be displayed correctly
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(2);
        cs3.TestAdd("John", "004", "Cannot connect to Wi-Fi");
        cs3.TestServe();  //should print John (004) : Cannot connect to Wi-Fi
        Console.WriteLine(cs3); //should show empty queue
        //Defect(s)found: Previously printed wrong customer due to removing before accessing; now fixed
        Console.WriteLine("=================");

        //Test 4
        //Scenario: Attempt to serve a customer when the queue is empty
        //Expected Result: Should print:  No customers to serve. No crash or removal should occur
        Console.WriteLine("Test 4");
        var cs4 = new CustomerService(2);
        cs4.TestServe();  //Should print: No customers to serve.
        //Defect(s) found: Previously caused a crash due to missing empty-check; now fixed
        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        //Changed to prevent adding too many customers
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        //added check to prevent removing from empty queue
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers to serve.");
            return;
        }
        //Moved removal after saving the customer
        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }
    
    // ===================+=========
    // METHODS USED ONLY FOR TESTING
    // =================

    public void TestAdd(string name, string accountId, string problem) {
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    public void TestServe() {
        if (_queue.Count == 0) {
            Console.WriteLine("No customers to serve.");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}