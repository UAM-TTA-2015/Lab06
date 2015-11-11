namespace UamTTA
{
    public class Account : ModelBase
    {
        public Account()
        {
            
        }

        public Account(string name, decimal balance, Account clearingAccount = null, string relatedBankAccount = null,
            decimal? expectedIncomes = null, decimal? targetBalance = null)
        {
            Name = name;
            Balance = balance;
            ClearingAccount = clearingAccount;
            RequiresClearing = clearingAccount != null;
            RelatedBankAccount = relatedBankAccount;
            ExpectedIncomes = expectedIncomes;
            TargetBalance = targetBalance;
        }

        public string Name { get; set; }

        public string RelatedBankAccount { get; set; }

        public decimal Balance { get; set; }

        public decimal? ExpectedIncomes { get; set; }

        public decimal? TargetBalance { get; set; }

        public bool RequiresClearing { get; set; }

        public Account ClearingAccount { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, RelatedBankAccount: {RelatedBankAccount}, Balance: {Balance}, ExpectedIncomes: {ExpectedIncomes}, TargetBalance: {TargetBalance}, RequiresClearing: {RequiresClearing}, ClearingAccount: {ClearingAccount}";
        }
    }
}
