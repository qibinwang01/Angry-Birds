using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int maxHP = 100;
    public int currHP;
    public List<Sprite> injureSpriteList;
    private SpriteRenderer spriteRenderer;
    private GameObject boomPrefab;
    private bool isDead = false;
    private void Awake()
    {
        boomPrefab = Resources.Load<GameObject>("Boom1");
    }
    private void Start()
    {

        currHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("碰撞速度为："+other.relativeVelocity.magnitude);
        float value = other.relativeVelocity.magnitude * 10;
        TakeDamge((int)value);
    }
    public void TakeDamge(int damage)
    {
        if (isDead)
        {
            return;
        }

        //Debug.Log("碰撞速度为："+other.relativeVelocity.magnitude);
        currHP -= damage;
        if (currHP <= 0)
        {
            isDead=true;
            Death(); return;
        }
        //获取索引
        int index = (int)((maxHP - currHP) / (maxHP / (injureSpriteList.Count + 1.0f)) - 1);
        Sprite beforeSprite = spriteRenderer.sprite;
        if (index >= 0 && index < injureSpriteList.Count)
        {
            spriteRenderer.sprite = injureSpriteList[index];
        }
        if (spriteRenderer.sprite != beforeSprite)
        {
            PlayAudioCollision();
        }
    }
    public virtual void Death()
    {
        PlayAudioDestroyed();
        GameObject.Instantiate(boomPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public virtual void PlayAudioCollision()
    {
        AudioManerger.Instance.PlayWoodCollision(transform.position);
    }
    public virtual void PlayAudioDestroyed()
    {
        AudioManerger.Instance.PlayWoodDestroy(transform.position);
    }
}
