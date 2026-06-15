using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 11: Demonstrate Access Modifiers
    /// </summary>
    public static class Exercise11
    {
        public class BaseAccount
        {
            public string AccountHolder = "Alice Johnson";       // accessible everywhere
            private double balance = 1000.0;                      // accessible only within this class
            protected string accountType = "Savings";             // accessible in this class and derived classes

            public double GetBalance() => balance; // public method to expose private field safely

            protected void Log(string message) => Console.WriteLine($"[BaseAccount] {message}");
        }

        public class DerivedAccount : BaseAccount
        {
            public void ShowDetails()
            {
                // public - accessible
                Console.WriteLine($"AccountHolder (public, inherited): {AccountHolder}");

                // protected - accessible because this is a derived class
                Console.WriteLine($"accountType (protected, inherited): {accountType}");

                // private 'balance' is NOT accessible here directly - must use public method
                Console.WriteLine($"Balance (via public method GetBalance()): {GetBalance():C}");

                Log("Accessed protected Log() method from derived class.");
            }
        }

        public static void Run()
        {
            var derived = new DerivedAccount();
            derived.ShowDetails();

            Console.WriteLine();

            var baseAccount = new BaseAccount();
            // From a non-derived class (here, Exercise11.Run), only public members are accessible:
            Console.WriteLine($"AccountHolder (public, non-derived access): {baseAccount.AccountHolder}");
            Console.WriteLine($"Balance (via public method, non-derived access): {baseAccount.GetBalance():C}");
            // baseAccount.balance      -> compile error (private)
            // baseAccount.accountType  -> compile error (protected)
            Console.WriteLine("Note: 'balance' (private) and 'accountType' (protected) are not " +
                               "accessible directly from this non-derived class.");
        }
    }
}
