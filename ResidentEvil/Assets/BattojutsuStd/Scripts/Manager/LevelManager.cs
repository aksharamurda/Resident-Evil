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
            if(currentZone.zoneStarSaved >= currentZone.zoneStarGoal)
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

            DebugSaveZoneData();

            if (currentLevel.levelStar > 2)
                currentLevel.isCompleted = true;

            StageData currentStageData = StageSave.GetStageData(currentZone.zoneName);
            if (currentStageData == null)
                return;

            for (int i=0; i < currentStageData.levels.Count; i++)
            {
                //CURRENT LEVEL
                if (currentStageData.levels[i].ID == currentLevel.ID)
                {
                    if (currentLevel.levelStar > 0)
                    {
                        
                        //IF STAR COUNT BLA BLA BLA
                        if(currentLevel.levelStar > currentStageData.levels[i].levelStar)
                        {
                            currentZone.zoneStarSaved = (currentZone.zoneStarSaved - currentStageData.levels[i].levelStar) + currentLevel.levelStar;
                        }

                        currentStageData.zone = currentZone;
                        currentStageData.levels[i] = currentLevel;

                        //NEXT LEVEL UNLOCK
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
