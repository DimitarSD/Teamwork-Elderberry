﻿namespace TeamElderberryProject
{
    using System.Collections.Generic;

    using TeamElderberryProject.Interfaces;

    public sealed class Account : IAccount
    {
        // Implements Singleton design pattern with lazy loading
        private static readonly Account instance = new Account();
        private IList<ITransaction> transactions = new List<ITransaction>();
        private decimal balance = 0;

        private Account()
        {
        }

        public static Account Instance
        {
            get
            {
                return instance;
            }
        }

        public decimal Balance
        {
            get
            {
                this.CalculateBalance();
                return this.balance;
            }

            private set
            {
                this.balance = value;
            }
        }

        public IList<ITransaction> Transactions
        {
            get { return this.transactions; }
        }

        public void AddTransaction(ITransaction transaction)
        {
            this.transactions.Add(transaction);
        }

        private void CalculateBalance()
        {
            foreach (var transaction in this.transactions)
            {
                this.balance += transaction.BalanceValue();
            }
        }
    }
}