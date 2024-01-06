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
                if (expense.type.IsMeal)
                {
                    mealExpenses += expense.amount;
                }

                string mealOverExpensesMarker = expense.IsOverLimitMeal ? "X" : " ";

                Console.WriteLine(Save(expense.type.Name + "\t" + expense.amount + "\t" + mealOverExpensesMarker));

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
