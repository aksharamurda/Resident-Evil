using BattojutsuStd.Scriptable;
using BattojutsuStd.Security;
using BattojutsuStd.Serialize;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using UnityEngine;

namespace BattojutsuStd.Util
{
    public class StageSave
    {
        public static void CreateStageData(StageData stageData, bool force = false)
        {
            //Debug.Log("Create " + stageData.zone.zoneName);
            PlayerData playerProfile = PlayerSave.GetPlayerData();
            if (force)
            {
                byte[] key = Convert.FromBase64String(Crypto.cryptoKey);
                BinaryFormatter binFormat = new BinaryFormatter();
                FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + EasyMD5.Hash(playerProfile.playerName + stageData.zone.zoneName) + ".bin", FileMode.Create);

                using (CryptoStream cryptoStream = Crypto.CreateEncryptionStream(key, fileStream))
                {
                    binFormat.Serialize(cryptoStream, stageData);
                }
            }
            else
            {
                if (!File.Exists(Application.persistentDataPath + "/" + EasyMD5.Hash(playerProfile.playerName + stageData.zone.zoneName) + ".bin"))
                {
                    byte[] key = Convert.FromBase64String(Crypto.cryptoKey);
                    BinaryFormatter binFormat = new BinaryFormatter();
                    FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + EasyMD5.Hash(playerProfile.playerName + stageData.zone.zoneName) + ".bin", FileMode.Create);

                    using (CryptoStream cryptoStream = Crypto.CreateEncryptionStream(key, fileStream))
                    {
                        binFormat.Serialize(cryptoStream, stageData);
                    }
                }
            }


        }


        public static void UpdateStageData(StageData stageData)
        {
            PlayerData playerProfile = PlayerSave.GetPlayerData();
            byte[] key = Convert.FromBase64String(Crypto.cryptoKey);

            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + EasyMD5.Hash(playerProfile.playerName + stageData.zone.zoneName) + ".bin", FileMode.Create);

            using (CryptoStream cryptoStream = Crypto.CreateEncryptionStream(key, fileStream))
            {
                binFormat.Serialize(cryptoStream, stageData);
            }

        }

        public static StageData GetStageData(string zoneName)
        {
            PlayerData playerProfile = PlayerSave.GetPlayerData();
            if (File.Exists(Application.persistentDataPath + "/" + EasyMD5.Hash(playerProfile.playerName + zoneName) + ".bin"))
            {
                try
                {
                    byte[] key = Convert.FromBase64String(Crypto.cryptoKey);
                    BinaryFormatter binFormat = new BinaryFormatter();
                    FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + EasyMD5.Hash(playerProfile.playerName + zoneName) + ".bin", FileMode.Open);

                    using (CryptoStream cryptoStream = Crypto.CreateDecryptionStream(key, fileStream))
                    {
                        return (StageData)binFormat.Deserialize(cryptoStream);
                    }
                }
                catch
                {
                    Debug.Log("Data has been manual change!");
                    StageManager stManager = Resources.Load("Scriptable/Stage/Stage Manager") as StageManager;
                    foreach (Stage stage in stManager.listStages)
                    {
                        StageData stageData = new StageData(stage.zone, stage.levels);
                        StageSave.CreateStageData(stageData, true);
                    }

                    return GetStageData(zoneName);
                }

            }

            return null;
        }

    }
}
