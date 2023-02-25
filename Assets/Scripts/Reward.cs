using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    TMPro.TextMeshProUGUI Text;

    string Value;
    public List<Color> Colors;

    Color c;

    public void Play(int value, Puerta.DOOR_TYPE type)
    {
        Value = value.ToString();
        c = Colors[(int)type];
    }

    private void Awake()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Start()
    {
        Text.text = Value.ToString();
        Text.color = c;
    }

    public void Remove()
    {
        Debug.Log("Destroy text");
        GameObject.Destroy(gameObject);
    }
}
