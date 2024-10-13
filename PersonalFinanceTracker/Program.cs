
using System.Text.Json;

public class Transaction
{
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceAfterTransaction { get; set; }

    public Transaction() { }  // Parameterless constructor required for JSON deserialization

    public Transaction(string type, decimal amount, decimal balanceAfterTransaction)
    {
        Date = DateTime.Now;
        Type = type;
        Amount = amount;
        BalanceAfterTransaction = balanceAfterTransaction;
    }
}

public class PersonalFinanceTracker
{
    private decimal balance;
    private List<Transaction> transactions;
    private string pin;
    private const string DataFile = "finance_data.json";  // File to save data

    public PersonalFinanceTracker()
    {
        LoadData();  // Load data from JSON file at startup
    }

    private void LoadData()
    {
        if (File.Exists(DataFile))
        {
            string jsonData = File.ReadAllText(DataFile);
            var data = JsonSerializer.Deserialize<TrackerData>(jsonData);
            balance = data.Balance;
            transactions = data.Transactions;
            pin = data.Pin;
            Console.WriteLine($"Data loaded! Current Balance: {balance:C}");
        }
        else
        {
            // Initialize with default values if no saved data is found
            balance = 0;
            transactions = new List<Transaction>();
            pin = SetInitialPin();
            Console.WriteLine("No previous data found. New tracker initialized.");
        }
    }

    private string SetInitialPin()
    {
        Console.Write("Set your security PIN: ");
        return Console.ReadLine();
    }

    private void SaveData()
    {
        var data = new TrackerData
        {
            Balance = balance,
            Transactions = transactions,
            Pin = pin
        };
        string jsonData = JsonSerializer.Serialize(data);
        File.WriteAllText(DataFile, jsonData);
    }

    private bool ValidatePin()
    {
        Console.Write("Enter PIN: ");
        string enteredPin = Console.ReadLine();

        if (enteredPin == pin)
        {
            Console.WriteLine("PIN validated successfully!\n");
            return true;
        }
        else
        {
            Console.WriteLine("Incorrect PIN! Access denied.\n");
            return false;
        }
    }

    public void CreditAmount(decimal amount)
    {
        if (!ValidatePin()) return;

        balance += amount;
        AddTransaction("Credit", amount);
        Console.WriteLine($"Credited: {amount:C}. New Balance: {balance:C}");
        SaveData();  // Save data after every transaction
    }

    public void DebitAmount(decimal amount)
    {
        if (!ValidatePin()) return;

        if (amount > balance)
        {
            Console.WriteLine("Insufficient balance.");
            return;
        }

        balance -= amount;
        AddTransaction("Debit", amount);
        Console.WriteLine($"Debited: {amount:C}. New Balance: {balance:C}");
        SaveData();  // Save data after every transaction
    }

    public void ShowTransactions()
    {
        if (!ValidatePin()) return;

        Console.WriteLine("\nTransaction History:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"{transaction.Date}: {transaction.Type} - {transaction.Amount:C} (Balance: {transaction.BalanceAfterTransaction:C})");
        }
    }

    public decimal GetBalance()
    {
        if (!ValidatePin()) return 0;
        return balance;
    }

    public void ChangePin()
    {
        if (!ValidatePin()) return;

        Console.Write("Enter new PIN: ");
        string newPin = Console.ReadLine();

        Console.Write("Confirm new PIN: ");
        string confirmPin = Console.ReadLine();

        if (newPin == confirmPin)
        {
            pin = newPin;
            Console.WriteLine("PIN changed successfully!\n");
            SaveData();  // Save data after PIN change
        }
        else
        {
            Console.WriteLine("PINs do not match. Try again.\n");
        }
    }

    private void AddTransaction(string type, decimal amount)
    {
        transactions.Add(new Transaction(type, amount, balance));
    }
}

public class TrackerData
{
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string Pin { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        PersonalFinanceTracker tracker = new PersonalFinanceTracker();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--- Personal Finance Tracker ---");
            Console.WriteLine("1. Credit Amount");
            Console.WriteLine("2. Debit Amount");
            Console.WriteLine("3. Show Transactions");
            Console.WriteLine("4. Show Balance");
            Console.WriteLine("5. Change PIN");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter amount to credit: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal creditAmount))
                    {
                        tracker.CreditAmount(creditAmount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    break;

                case "2":
                    Console.Write("Enter amount to debit: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal debitAmount))
                    {
                        tracker.DebitAmount(debitAmount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    break;

                case "3":
                    tracker.ShowTransactions();
                    break;

                case "4":
                    decimal balance = tracker.GetBalance();
                    Console.WriteLine($"Current Balance: {balance:C}");
                    break;

                case "5":
                    tracker.ChangePin();
                    break;

                case "6":
                    exit = true;
                    Console.WriteLine("Exiting... Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
