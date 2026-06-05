using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Destructable
{
    public int score=3000;
    public override void Death()
    {
        base.Death();
        GameManerger.Instance.OnPigDead();
        ScoreManager.Instance.ShowScore(transform.position,score);
    }
    public override void PlayAudioCollision()
    {
        //base.PlayAudioCollision();
        AudioManerger.Instance.PlayPigCollision(transform.position);
    }
    public override void PlayAudioDestroyed()
    {
        //base.PlayAudioDestroyed();
    }
}
