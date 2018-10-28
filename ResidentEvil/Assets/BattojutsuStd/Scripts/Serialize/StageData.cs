using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattojutsuStd.Serialize
{
    [System.Serializable]
    public class StageData
    {
        public Zone zone;
        public List<Level> levels = new List<Level>();

        public StageData()
        {

        }

        public StageData(Zone z, List<Level> l)
        {
            zone = z;
            levels = l;
        }
    }
}
