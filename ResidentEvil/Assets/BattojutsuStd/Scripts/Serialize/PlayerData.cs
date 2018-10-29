using UnityEngine;
using System.Collections;
using BattojutsuStd.Util;

namespace BattojutsuStd.Serialize
{
    [System.Serializable]
    public class PlayerData
    {
        public string playerName = "[Player Name]";
        public int playerCoin;

        public PlayerData()
        {

        }

        public PlayerData(string pName)
        {
            playerName =  pName;
        }
    }
}
