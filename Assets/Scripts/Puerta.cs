using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public AudioSource asource;

    public enum DOOR_TYPE { Red = 0, Blue = 1, Green = 2}

    public AudioClip AudioCoin;
    public AudioClip AudioOpen;
    public AudioClip AudioRegen;

    public DOOR_TYPE Type;

    public Sprite DoorClosed;
    public Sprite DoorOpened;

    public GameObject CoinPrefab;
    private GameObject CoinInstance;
    public int CoinScale = 3;

    [SerializeField]
    private int NotUsedClicksAgo = 0;

    [SerializeField]
    int RewardMax;
    [SerializeField]
    int RewardMin;

    [SerializeField]
    bool IsOpen;

    SpriteRenderer SR;

    public DoorsController DoorsController;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        asource = GetComponent<AudioSource>();
    }

    public void IncrementNotUsed()
    {
        NotUsedClicksAgo++;

        if (GameParams.GetInstance().MustReduceDoors)
        {
            if (NotUsedClicksAgo >= GameParams.CLICKS_TO_DISAPPEAR_DOOR)
            {
                Hide(GameParams.GetInstance().CanReviveDoors);
            } else
            {
                Reduce();
            }
        }
    }

    public void SetRewards(int min, int max)
    {
        RewardMin = min;
        RewardMax = max;
    }

    void OnMouseDown()
    {
        ClickDoor();
    }

    private void ClickDoor()
    {
        NotUsedClicksAgo = 0;

        if (IsOpen)
        {
            Debug.Log("Coin Clicked");
            asource.PlayOneShot(AudioCoin);
            DoorsController.OnDoorClick(this, Random.Range(RewardMin, RewardMax));
        }
        else
        {
            asource.PlayOneShot(AudioOpen);
            Restore(false, false);
            Open();
            DoorsController.OnDoorClick(this, 0);
        }
    }

    public void Open()
    {
        Debug.Log("Door open");
        IsOpen = true;
        SR.sprite = DoorOpened;

        CoinInstance = Instantiate(CoinPrefab, transform.position, Quaternion.identity);
        CoinInstance.transform.localScale = new Vector3(CoinScale, CoinScale, 0);
    }

    public void Close()
    {
        DestroyCoin();

        Debug.Log("Door closed");
        IsOpen = false;
        SR.sprite = DoorClosed;
    }

    private void DestroyCoin()
    {
        if (CoinInstance)
        {
            GameObject.Destroy(CoinInstance);
            CoinInstance = null;
        }
    }

    public void Restore(bool countClick, bool sound)
    {
        NotUsedClicksAgo = 0;
        gameObject.SetActive(true);
        transform.localScale = Vector3.one;
        if (sound) asource.PlayOneShot(AudioRegen);
        //Contabilizamos el click
        if (countClick) DoorsController.OnDoorClick(this, 0);
    }

    public void Reduce()
    {
        float s = (float)(GameParams.CLICKS_TO_DISAPPEAR_DOOR - NotUsedClicksAgo) / (float)GameParams.CLICKS_TO_DISAPPEAR_DOOR;
        transform.localScale = new Vector3(s, s, 0);
    }

    public void Hide(bool showRestore)
    {
        if (showRestore) DoorsController.ShowRestore(this);
        gameObject.SetActive(false);
    }

    public void Destruir()
    {
        DestroyCoin();
        GameObject.Destroy(gameObject);
    }
}
