namespace Server.AccountInfo
{
    public static class MoneyTransaction
    {
        public static void GiveMoney(Account account, int money)
        {
            account.Money += money;
        }

        public static void TakeMoney(Account account, int money)
        {
            if((account.Money - money) < 0)
            {
                account.Money = 0;
                return;
            }
            else
            {
                account.Money -= money;
            }
        }
    }
}
