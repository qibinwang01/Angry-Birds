using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public GameObject unLockUI;
    public GameObject lockUI;
    public Text text;
    public GameObject star0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    private MapLeveUI mapLeveUI;
    private int levelID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 关卡选择显示
    /// </summary>
    /// <param name="starCount=-1表示关卡锁定>=0表示关卡可以游玩"></param>
    public void Show(int starCount,int levelID,MapLeveUI mapLeveUI)
    {
        this.mapLeveUI=mapLeveUI;
        this.levelID=levelID;
        text.text=levelID.ToString();
        star0.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        if (starCount < 0)
        {
            unLockUI.SetActive(false);
            lockUI.SetActive(true);
        }
        else
        {
            unLockUI.SetActive(true);
            lockUI.SetActive(false);
            if (starCount == 0)
            {
                star0.SetActive(true);
            }
            else if (starCount == 1)
            {
                star1.SetActive(true);
            }
            else if (starCount == 2)
            {
                star2.SetActive(true);
            }
            else
            {
                star3.SetActive(true);
            }
        }
    }
    public void OnClik()
    {
        mapLeveUI.OnLevelButtonClick(levelID);
    }
}
