using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public GameObject lockUI;
    public GameObject StarUI;
    public Text score;
    private MapLeveUI mapLeveUI;
    private int mapID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 是显示锁还是关卡
    /// </summary>
    /// <param name="starCount=-1的时候显示锁"></param>
    public void Show(int starCount,MapLeveUI mapLeveUI,int mapID)
    {
        this.mapLeveUI=mapLeveUI;
        this.mapID=mapID;
        Button button=GetComponent<Button>();
        
        if (starCount < 0)
        {
            button.enabled=false;
            GetComponent<Button>().enabled=false;
            lockUI.SetActive(true);
            //lockUI.GetComponent<Image>().sprite=GetComponent<Image>().sprite;
            StarUI.SetActive(false);
        }
        else
        {
            button.enabled=true;
            lockUI.SetActive(false); 
            StarUI.SetActive(true);
            score.text=starCount.ToString();
        }
    }
    public void OnClick()
    {
        mapLeveUI.OnMapButtonClick(mapID);
    }
}
