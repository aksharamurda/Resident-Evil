using BattojutsuStd.Serialize;
using BattojutsuStd.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattojutsuStd.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        public Zone currentZone;
        public Level currentLevel;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

        }

        public void OnSetLevel(Zone z, Level l)
        {
            currentZone = z;
            currentLevel = l;
        }

        public void OnClearLevel()
        {
            currentZone = null;
            currentLevel = null;
        }

        public void DebugSaveZoneData()
        {
            currentZone.isCompleted = true;

            StageData nextStageData = StageSave.GetStageData("Zone" + (currentZone.ID + 1));
            if (nextStageData == null)
                return;

            if (currentZone.isCompleted)
            {
                nextStageData.zone.isUnlocked = true;
                StageSave.UpdateStageData(nextStageData);
            }
        }

        public void DebugSaveLevelData()
        {
            currentLevel.isCompleted = true;

            StageData currentStageData = StageSave.GetStageData(currentZone.zoneName);
            if (currentStageData == null)
                return;

            for (int i=0; i < currentStageData.levels.Count; i++)
            {
                //CURRENT LEVEL
                if (currentStageData.levels[i].ID == currentLevel.ID)
                {
                    if (currentLevel.isCompleted)
                    {
                        currentStageData.levels[i] = currentLevel;

                        int index = i + 1;
                        if (index < currentStageData.levels.Count)
                            currentStageData.levels[index].isUnlocked = true;

                        StageSave.UpdateStageData(currentStageData);
                    }
                }
            }

        }
    }
}
