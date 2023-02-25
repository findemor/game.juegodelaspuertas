using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public List<GameObject> Floors;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Floors.Count; i++)
        {
            Floors[i].SetActive(i == (int)GameParams.GetInstance().ExperimentMode);
        }
    }

}
