using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    private void Awake() {
        animator=GetComponent<Animator>();
    }
    public void Show()
    {
        animator.SetTrigger("IsShow");
    }
}
