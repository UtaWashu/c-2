using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoThree
{
    class WithdrawFromAccountException : Exception
    {
        public WithdrawFromAccountException(string message) : base(message) { }
    }

    class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }

    class Account
    {
        public int Number { get; set; }
        public int PINCode { get; set; }
        private double _balance;

        public double Balance
        {
            get { return _balance; }
            set
            {
                if (value < 0)
                {
                    throw new InsufficientBalanceException("Incorrect balance value: " + value);
                }
                _balance = value;
            }
        }

        public virtual void WithdrawFromAccount(double amount)
        {
            try
            {
                if (amount < 0 || amount > Balance)
                {
                    throw new WithdrawFromAccountException("Incorrect amount to withdraw from account: " + amount);
                }
                Balance -= amount;
                Console.WriteLine("Withdrawal successful. Balance: " + Balance);
            }
            catch (WithdrawFromAccountException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    class NormalAccount : Account
    {
        public override void WithdrawFromAccount(double amount)
        {
            base.WithdrawFromAccount(amount);
        }
    }

    class PreferentialAccount : Account
    {
        public override void WithdrawFromAccount(double amount)
        {
            base.WithdrawFromAccount(amount);
        }
    }

    class ATM
    {
        public string IdentificationNumber { get; set; }
        public string Address { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NormalAccount normalAccount = new NormalAccount();
                normalAccount.Number = 1234567890;
                normalAccount.PINCode = 1111;
                normalAccount.Balance = 1000;

                PreferentialAccount preferentialAccount = new PreferentialAccount();
                preferentialAccount.Number = 987654321;
                preferentialAccount.PINCode = 2222;
                preferentialAccount.Balance = 2000;

                ATM atm = new ATM();
                atm.IdentificationNumber = "ATM001";
                atm.Address = "Street Proletarskaya, 10";

                Console.WriteLine("Обычный счёт:");
                try
                {
                    normalAccount.WithdrawFromAccount(500);
                    normalAccount.WithdrawFromAccount(700);
                    normalAccount.WithdrawFromAccount(-200);
                    normalAccount.WithdrawFromAccount(2000);
                }
                catch (WithdrawFromAccountException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (InsufficientBalanceException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.WriteLine("\n");
                Console.WriteLine("Лгетный счёт:");
                try
                {
                    preferentialAccount.WithdrawFromAccount(500);
                    preferentialAccount.WithdrawFromAccount(1500);
                }
                catch (WithdrawFromAccountException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (InsufficientBalanceException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    }
}