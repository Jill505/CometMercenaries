using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_VBF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float hp = 10f;
    public float Strength = 10f;
    public float Ether = 10f;
    public float Mentle = 10f;
    public float Agility = 10f;

    public float Speed = 10f;
    public float SpeedHit = 100;
    public float GameSpeed = 0;

    public bool isDead = false;
    public void calAllInfo()
    {

    }

    //一些數據根據武器 現在先暫時寫個垃圾機制當交換用

    public void useLittleBrain()
    {
        //做出下一步決策
        StartCoroutine(mobMove());
    }
    IEnumerator mobMove()
    {
        yield return null;
        //執行結束後 回call使協程繼續
        GameObject.Find("exp_VBF_Core").GetComponent<exp_VBF_core>().mobClug = false;
    }

    public void die()
    {

    }
}
