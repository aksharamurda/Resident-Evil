using BattojutsuStd.Serialize;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattojutsuStd.Scriptable
{
    [CreateAssetMenu(fileName = "Stage", menuName = "Scriptable/Stage", order = 1)]
    public class Stage : ScriptableObject
    {
        [Header("UI")]
        public Sprite spriteCommingSoon;
        public Sprite spriteLocked;
        public Sprite spriteUnlocked;

        [Header("Zone")]
        public Zone zone;
        [Header("Levels")]
        public List<Level> levels = new List<Level>();
    }
}
