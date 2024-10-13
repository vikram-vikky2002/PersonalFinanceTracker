# Personal Finance Tracker
A console-based personal finance tracker that helps users manage their financial transactions by tracking credits and debits. It features a PIN-based security system to ensure only authorized users can access the application and perform transactions. The app also saves data locally to maintain the balance and transaction history across sessions.

## Features
- Credit and Debit Transactions:
Add credit or debit transactions, and the app automatically updates your balance.
- Transaction History:
View a detailed history of all transactions with the date, type, amount, and updated balance.
- PIN Security:
Each time you open the app or perform a transaction, it prompts for a PIN to ensure security.
- Change PIN:
Option to change the security PIN anytime with confirmation.
- Persistent Data:
Saves balance, transactions, and PIN to a local JSON file, allowing data to be restored when the app is reopened.
- User-friendly Console Interface:
Easy-to-navigate menu for managing transactions and PIN securely.
## Technologies Used
C# (.NET Core)
JSON for data storage and retrieval
## How to Use
### 1. Set up the project:

- Ensure you have .NET SDK installed on your machine.
- Download the code or clone the repository.
```bash
git clone <repository-url>
cd PersonalFinanceTracker
```
### 2. Run the application:
Open the terminal/command prompt in the project folder and run the following command:

```bash
dotnet run
```
### 3. Set a Security PIN:
When you launch the application for the first time, you will be prompted to set your security PIN.
### 4. Perform Transactions:

- Choose Credit or Debit and enter the amount.
- The app will prompt for your PIN before performing the transaction.
### 5. View Balance and History:

- Use the "Show Balance" option to display your current balance.
- Use the "Show Transactions" option to view all your transactions.
### 6. Change PIN:

- Select the "Change PIN" option to update your security PIN.
Menu Option

```text
--- Personal Finance Tracker ---
1. Credit Amount
2. Debit Amount
3. Show Transactions
4. Show Balance
5. Change PIN
6. Exit
```

## Example Run

```text
Set your security PIN: ****
Data loaded! Current Balance: $500.00

--- Personal Finance Tracker ---
1. Credit Amount
2. Debit Amount
3. Show Transactions
4. Show Balance
5. Change PIN
6. Exit
Select an option: 1
Enter PIN: ****
Enter amount to credit: 200
Credited: $200.00. New Balance: $700.00
```

## Data Persistence
The app stores the following data in a JSON file (finance_data.json):

- Balance: The last saved balance.
- Transactions: A list of all transactions.
- PIN: The current security PIN.

## Security
- PIN Input: The app hides the PIN entry with `*` for security.
- Validation: Only authorized users with the correct PIN can access the app or perform transactions.

## License
This project is released under the MIT License. Feel free to use, modify, and distribute it.

## Contributing
If you have suggestions or improvements, feel free to submit a pull request.

