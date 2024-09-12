using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToStartScene()
    {
        SceneManager.LoadScene(0);
    }
    

}
