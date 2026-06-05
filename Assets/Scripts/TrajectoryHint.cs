using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TrajectoryHint : MonoBehaviour
{
    [Header("轨迹点预制体")]
    public GameObject dotPrefab;

    [Header("轨迹点数量，只显示前面一小段")]
    public int dotCount = 10;

    [Header("每个轨迹点之间的时间间隔")]
    public float timeStep = 0.08f;

    [Header("轨迹点缩放")]
    public float dotScale = 0.18f;

    private readonly List<GameObject> dotList = new List<GameObject>();
    private void Awake() {
        CreateDots();
        Hide();
    }
    private void CreateDots()
    {
        if (dotPrefab == null)
        {
            Debug.LogError("TrajectoryHint 没有设置 dotPrefab");
            return;
        }
        for(int i = 0; i < dotCount; i++)
        {
            GameObject dot=Instantiate(dotPrefab,transform);
            dot.transform.localScale=Vector3.one*dotScale;
            dot.SetActive(false);
            SpriteRenderer spriteRenderer=dot.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color=spriteRenderer.color;
                color.a=1f - i * 1.0f / dotCount;
                spriteRenderer.color = color;
            }
            dotList.Add(dot);
        }
    }
    public void Hide()
    {
        for(int i = 0; i < dotList.Count; i++)
        {
            dotList[i].SetActive(false);
        }
    }
    public void Show()
    {
        for(int i = 0; i < dotList.Count; i++)
        {
            dotList[i].SetActive(true);
        }
    }
    public void UpdateTrajectory(Transform birdTransform, Vector3 centerPoint)
    {
        if (birdTransform == null)
        {
            Hide();
            return;
        }

        Bird bird = birdTransform.GetComponent<Bird>();
        Rigidbody2D rb = birdTransform.GetComponent<Rigidbody2D>();

        if (bird == null || rb == null)
        {
            Hide();
            return;
        }

        Vector2 startPosition = birdTransform.position;

        Vector2 startVelocity =
            ((Vector2)centerPoint - startPosition).normalized * bird.flySpeed;

        Vector2 gravity = Physics2D.gravity * rb.gravityScale;

        for (int i = 0; i < dotList.Count; i++)
        {
            float t = (i + 1) * timeStep;

            Vector2 position =
                startPosition +
                startVelocity * t +
                0.5f * gravity * t * t;

            dotList[i].transform.position = position;
        }
    }

}
