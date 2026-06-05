using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLeveUI : MonoBehaviour
{
    public GameObject mapListGo;
    public GameObject levelListGo;
    public List<MapUI> mapUIList;
    public GameObject levelTemplatePrefab;
    public GameObject levelGridGo;
    public void ShowMapGo(MapSo[] mapArry)
    {
        mapListGo.SetActive(true);
        levelListGo.SetActive(false);
        UpadateMapUIList(mapArry);
    }
    public void UpadateMapUIList(MapSo[] mapArray)
    {
        for(int i = 0; i < mapArray.Length; i++)
        {
            mapUIList[i].Show(mapArray[i].starNumberOfMap,this,i+1);
        }
    }
    public void OnMapButtonClick(int mapID)
    {
        LevelSelectManager.Instance.SetSelectedMap(mapID);
        //Debug.Log("现在是第"+mapID+"张图");
        ShowLeveGrid();
    }
    private void ShowLeveGrid()
    {
        mapListGo.SetActive(false);
        levelListGo.SetActive(true);
        int[] starNumberOfLevel=LevelSelectManager.Instance.GetSelectedMap();
        foreach(Transform child in levelGridGo.transform)
        {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < starNumberOfLevel.Length; i++)
        {
            GameObject go=GameObject.Instantiate(levelTemplatePrefab);
            go.GetComponent<RectTransform>().SetParent(levelGridGo.transform);
            go.GetComponent<LevelUI>().Show(starNumberOfLevel[i],i+1,this);
        }
    }
    public void OnLevelButtonClick(int levelID)
    {
        LevelSelectManager.Instance.SetSelectedLevel(levelID);
    }
    public void OnReturnButtonClick()
    {
        mapListGo.SetActive(true);
        levelListGo.SetActive(false);
    }
}
