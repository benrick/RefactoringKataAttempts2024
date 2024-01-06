using expensereport_csharp;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
        [TestCase(100, ExpenseType.BREAKFAST)]
        [TestCase(200, ExpenseType.DINNER)]
        [TestCase(300, ExpenseType.CAR_RENTAL)]
        public void SingleItemReport(int amount, ExpenseType type)
        {
            List<Expense> expenses = [new () {amount = amount, type = type}];

            _expenseReport.PrintReport(expenses);

            List<string> printedLines = _expenseReport.PrintedLines;

            Assert.That(printedLines.Count, Is.EqualTo(4));
            Assert.That(printedLines[0], Contains.Substring("Expenses"));
            Assert.That(printedLines[1], Contains.Substring($"\t{amount}\t "));
            Assert.That(printedLines[2], Is.EqualTo($"Meal expenses: {(type == ExpenseType.CAR_RENTAL ? 0 : amount)}"));
            Assert.That(printedLines[3], Is.EqualTo($"Total expenses: {amount}"));
        }

        [Test]
        [TestCase(100, ExpenseType.BREAKFAST, 200, ExpenseType.DINNER)]
        public void MultiItemReport(int amount1, ExpenseType type1, int amount2, ExpenseType type2)
        {
            List<Expense> expenses = [new () {amount = amount1, type = type1}, new () {amount = amount2, type = type2}];

            _expenseReport.PrintReport(expenses);

            List<string> printedLines = _expenseReport.PrintedLines;

            Assert.That(printedLines[^1], Is.EqualTo($"Total expenses: {amount1 + amount2}"));
        }

        [Test]
        [TestCase(1001, ExpenseType.BREAKFAST, 200, ExpenseType.DINNER)]
        [TestCase(5001, ExpenseType.DINNER, 200, ExpenseType.BREAKFAST)]
        public void OverLimitItemReport(int overAmount1, ExpenseType type1, int amount2, ExpenseType type2)
        {
            List<Expense> expenses = [new () {amount = overAmount1, type = type1}, new () {amount = amount2, type = type2}];

            _expenseReport.PrintReport(expenses);

            List<string> printedLines = _expenseReport.PrintedLines;

            Assert.That(printedLines[1], Contains.Substring($"\t{overAmount1}\tX"));
            Assert.That(printedLines[2], Contains.Substring($"\t{amount2}\t "));
            Assert.That(printedLines[^1], Is.EqualTo($"Total expenses: {overAmount1 + amount2}"));
        }
    }
}