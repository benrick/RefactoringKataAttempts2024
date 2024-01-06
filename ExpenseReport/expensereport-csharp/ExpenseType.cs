using System.Collections.Generic;
using System.Linq;

namespace expensereport_csharp;

public class ExpenseType
{
    public int Id { get; init; }
    public string Name { get; init; }
    public bool IsMeal { get; init; }
    public int? Limit { get; init; }
    private ExpenseType(int id, string name, bool isMeal, int? limit = null)
    {
        Id = id;
        Name = name;
        IsMeal = isMeal;
        Limit = limit;
    }

    public static ExpenseType Dinner = new(0, "Dinner", true, 5000);
    public static ExpenseType Breakfast = new(1, "Breakfast", true, 1000);
    public static ExpenseType CarRental = new(2, "Car Rental", false);

    public static List<ExpenseType> All = [Dinner, Breakfast, CarRental];

    public static ExpenseType GetById(int id) => All.SingleOrDefault(x => x.Id == id);
}
