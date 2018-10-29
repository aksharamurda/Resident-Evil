using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BattojutsuStd.Serialize
{
    [Serializable]
    public class Zone
    {
        public int ID;
        public string zoneName;
        public string zoneTitle;
        public int zoneItemMax;
        public int currentItemMax;
        public bool isCompleted;
        public bool isUnlocked;
    }

}
