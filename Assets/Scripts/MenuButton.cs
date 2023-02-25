using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class MenuButton : MonoBehaviour
{
    AudioSource asource;

    private void Awake()
    {
        asource = GetComponent<AudioSource>();
    }


    public void Knock()
    {
        asource.Play(0);
    }
}
