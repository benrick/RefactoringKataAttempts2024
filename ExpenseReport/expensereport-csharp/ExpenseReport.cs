using System;
using System.Collections.Generic;

namespace expensereport_csharp
{
    public class ExpenseReport
    {
        public List<string> PrintedLines { get; set; }
        public void PrintReport(List<Expense> expenses)
        {
            PrintedLines = new List<string>();

            int total = 0;
            int mealExpenses = 0;

            Console.WriteLine(Save("Expenses " + DateTime.Now));
            
            foreach (Expense expense in expenses)
            {
                if (expense.type == ExpenseType.DINNER || expense.type == ExpenseType.BREAKFAST)
                {
                    mealExpenses += expense.amount;
                }

                String mealOverExpensesMarker =
                    expense.type == ExpenseType.DINNER && expense.amount > 5000 ||
                    expense.type == ExpenseType.BREAKFAST && expense.amount > 1000
                        ? "X"
                        : " ";

                Console.WriteLine(Save(expense.Name + "\t" + expense.amount + "\t" + mealOverExpensesMarker));

                total += expense.amount;
            }

            Console.WriteLine(Save("Meal expenses: " + mealExpenses));
            Console.WriteLine(Save("Total expenses: " + total));
        }

        private string Save(string line)
        {
            PrintedLines.Add(line);
            return line;
        }
    }
}
