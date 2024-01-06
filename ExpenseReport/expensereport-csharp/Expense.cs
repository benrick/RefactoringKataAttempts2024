namespace expensereport_csharp;

public class Expense
{
    public ExpenseType type;
    public int amount;

    public bool IsMeal => type.IsMeal;
    public bool IsOverLimitMeal => type.IsMeal && amount > type.Limit;
}
