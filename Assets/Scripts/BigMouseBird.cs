using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMouseBird : Bird
{
    public override void FlyingSkill()
    {
        //base.FlyingSkill();
        Vector2 velocity=rigidbody2d.velocity;
        //释放技能后改变鸟的飞行方向
        velocity.x=-velocity.x;
        rigidbody2d.velocity=velocity;
        //调转鸟的朝向使得释放技能的时候看起来不那么奇怪
        Vector3 scale=transform.localScale;
        scale.x=-scale.x;
        transform.localScale=scale;
    }
}
