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
        public Image imagePanelZone;
        public GameObject panelItem;
        public TextMeshProUGUI zoneTitle;
        public TextMeshProUGUI zoneMaxItem;

        private Stage stage;

        public void InitUIPanelZone(Stage s)
        {
            stage = s;
        }

        private void Start()
        {

            zoneTitle.text = stage.zone.zoneTitle;

            if (stage.zone.isUnlocked)
                imagePanelZone.sprite = stage.spriteUnlocked;
            else
                imagePanelZone.sprite = stage.spriteLocked;



            if (stage.zone.isCommingSoon)
                imagePanelZone.sprite = stage.spriteCommingSoon;

            zoneMaxItem.text = stage.zone.zoneStarSaved + "/" + stage.zone.zoneStarGoal;
            panelItem.SetActive(!stage.zone.isCommingSoon && stage.zone.isUnlocked);

        }

        public void OnButtonSelectZone()
        {
            if (!stage.zone.isUnlocked || stage.zone.isCommingSoon)
                return;

            UIMenuManager.instance.CreatePanelUILevel(stage.zone, stage.levels);
        }
    }
}
