using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManerger : MonoBehaviour
{
    public static GameManerger Instance { get; private set; }
    public Bird[] birdlist;
    private int index = -1;
    public int pigCount;
    public int pigDeadCount;
    private FollowTarget cameraFollowTarget;
    public GameOverUI gameOverUI;
    public GameSo gameSo;
    private bool isGameOver = false;
    private GameObject currentLevelGo;
    private Transform birdRoot;
    private void Awake()
    {
        Instance = this;
        pigDeadCount = 0;
        LoadSelectedLevel();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitBirdListByHierarchyOrder();
        pigCount = FindObjectsByType<Pig>(FindObjectsSortMode.None).Length;
        cameraFollowTarget = Camera.main.GetComponent<FollowTarget>();
        loadNextBird();
    }
    public void InitBirdListByHierarchyOrder()
    {
        if (birdRoot == null)
        {
            Debug.LogError("birdRoot 为空，无法按摆放顺序初始化小鸟队列。");
            birdlist = new Bird[0];
            return ;
        }
        List<Bird> birds=new List<Bird>();
        for(int i = 0; i < birdRoot.childCount; i++)
        {
            Transform child=birdRoot.GetChild(i);
            Bird bird=child.GetComponent<Bird>();
            if (bird != null)
            {
                birds.Add(bird);
            }
            else
            {
                Debug.LogWarning("BirdRoot 的子节点 " + child.name + " 上没有 Bird 脚本，已跳过。");
            }
        }
        birdlist=birds.ToArray();
         // 关键：所有小鸟初始化为备战状态，关闭碰撞体，避免干扰当前小鸟发射。
        for (int i = 0; i < birdlist.Length; i++)
        {
            birdlist[i].GoWait();
        }
    }

    private void LoadSelectedLevel()
    {
        int mapID = gameSo.selectedMapID;
        int levelID = gameSo.selectedLevelID;
        GameObject levelPrefab = Resources.Load<GameObject>("Map_" + mapID + "/" + "Level_" + levelID);
        if (levelPrefab == null)
        {
            Debug.LogError("关卡预制体加载失败：" + "Map_" + mapID + "/" + "Level_" + levelID);
            return;
        }
        currentLevelGo=GameObject.Instantiate(levelPrefab);
        birdRoot=currentLevelGo.transform.Find("BirdRoot");
        if (birdRoot == null)
        {
            Debug.LogError("当前关卡预制体下没有找到 BirdRoot，请在关卡预制体根节点下创建 BirdRoot，并把小鸟按顺序放进去。");
        }
    }
    public void loadNextBird()
    {
        index++;
        if (index >= birdlist.Length||birdlist==null)
        {
            GameOver();
            return;
        }
        else
        {

            birdlist[index].GoStage(SlingShoot.Instance.GetCenterPoint());
            cameraFollowTarget.SetTarget(birdlist[index].transform);
        }

    }
    public void OnPigDead()
    {
        pigDeadCount++;
        if (pigDeadCount >= pigCount)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        int starCount = 0;
        float pigDeadPrecent = 1.0f * pigDeadCount / pigCount;
        if (pigDeadPrecent > 0.33f && pigDeadPrecent < 0.66f)
        {
            starCount = 1;
        }
        else if (pigDeadPrecent > 0.66f && pigDeadPrecent < 0.99f)
        {
            starCount = 2;
        }
        else if (pigDeadPrecent > 0.99)
        {
            starCount = 3;
        }
        gameOverUI.Show(starCount);
        gameSo.UpdateLevel(starCount);

    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LeveList()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
