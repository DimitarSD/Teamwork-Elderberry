﻿namespace TeamElderberryProject
{
    using System;

    public struct TransactionData
    {
        private decimal amount;
        private DateTime date;

        public TransactionData(decimal amount, DateTime date)
            : this()
        {
            this.Amount = amount;
            this.Date = date;
        }

        public decimal Amount
        {
            get
            {
                return this.amount;
            }
            private set
            {
                if (value < 0)
                {
                    //throw new ArgumentOutOfRangeException("Amount cannot be negative number");
                    throw new InputException("Amount cannot be negative number!", new ArgumentOutOfRangeException());
                }

                this.amount = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            private set
            {
                this.date = value;
            }
        }
    }
}