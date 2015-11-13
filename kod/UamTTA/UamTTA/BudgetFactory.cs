using System;

namespace UamTTA
{
    public class BudgetFactory
    {
        public Budget CreateBudget(BudgetTemplate template, DateTime startDate)
        {
            DateTime endDate = default(DateTime);
            switch (template.DefaultDuration)
            {
                case Duration.Weekly:
                    endDate = AddWeek(startDate);
                    break;

                case Duration.Monthly:
                    endDate = AddMonths(startDate, 1);
                    break;

                case Duration.Quarterly:
                    endDate = AddMonths(startDate, 3);
                    break;

                case Duration.Yearly:
                    endDate = AddMonths(startDate, 12);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return template.DefaultName != null ? new Budget(startDate, endDate, template.DefaultName) : new Budget(startDate, endDate);
        }

        private static DateTime AddWeek(DateTime startDate)
        {
            return startDate.AddDays(6);
        }

        private static DateTime AddMonths(DateTime startDate, int no)
        {
            DateTime endDate = startDate.AddMonths(no);
            int daysInStartDate = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            int daysInNextMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);
            if (daysInNextMonth >= 30 && (endDate.Day < daysInNextMonth || daysInNextMonth == daysInStartDate))
                endDate = endDate.AddDays(-1);
            return endDate;
        }
    }
}