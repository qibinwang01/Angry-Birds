using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Bird : Bird
{
    public float boomRadius=2f;
    private bool hasExploded=false;
    public override void FlyingSkill()
    {
        Exploded();
    }

    public override void FullTimeSkill()
    {
        Exploded();
    }
    public void Exploded()
    {
        if (hasExploded)
        {
            return;
        }

        hasExploded = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, boomRadius);

        foreach (Collider2D collider in colliders)
        {
            Destructable des = collider.GetComponent<Destructable>();

            if (des != null)
            {
                // 目前直接秒杀范围内可破坏物体，后续可以按距离计算伤害衰减。
                des.TakeDamge(Int32.MaxValue);
            }
        }

        state = BirdState.WaittingForDead;
        LoadNextBird();
    }
}
