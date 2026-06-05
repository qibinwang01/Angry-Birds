using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    public override void FlyingSkill()
    {
        //base.FlyingSkill();
        rigidbody2d.velocity = rigidbody2d.velocity * 2;
    }
}
