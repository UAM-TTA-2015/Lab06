﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace UamTTA
{
    public class Budget : ModelBase
    {
        public Budget(
            DateTime validFrom, DateTime validTo, String name = "Default Budget",
            IEnumerable<Transfer> operations = null, IEnumerable<Account> relatedAccounts = null,
            Account clearingAccount = null)
        {
            ValidFrom = validFrom;
            ValidTo = validTo;
            Name = name;
            Operations = operations ?? Enumerable.Empty<Transfer>();
            RelatedAccounts = relatedAccounts ?? Enumerable.Empty<Account>();
        }

        public DateTime ValidFrom { get; }

        public DateTime ValidTo { get; }

        public String Name { get; }

        public IEnumerable<Account> RelatedAccounts { get; }

        public IEnumerable<Transfer> Operations { get; }

        public override string ToString()
        {
            return $"ValidFrom: {ValidFrom}, ValidTo: {ValidTo}, RelatedAccounts: {RelatedAccounts.ToElementsString()}, Operations: {Operations.ToElementsString()}";
        }
    }
}