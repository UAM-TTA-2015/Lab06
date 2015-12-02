﻿using System.Collections.Generic;
using System.Linq;

namespace UamTTA
{
    public class BudgetTemplate : ModelBase
    {
        public BudgetTemplate(Duration defaultDuration, string defaultName = null, IEnumerable<Account> defaultAccounts = null)
        {
            DefaultAccounts = defaultAccounts ?? Enumerable.Empty<Account>();
            DefaultDuration = defaultDuration;
            DefaultName = defaultName;
        }

        public string DefaultName { get; }

        public IEnumerable<Account> DefaultAccounts { get; }

        public Duration DefaultDuration { get; }

        public override string ToString()
        {
            return $"DefaultName: {DefaultName}, DefaultAccounts: {DefaultAccounts.ToElementsString()}, DefaultDuration: {DefaultDuration}";
        }
    }
}