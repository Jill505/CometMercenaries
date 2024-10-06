using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCaster_VBF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {

                if (hit.collider.gameObject.tag == "")
                {
                    //啟動其選單
                    Debug.Log("啟動" + hit.collider.gameObject.name);
                }
                if (hit.collider.gameObject.tag == "chunk")
                {
                    Debug.Log("偵測到chunk" + hit.collider.gameObject.name);

                    if (hit.collider.gameObject.GetComponent<chunk_VBF>().chunkType == "entry")
                    {
                        //玩家點擊了節點 將其註冊進選擇入口中
                        GameObject.Find("VBF_core").GetComponent<exp_VBF_core>().submittingEntry = hit.collider.gameObject.GetComponent<chunk_VBF>().entrySort;
                        //觸發同步顯示
                        Debug.Log("改變關卡入口：入口" + hit.collider.gameObject.GetComponent<chunk_VBF>().entrySort);
                    }
                }

                // hit.collider.GetComponent<SomeComponent>().SomeMethod();
            }
        }
    }
}
