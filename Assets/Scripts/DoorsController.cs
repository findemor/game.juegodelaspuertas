using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{

    AudioSource asource;


    public List<Puerta> Puertas;
    public List<Transform> RewardPositions;
    [SerializeField]
    int ClicksLeft;
    [SerializeField]
    int OpensCount = 0;

    Puerta CurrentDoor = null;

    [SerializeField]
    private int CurrentReward = 0;

    public UIController UI;

    public GameObject TextDoorReward;

    private void Awake()
    {
        asource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CurrentReward = 0;
        ClicksLeft = GameParams.CLICKS_AVAILABLE;
        OpensCount = 0;

        Puertas.Shuffle<Puerta>();

        int pointsIncrement = 1;
        int points = 0;
        int min = 1;
        int max = 10;
        foreach(Puerta p in Puertas)
        {
            p.SetRewards(min, max + points);
            points += pointsIncrement;
        }

        UI.SetValues(CurrentReward, ClicksLeft, OpensCount);
    }


    public void OnDoorClick(Puerta door, int value)
    {
        if (ClicksLeft > 0)
        {
            ClicksLeft--;

            if (CurrentDoor != null)
            {
                if (CurrentDoor.Type != door.Type)
                {
                    CurrentDoor.Close();
                }
            }

            CurrentDoor = door;

            //las puertas otras incrementan el numero de veces que no se usaron
            foreach(Puerta p in Puertas)
            {
                if (p.Type != door.Type)
                {
                    p.IncrementNotUsed();
                }
            }

            //valor
            if (value <= 0)
            {
                //si el valor era mayor que cero es que la puerta estaba abierta
                //si no estaba abierta, contabilizamos el click
                OpensCount++;
            }

            AddReward(door.Type, value);

            //Update other ui elements
            UI.SetValues(CurrentReward, ClicksLeft, OpensCount);

            //Si se ha acabado
            if (ClicksLeft <= 0)
            {
                asource.Play(0);
                //eliminamos puertas
                foreach(Puerta p in Puertas)
                {
                    p.Destruir();
                }
            }
        }
    }


    public void ShowRestore(Puerta puerta)
    {
        UI.ShowRestore(puerta);
    }

    public void AddReward(Puerta.DOOR_TYPE type, int value)
    {
        if (value > 0)
        {
            CurrentReward += value;
            //TextCurrentReward.text = CurrentReward.ToString();

            //instancia la recompensa de la puerta
            int p = (int)type;
            GameObject g = Instantiate(TextDoorReward, RewardPositions[p].position, Quaternion.identity);
            g.transform.SetParent(RewardPositions[p]);
            g.GetComponent<Reward>().Play(value, type);
        }
    }


}

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}