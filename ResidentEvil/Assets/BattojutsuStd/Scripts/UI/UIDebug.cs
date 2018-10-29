using BattojutsuStd.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDebug : MonoBehaviour {

	public void OnDebugZoneButton()
    {
        LevelManager.instance.DebugSaveZoneData();
    }

    public void OnDebugLevelButton()
    {
        LevelManager.instance.DebugSaveLevelData();
    }
}
