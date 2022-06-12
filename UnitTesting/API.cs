using System;

namespace UnitTesting
{
    public class API
    {
        public virtual void Call()
        {
            Console.WriteLine("CALLING API");
        }

        public virtual string GetPaymentMethod(int money)
        {
            if (money < 1000)
            {
                return "PayPal";
            }
            else
            {
                return "Debit card";
            }
        }
    }
}
