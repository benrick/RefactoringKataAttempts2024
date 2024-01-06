namespace expensereport_csharp;

public class Expense
{
    public ExpenseType type;
    public int amount;

    public string Name => type switch
    {
        ExpenseType.DINNER => "Dinner",
        ExpenseType.BREAKFAST => "Breakfast",
        ExpenseType.CAR_RENTAL => "Car Rental",
    };
}
