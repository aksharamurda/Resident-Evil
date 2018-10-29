using BattojutsuStd.Manager;
using BattojutsuStd.Scriptable;
using BattojutsuStd.Serialize;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattojutsuStd.UI
{
    public class UIPanelLevel : MonoBehaviour
    {
        public TextMeshProUGUI textLevelName;
        public Sprite starActive;
        public List<Image> starImage = new List<Image>();

        private Zone zone;
        private Level level;

        public void InitUIPanelLevel(Zone z, Level l)
        {
            zone = z;
            level = l;

            textLevelName.text = level.ID.ToString();

            if (level.isUnlocked)
                GetComponent<Image>().color = Color.white;

            for (int x=0; x < level.levelStar; x++)
            {
                starImage[x].sprite = starActive;
            }

        }

        public void OnButtonSelectLevel()
        {
            if (!level.isUnlocked)
                return;

            LevelManager.instance.OnSetLevel(zone, level);

        }
    }
}
