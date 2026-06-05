using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance{get;private set;}
    public GameSo gameSo;
    public MapLeveUI mapLeveUI;
    private void Awake() {
        Instance=this;
    }
    private void Start() {
        mapLeveUI.ShowMapGo(gameSo.mapArry);
    }
    public void SetSelectedMap(int mapID)
    {
        gameSo.selectedMapID=mapID;
    }
    public int[] GetSelectedMap()
    {
        //Debug.Log(gameSo.selectedMapID-1);
        return gameSo.mapArry[gameSo.selectedMapID-1].starNumberOfLevel;
    }
    public void SetSelectedLevel(int levelID)
    {
        gameSo.selectedLevelID=levelID;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
}
