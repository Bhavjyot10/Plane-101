using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{    
    public void SceneLoad(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
