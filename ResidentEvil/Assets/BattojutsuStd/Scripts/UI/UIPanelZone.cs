using BattojutsuStd.Scriptable;
using BattojutsuStd.Serialize;
using BattojutsuStd.Util;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattojutsuStd.UI
{
    public class UIPanelZone : MonoBehaviour
    {
        public TextMeshProUGUI zoneTitle;
        public TextMeshProUGUI zoneMaxItem;

        public Zone zone;
        public List<Level> levels = new List<Level>();

        public void InitUIPanelZone(Zone z, List<Level> ls)
        {
            zone = z;
            levels = ls;
            zoneTitle.text = zone.zoneTitle;

            if (zone.isUnlocked)
                GetComponent<Image>().color = Color.white;

            zoneMaxItem.text = zone.currentItemMax + "/" + zone.zoneItemMax;

        }

        public void OnButtonSelectZone()
        {
            if (!zone.isUnlocked)
                return;

            UIMenuManager.instance.CreatePanelUILevel(zone, levels);
        }
    }
}
