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

    //�@�ǼƾڮھڪZ�� �{�b���Ȯɼg�өU�������洫��

    public void useLittleBrain()
    {
        //���X�U�@�B�M��
        StartCoroutine(mobMove());
    }
    IEnumerator mobMove()
    {
        yield return null;
        //���浲���� �^call�Ϩ�{�~��
        GameObject.Find("exp_VBF_Core").GetComponent<exp_VBF_core>().mobClug = false;
    }

    public void die()
    {

    }
}
