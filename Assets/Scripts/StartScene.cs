using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        //SceneManager.GetActiveScene().buildIndex获取当前场景序号
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
}
