using BattojutsuStd.Scriptable;
using BattojutsuStd.Serialize;
using BattojutsuStd.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattojutsuStd.UI
{
    public class UIMenuManager : MonoBehaviour
    {
        public static UIMenuManager instance;

        public StageManager stageManager;
        private Transform parentUIPanelZone;
        private Transform parentUIPanelLevel;
        private StageData currentStageData;

        void Awake()
        {
            instance = this;
            

            foreach (Stage stage in stageManager.listStages)
            {
                StageData stageData = new StageData(stage.zone, stage.levels);
                StageSave.CreateStageData(stageData);
            }

            parentUIPanelZone = GameObject.Find("UIPanelZoneViewport").transform;
            parentUIPanelLevel = GameObject.Find("UIPanelLevelViewport").transform;
            CreatePanelUIZone();
        }

        public void CreatePanelUIZone()
        {
            
            for (int x = 0; x < stageManager.listStages.Count; x++)
            {
                currentStageData = StageSave.GetStageData(stageManager.listStages[x].zone.zoneName);
                GameObject newPanel = Instantiate(stageManager.prefabPanelZone);
                newPanel.GetComponent<UIPanelZone>().InitUIPanelZone(currentStageData.zone, currentStageData.levels);
                newPanel.transform.SetParent(parentUIPanelZone);
                newPanel.transform.localScale = Vector3.one;
            }
        }

        public void CreatePanelUILevel(Zone s, List<Level> ls) 
        {
            foreach (Transform t in parentUIPanelZone)
            {
                Destroy(t.gameObject);
            }

            for (int y = 0; y < ls.Count; y++)
            {
                GameObject newPanel = Instantiate(stageManager.prefabPanelLevel);
                newPanel.GetComponent<UIPanelLevel>().InitUIPanelLevel(s, ls[y]);
                newPanel.transform.SetParent(parentUIPanelLevel);
                newPanel.transform.localScale = Vector3.one;
            }

        }
    }
}
