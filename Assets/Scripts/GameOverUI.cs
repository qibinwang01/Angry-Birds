using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private Animator animator;
    private int starCount=0;
    public GameObject failPig;
    public StarUI starUI1;
    public StarUI starUI2;
    public StarUI starUI3;
    private void Awake() {
        animator=GetComponent<Animator>();
    }
    private void Start() {
        //Show(1);
    }
    /// <summary>
    /// 显示多少个星星或者猪的头像
    /// </summary>
    /// <param name="starCount显示星星的个数"></param>
    public void Show(int starCount)
    {
        animator.SetTrigger("IsShow");
        this.starCount=starCount;
    }
    public void ShowStar()
    {
        if (starCount == 0)
        {
            failPig.SetActive(true);
        }
        if (starCount >= 1)
        {
            starUI1.Show();
        }
        if (starCount >= 2)
        {
            starUI2.Show();
        }
        if (starCount >= 3)
        {
            starUI3.Show();
        }
    }
    public void OnRestarButtonClick()
    {
        GameManerger.Instance.ReStart();
    }
    public void OnLevelListButtonClick()
    {
        GameManerger.Instance.LeveList();
    }
}
