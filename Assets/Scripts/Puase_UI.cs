using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puase_UI : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPauseButtonClick()
    {
        Time.timeScale = 0;
        animator.SetBool("IsShow", true);
    }
    public void OnContinueButtonClick()
    {
        Time.timeScale = 1;
        animator.SetBool("IsShow", false);
    }
    public void OnRestartButtonClick()
    {
        //TODO
        Time.timeScale = 1;
        GameManerger.Instance.ReStart();
    }
    public void OnLevelListButtonClick()
    {
        Time.timeScale = 1;
        GameManerger.Instance.LeveList();
    }
}
