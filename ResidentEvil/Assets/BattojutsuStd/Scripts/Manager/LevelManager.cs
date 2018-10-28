using BattojutsuStd.Serialize;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattojutsuStd.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        public Zone currentZone;
        public Level currentLevel;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

        }

        public void OnSetLevel(Zone z, Level l)
        {
            currentZone = z;
            currentLevel = l;
        }

        public void OnClearLevel()
        {
            currentZone = null;
            currentLevel = null;
        }
    }
}
