using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace BattojutsuStd.Serialize
{
    [Serializable]
    public class Level
    {
        public int ID;
        public string levelName;
        [Range(0, 3)]
        public int levelStar;
        public bool isTutorial;
        public bool isUnlocked;
        public bool isCompleted;
    }
}
