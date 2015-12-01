using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UamTTA.Model
{
    public class Transaction : ModelBase
    {
        public Transaction(DateTime trnasactionTime, double amount, Account sendingAccount, Account receivingAccount)
        {
            TransactionTime = trnasactionTime;
            Amount = amount;
            SendingAccount = sendingAccount;
            ReceivingAccount = receivingAccount;
        }

        public DateTime TransactionTime { get; set; }
        public double Amount { get; set; }
        public Account SendingAccount { get; set; }
        public Account ReceivingAccount { get; set; }

        public override string ToString()
        {
            return $"{SendingAccount.Name} sent (will send) {Amount} to {ReceivingAccount.Name} at {TransactionTime}";
        }
    }
}