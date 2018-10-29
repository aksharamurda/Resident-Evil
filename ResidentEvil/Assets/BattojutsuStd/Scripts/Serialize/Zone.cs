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
        public string zoneName = "Zone";
        public string zoneTitle;
        public int zoneStarGoal;
        public int zoneStarSaved;
        public bool isCommingSoon;
        public bool isCompleted;
        public bool isUnlocked;

    }

}
