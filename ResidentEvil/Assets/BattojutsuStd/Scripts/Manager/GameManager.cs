using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattojutsuStd.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        void Awake()
        {
            instance = this;
        }
    }
}
