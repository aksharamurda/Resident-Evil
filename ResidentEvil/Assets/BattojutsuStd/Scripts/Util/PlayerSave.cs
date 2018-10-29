using UnityEngine;
using System.Collections;
using BattojutsuStd.Serialize;
using System.IO;
using System;
using BattojutsuStd.Security;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace BattojutsuStd.Util
{
    public class PlayerSave
    {
        public const string fileName = "PlayerProfile";
        public static void CreatePlayerData(PlayerData playerData)
        {
            Debug.Log(Application.persistentDataPath);
            if (!File.Exists(Application.persistentDataPath + "/" + EasyMD5.Hash(fileName) + ".bin"))
            {
                byte[] key = Convert.FromBase64String(Crypto.cryptoKey);
                BinaryFormatter binFormat = new BinaryFormatter();
                FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + EasyMD5.Hash(fileName) + ".bin", FileMode.Create);

                using (CryptoStream cryptoStream = Crypto.CreateEncryptionStream(key, fileStream))
                {
                    binFormat.Serialize(cryptoStream, playerData);
                }
            }

        }

        public static void UpdatePlayerData(PlayerData playerData)
        {
            byte[] key = Convert.FromBase64String(Crypto.cryptoKey);

            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + EasyMD5.Hash(fileName) + ".bin", FileMode.Create);

            using (CryptoStream cryptoStream = Crypto.CreateEncryptionStream(key, fileStream))
            {
                binFormat.Serialize(cryptoStream, playerData);
            }

        }

        public static PlayerData GetPlayerData(string playerName = fileName)
        {
            if (File.Exists(Application.persistentDataPath + "/" + EasyMD5.Hash(playerName) + ".bin"))
            {
                byte[] key = Convert.FromBase64String(Crypto.cryptoKey);
                BinaryFormatter binFormat = new BinaryFormatter();
                FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + EasyMD5.Hash(playerName) + ".bin", FileMode.Open);

                using (CryptoStream cryptoStream = Crypto.CreateDecryptionStream(key, fileStream))
                {
                    return (PlayerData)binFormat.Deserialize(cryptoStream);
                }
            }

            return null;
        }
    }
}
