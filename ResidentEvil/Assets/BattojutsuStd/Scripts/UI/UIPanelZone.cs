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

        public Stage stage;
        /*
        public Zone zone;
        public List<Level> levels = new List<Level>();
        */

        public void InitUIPanelZone(Stage s)
        {
            stage = s;

            zoneTitle.text = stage.zone.zoneTitle;

            if (stage.zone.isUnlocked && !stage.zone.isCommingSoon)
                GetComponent<Image>().color = Color.white;
            else if (!stage.zone.isUnlocked && stage.zone.isCommingSoon)
                GetComponent<Image>().color = Color.gray;

            zoneMaxItem.text = stage.zone.currentItemMax + "/" + stage.zone.zoneItemMax;

        }

        public void OnButtonSelectZone()
        {
            if (!stage.zone.isUnlocked || !stage.zone.isCommingSoon)
                return;

            UIMenuManager.instance.CreatePanelUILevel(stage.zone, stage.levels);
        }
    }
}
