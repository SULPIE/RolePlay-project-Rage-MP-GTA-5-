using GTANetworkAPI;
using System.Collections.Generic;

namespace Server.AccountInfo
{
    public static class AccountHandlerDictionary
    {
        private static readonly int _max_players = 2000;
        public static Dictionary<Player, Account> accounts = new Dictionary<Player, Account>(_max_players);

        public static Account GetAccount(Player player)
        {
            if(accounts.ContainsKey(player)) {
                return accounts[player];
            }
            else {
                return null;
            }
        }

        public static void AddAccountToDictionary(Player player, Account account)
        {
            try
            {
                accounts.Add(player, account);
            }
            catch
            {

            }
        }

        public static void RemoveAccountFromDictionary(Player player)
        {
            accounts.Remove(player);
        }

        public static int DictionarySize()
        {
            return (int)accounts.Count;
        }
    }
}
