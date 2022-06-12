using System;

namespace UnitTesting
{
    public class Bank
    {
        public static API Api;

        public static int RequestLoan(int money, int months)
        {
            if (months > 6)
            {
                throw new Exception("Max count months should be 6.");
            }
            else if (months < 3)
            {
                throw new Exception("Min count months should be 3.");
            }

            int bonus = 0;

            if (money <= 0)
            {
                throw new Exception("Money can't be negative or zero.");
            }
            else if (money >= 1001)
            {
                bonus = 10;
            }

            int monthly = money / months;

            Api.Call();
            string method = Api.GetPaymentMethod(money);

            return monthly - bonus;
        }
    }
}
