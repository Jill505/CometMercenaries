using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotTester : MonoBehaviour
{
    public GameCore gCore;
    // Start is called before the first frame update
    void Start()
    {
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
    }

    // Update is called once per frame
    void Update()
    {
        //warningFunc();
    }

    public void warningFunc()
    {
        Debug.LogWarning("One Shot Tester Functioned");
    }

    public void cleanSaveData()
    {

    }
    public void cleanMercenariesList()
    {
        Debug.Log("Clean Mercenaries List Already");
        GameCore.Camp.MercenariesList.Clear();
        GameCore.Save();
    }

    public void cleanAllNodes()
    {

    }

    public void GiveExp4()
    {
        GameObject.Find("GameCore").GetComponent<EditSystem>().mercenariesLoading.GainExp(4);
    }

    public void healthAllRecover()
    {
        GameObject.Find("GameCore").GetComponent<EditSystem>().mercenariesLoading.health = GameObject.Find("GameCore").GetComponent<EditSystem>().mercenariesLoading.maxHealth;
    }
}
