using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManerger : MonoBehaviour
{
    public static AudioManerger Instance {get;private set;}
    public AudioClip birdCollison;
    public AudioClip birdSelect;
    public AudioClip birdFlying;
    public AudioClip[] pigCollisions;
    public AudioClip woodCollision;
    public AudioClip woodDestory;
    private void Awake() {
        Instance=this;  
    }
    public void PlayBirdCollision(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(birdCollison,position,1f);
    }
    public void PlayBirdSelect(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(birdSelect,position,1f);
    }
    public void PlayBirdFlying(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(birdFlying,position,1f);
    }
    public void PlayPigCollision(Vector3 position)
    {
        int index=Random.Range(0,pigCollisions.Length);
        AudioSource.PlayClipAtPoint(pigCollisions[index],position,1f);
    }
    public void PlayWoodCollision(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(woodCollision,position,.6f);
    }
    public void PlayWoodDestroy(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(woodDestory,position,.6f);
    }
}
