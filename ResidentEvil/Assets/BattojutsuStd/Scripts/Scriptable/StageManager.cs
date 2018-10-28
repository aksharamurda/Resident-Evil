using BattojutsuStd.Serialize;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattojutsuStd.Scriptable
{
    [CreateAssetMenu(fileName = "Stage Manager", menuName = "Scriptable/Stage Manager", order = 1)]
    public class StageManager : ScriptableObject
    {
        public GameObject prefabPanelZone;
        public GameObject prefabPanelLevel;
        public List<Stage> listStages = new List<Stage>();
    }
}
