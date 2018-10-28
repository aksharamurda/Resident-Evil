using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattojutsuStd.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

        }
    }
}
