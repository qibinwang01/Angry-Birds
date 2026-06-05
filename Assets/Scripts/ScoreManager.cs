using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance {get;private set;}
    public GameObject scorePrefab;
    public Sprite[] score_3000;
    public Sprite[] score_5000;
    public Sprite[] score_10000;
    private Dictionary<int,Sprite[]> scoreDict;
    private void Awake() {
        Instance=this;
    }
    private void Start() {
        scoreDict=new Dictionary<int, Sprite[]>();
        scoreDict.Add(3000,score_3000);
        scoreDict.Add(5000,score_5000);
        scoreDict.Add(10000,score_10000);
    }
    public void ShowScore(Vector3 position,int score)
    {
        GameObject scoreGo=GameObject.Instantiate(scorePrefab,position,Quaternion.identity);
        Sprite[] scoreArry;
        scoreDict.TryGetValue(score,out scoreArry);
        //生成0~length-1的随机数
        int index=Random.Range(0,scoreArry.Length);
        Sprite sprite=scoreArry[index];
        scoreGo.GetComponent<SpriteRenderer>().sprite=sprite;
        Destroy(scoreGo,1f);
    }
}
