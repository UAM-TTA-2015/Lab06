using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UamTTA.Tests
{
    [TestFixture]
    public class BudgetFactoryTests
    {
        private BudgetFactory _budgetFactory;

        [SetUp]
        public void SetUp()
        {
            _budgetFactory = new BudgetFactory();
        }

        public IEnumerable<DateTime[]> WeeklyBudgetTestCases
        {
            get
            {
                yield return new[] { new DateTime(2015, 10, 2), new DateTime(2015, 10, 8) };
                yield return new[] { new DateTime(2015, 10, 29), new DateTime(2015, 11, 4) };
                yield return new[] { new DateTime(2015, 12, 26), new DateTime(2016, 1, 1) };
                yield return new[] { new DateTime(2015, 7, 28), new DateTime(2015, 8, 3) };
            }
        }

        [Test]
        [TestCaseSource(nameof(WeeklyBudgetTestCases))]
        public void Can_Create_Weekly_Budget_From_Template(DateTime expectedStartDate, DateTime expectedEndDate)
        {
            // Arrange
            var template = new BudgetTemplate(Duration.Weekly, "Weekly Budget");

            // Act
            Budget budget = _budgetFactory.CreateBudget(template, expectedStartDate);

            // Assert
            Assert.That(budget, Is.Not.Null);
            Assert.That(budget.ValidFrom, Is.EqualTo(expectedStartDate));
            Assert.That(budget.ValidTo, Is.EqualTo(expectedEndDate));
        }

        public IEnumerable<DateTime[]> MonthlyBudgetTestCases
        {
            get
            {
                yield return new[] { new DateTime(2015, 10, 2), new DateTime(2015, 11, 1) };
                yield return new[] { new DateTime(2015, 10, 31), new DateTime(2015, 11, 30) };
                yield return new[] { new DateTime(2016, 1, 31), new DateTime(2016, 2, 29) };
                yield return new[] { new DateTime(2015, 1, 31), new DateTime(2015, 2, 28) };
                yield return new[] { new DateTime(2015, 12, 1), new DateTime(2015, 12, 31) };
                yield return new[] { new DateTime(2015, 12, 15), new DateTime(2016, 1, 14) };
                yield return new[] { new DateTime(2015, 7, 31), new DateTime(2015, 8, 30) };
            }
        }

        [Test]
        [TestCaseSource(nameof(MonthlyBudgetTestCases))]
        public void Can_Create_Monthly_Budget_From_Template(DateTime expectedStartDate, DateTime expectedEndDate)
        {
            // Arrange
            var template = new BudgetTemplate(Duration.Monthly, "Monthly Budget");

            // Act
            Budget budget = _budgetFactory.CreateBudget(template, expectedStartDate);

            // Assert
            Assert.That(budget, Is.Not.Null);
            Assert.That(budget.ValidFrom, Is.EqualTo(expectedStartDate));
            Assert.That(budget.ValidTo, Is.EqualTo(expectedEndDate));
        }

        public IEnumerable<DateTime[]> QuarterlyBudgetTestCases
        {
            get
            {
                yield return new[] { new DateTime(2015, 10, 2), new DateTime(2016, 1, 1) };
                yield return new[] { new DateTime(2015, 10, 31), new DateTime(2015, 10, 30) };
                yield return new[] { new DateTime(2016, 1, 31), new DateTime(2016, 4, 29) };
                yield return new[] { new DateTime(2015, 1, 31), new DateTime(2015, 4, 30) };
                yield return new[] { new DateTime(2015, 12, 15), new DateTime(2016, 3, 14) };
                yield return new[] { new DateTime(2015, 7, 31), new DateTime(2015, 10, 30) };
            }
        }

        [Test]
        [TestCaseSource(nameof(QuarterlyBudgetTestCases))]
        public void Can_Create_Quarterly_Budget_From_Template(DateTime expectedStartDate, DateTime expectedEndDate)
        {
            var template = new BudgetTemplate(Duration.Quarterly, "Quarterly Budget");

            Budget budget = _budgetFactory.CreateBudget(template, expectedStartDate);

            Assert.That(budget, Is.Not.Null);
            Assert.That(budget.ValidFrom, Is.EqualTo(expectedStartDate));
            Assert.That(budget.ValidTo, Is.EqualTo(expectedEndDate));
        }

        public IEnumerable<DateTime[]> YearlyBudgetTestCases
        {
            get
            {
                yield return new[] { new DateTime(2015, 10, 2), new DateTime(2016, 10, 2) };
                yield return new[] { new DateTime(2015, 10, 31), new DateTime(2016, 10, 31) };
                yield return new[] { new DateTime(2016, 2, 29), new DateTime(2017, 2, 28) };
            }
        }

        [Test]
        [TestCaseSource(nameof(YearlyBudgetTestCases))]
        public void Can_Create_Yearly_Budget_From_Template(DateTime expectedStartDate, DateTime expectedEndDate)
        {
            var template = new BudgetTemplate(Duration.Yearly, "Quarterly Budget");

            Budget budget = _budgetFactory.CreateBudget(template, expectedStartDate);

            Assert.That(budget, Is.Not.Null);
            Assert.That(budget.ValidFrom, Is.EqualTo(expectedStartDate));
            Assert.That(budget.ValidTo, Is.EqualTo(expectedEndDate));
        }
    }
}