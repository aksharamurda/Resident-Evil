using BattojutsuStd.Manager;
using BattojutsuStd.Scriptable;
using BattojutsuStd.Serialize;
using BattojutsuStd.Util;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BattojutsuStd.UI
{
    public class UIMenuManager : MonoBehaviour
    {
        public static UIMenuManager instance;

        public StageManager stageManager;
        private Transform parentUIPanelZone;
        private Transform parentUIPanelLevel;
        private Transform buttonBackUIPanelLevel;
        private Transform uiCoinText;
        private Transform uiStarTotalText;

        //private StageData currentStageData;

        void Awake()
        {
            if (LevelManager.instance == null)
                Instantiate(stageManager.levelManager);

            instance = this;

            InitData();

            uiCoinText = GameObject.Find("UICoinText").transform;
            parentUIPanelZone = GameObject.Find("UIPanelZoneViewport").transform;
            parentUIPanelLevel = GameObject.Find("UIPanelLevelViewport").transform;
            buttonBackUIPanelLevel = GameObject.Find("UIButtonBackPanelLevel").transform;
            buttonBackUIPanelLevel.gameObject.SetActive(false);

            uiStarTotalText = GameObject.Find("UITotalStarText").transform;

            CreatePanelUIZone();
            InitUIProfile();
        }

        void InitData()
        {
            if (stageManager == null)
                stageManager = Resources.Load("Scriptable/Stage/Stage Manager") as StageManager;

            PlayerData playerProfile = new PlayerData();
            PlayerSave.CreatePlayerData(playerProfile);

            foreach (Stage stage in stageManager.listStages)
            {
                if (stage.zone.isCommingSoon)
                    continue;

                StageData stageData = new StageData(stage.zone, stage.levels);
                StageSave.CreateStageData(stageData);
            }


        }

        public void InitUIProfile()
        {
            PlayerData playerProfile = PlayerSave.GetPlayerData();
            uiCoinText.GetComponent<TextMeshProUGUI>().text = playerProfile.playerCoin.ToString();

            int totalStarSaved = 0;
            int totalStarGoal = 0;
            foreach (Stage stg in stageManager.listStages)
            {
                totalStarGoal += stg.zone.zoneStarGoal;
                totalStarSaved += stg.zone.zoneStarSaved;
                Debug.Log(totalStarGoal);
            }

            uiStarTotalText.GetComponent<TextMeshProUGUI>().text = "TOTAL : " + totalStarSaved + "/" + totalStarGoal;

        }

        public void CreatePanelUIZone()
        {
            foreach (Transform t in parentUIPanelLevel)
            {
                Destroy(t.gameObject);
            }

            for (int x = 0; x < stageManager.listStages.Count; x++)
            {

                StageData currentStageData = StageSave.GetStageData(stageManager.listStages[x].zone.zoneName);
                GameObject newPanel = Instantiate(stageManager.prefabPanelZone);

                if (stageManager.listStages[x].zone.isCommingSoon)
                    newPanel.GetComponent<UIPanelZone>().InitUIPanelZone(stageManager.listStages[x]);
                else
                {
                    Stage stage = ScriptableObject.CreateInstance(typeof(Stage)) as Stage;
                    stage.spriteLocked = stageManager.listStages[x].spriteLocked;
                    stage.spriteUnlocked = stageManager.listStages[x].spriteUnlocked;
                    stage.spriteCommingSoon = stageManager.listStages[x].spriteCommingSoon;
                    stage.zone = currentStageData.zone;
                    stage.levels = currentStageData.levels;

                    newPanel.GetComponent<UIPanelZone>().InitUIPanelZone(stage);
                }
                    

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

            buttonBackUIPanelLevel.gameObject.SetActive(true);
        }

        public void OnButtonBackUIPanelLevel()
        {
            buttonBackUIPanelLevel.gameObject.SetActive(false);
            CreatePanelUIZone();
            LevelManager.instance.OnClearLevel();
        }
    }
}
