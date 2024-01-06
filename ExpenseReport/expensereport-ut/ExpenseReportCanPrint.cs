using expensereport_csharp;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class ExpenseReportCanPrint
    {
        private ExpenseReport _expenseReport;

        [SetUp]
        public void Setup()
        {
            _expenseReport = new ExpenseReport();
        }

        [Test]
        public void Canary()
        {
            Assert.Pass();
        }

        [Test]
        public void EmptyReport()
        {
            var expenses = new List<Expense>();

            _expenseReport.PrintReport(expenses);

            List<string> printedLines = _expenseReport.PrintedLines;

            Assert.That(printedLines.Count, Is.EqualTo(3));
            Assert.That(printedLines[0], Contains.Substring("Expenses"));
            Assert.That(printedLines[1], Is.EqualTo("Meal expenses: 0"));
            Assert.That(printedLines[2], Is.EqualTo("Total expenses: 0"));
        }

        [Test]
        [TestCase(100, 1)]
        [TestCase(200, 0)]
        [TestCase(300, 2)]
        [TestCase(300, 3)]
        public void SingleItemReport(int amount, int typeId)
        {
            ExpenseType type = ExpenseType.GetById(typeId);
            List<Expense> expenses = [new() { amount = amount, type = type }];

            _expenseReport.PrintReport(expenses);

            List<string> printedLines = _expenseReport.PrintedLines;

            Assert.That(printedLines.Count, Is.EqualTo(4));
            Assert.That(printedLines[0], Contains.Substring("Expenses"));
            Assert.That(printedLines[1], Contains.Substring($"\t{amount}\t "));
            Assert.That(printedLines[2], Is.EqualTo($"Meal expenses: {(type.IsMeal ? amount : 0)}"));
            Assert.That(printedLines[3], Is.EqualTo($"Total expenses: {amount}"));
        }

        [Test]
        [TestCase(100, 200)]
        public void MultiItemReport(int amount1, int amount2)
        {
            List<Expense> expenses = [new() { amount = amount1, type = ExpenseType.Breakfast },
                new() { amount = amount2, type = ExpenseType.Dinner }];

            _expenseReport.PrintReport(expenses);

            List<string> printedLines = _expenseReport.PrintedLines;

            Assert.That(printedLines[^1], Is.EqualTo($"Total expenses: {amount1 + amount2}"));
        }

        [Test]
        public void OverLimitItemReport()
        {
            List<Expense> expenses = [
                new() { amount = 1001, type = ExpenseType.Breakfast },
                new() { amount = 1000, type = ExpenseType.Breakfast },
                new() { amount = 2001, type = ExpenseType.Lunch },
                new() { amount = 2000, type = ExpenseType.Lunch },
                new() { amount = 5001, type = ExpenseType.Dinner },
                new() { amount = 5000, type = ExpenseType.Dinner }
            ];

            _expenseReport.PrintReport(expenses);

            List<string> printedLines = _expenseReport.PrintedLines;

            Assert.That(printedLines[1], Contains.Substring($"\t1001\tX"));
            Assert.That(printedLines[2], Contains.Substring($"\t1000\t "));
            Assert.That(printedLines[3], Contains.Substring($"\t2001\tX"));
            Assert.That(printedLines[4], Contains.Substring($"\t2000\t "));
            Assert.That(printedLines[5], Contains.Substring($"\t5001\tX"));
            Assert.That(printedLines[6], Contains.Substring($"\t5000\t "));
        }
    }
}