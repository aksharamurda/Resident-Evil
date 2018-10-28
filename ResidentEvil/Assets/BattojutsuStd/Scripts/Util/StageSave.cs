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
        public static void CreateStageData(StageData stageData)
        {
            Debug.Log(Application.persistentDataPath);
            if (!File.Exists(Application.persistentDataPath + "/" + stageData.zone.zoneName + ".bin"))
            {
                byte[] key = Convert.FromBase64String(Crypto.cryptoKey);
                BinaryFormatter binFormat = new BinaryFormatter();
                FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + stageData.zone.zoneName + ".bin", FileMode.Create);

                using (CryptoStream cryptoStream = Crypto.CreateEncryptionStream(key, fileStream))
                {
                    binFormat.Serialize(cryptoStream, stageData);
                }
            }

        }


        public static void UpdateStageData(StageData stageData)
        {
            byte[] key = Convert.FromBase64String(Crypto.cryptoKey);

            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + stageData.zone.zoneName + ".bin", FileMode.Create);

            using (CryptoStream cryptoStream = Crypto.CreateEncryptionStream(key, fileStream))
            {
                binFormat.Serialize(cryptoStream, stageData);
            }

        }

        public static StageData GetStageData(string zoneName)
        {
            byte[] key = Convert.FromBase64String(Crypto.cryptoKey);
            BinaryFormatter binFormat = new BinaryFormatter();
            FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + zoneName + ".bin", FileMode.Open);

            using (CryptoStream cryptoStream = Crypto.CreateDecryptionStream(key, fileStream))
            {
                return (StageData)binFormat.Deserialize(cryptoStream);
            }

        }
    }
}
