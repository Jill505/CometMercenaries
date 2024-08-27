using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditSystem : MonoBehaviour
{
    public GameCore gameCore;
    public CampSystem campSystem;
    public Mercenaries mercenaries;

    public Text MercenariesNameShow;

    // Start is called before the first frame update
    void Start()
    {
        campSystem = GameObject.Find("GameCore").GetComponent<CampSystem>();
        gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int MaxMentMercenariesNumbers;

    public void AddNewMercenaries()
    {
        Mercenaries newMercenaries = new Mercenaries();
        campSystem.MercenariesList.Add(newMercenaries);

        //UpdateMercenariesListAmount
    }

    public void LoadMercenaries(int MercenariesSort)
    {
        
    }
}
