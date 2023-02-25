using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class Menu : MonoBehaviour
{

    //https://www.youtube.com/watch?v=kWxPZLNVxSU
    public static void OpenURL(string url)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
OpenTab(url);
#endif
    }

    [DllImport("__Internal")]
    private static extern void OpenTab(string url);

    public void StartGame(int mode)
    {
        GameParams p = GameParams.GetInstance();
        if (mode == 1)
        {
            p.SetExperiment(GameParams.EXPERIMENT.EXP1);            
        } else if (mode == 2)
        {
            p.SetExperiment(GameParams.EXPERIMENT.EXP2);
        } else //mode 3
        {
            p.SetExperiment(GameParams.EXPERIMENT.EXP3);
        }

        GameParams.GetInstance().PlayChangeSceneSound();
        SceneManager.LoadScene("Game");
    }
}
