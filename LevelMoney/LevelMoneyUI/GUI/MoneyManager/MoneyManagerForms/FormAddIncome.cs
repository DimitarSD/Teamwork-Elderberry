﻿namespace MoneyManagerForms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using TeamElderberryProject;
    using TeamElderberryProject.Interfaces;
    public partial class FormAddIncome : Form
    {
        private const int CommentLenghtManimumValue = 30;
        private const int AmmountMinimumValue = 0;

        public FormAddIncome()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Location = this.Owner.Location;
            this.Left += this.Owner.ClientSize.Width / 2 - this.Width / 2;
            this.Top += this.Owner.ClientSize.Height / 2 - this.Height / 2;
        }


        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            var ammount = decimal.Parse(boxMoney.Text);
            var comment = boxComment.Text;
            var date = DateTime.Parse(dateTimePicker1.Text);

            Income incomeToAdd = null;

            switch (dropTypes.Text)
            {
                case "Irregular":
                    incomeToAdd = new IrregularIncome(new TransactionData(ammount, date), comment);
                    break;
                case "Regular":
                    incomeToAdd = new RegularIncome(new TransactionData(ammount, date), comment);
                    break;
                default: MessageBox.Show("Please fill the form!");
                    break;
            }

            ExcelExporter exporter = new ExcelExporter();


            if (ammount < AmmountMinimumValue)
            {
                MessageBox.Show(GlobalMessages.NonNegativeInput, GlobalMessages.ExpenseTitle);

                boxMoney.Clear();
            }
            else if (comment == string.Empty)
            {
                MessageBox.Show(GlobalMessages.CommentFieldErrorMessage, GlobalMessages.ExpenseTitle);
            }
            else
            {
                exporter.Export(incomeToAdd);

                MessageBox.Show(GlobalMessages.IncomeAdded, GlobalMessages.ExpenseTitle);

                boxMoney.Clear();
                boxComment.Clear();
                dropTypes.ResetText();
                dateTimePicker1.ResetText();
            }

            //Old way printing with json serializer
            //ExportInFile.SaveIncomeData(incomeToAdd);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form4 = new FormStart();
            form4.Show(this);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void boxComment_TextChanged(object sender, EventArgs e)
        {
            var commentLenght = boxComment.TextLength;
            if (commentLenght > CommentLenghtManimumValue)
            {
                string errorMessage = string.Format(GlobalMessages.ExpenseCommentLenghtErrorMessage, CommentLenghtManimumValue);
                MessageBox.Show(errorMessage, GlobalMessages.ExpenseTitle);
                boxComment.Clear();
            }
        }
    }
}
