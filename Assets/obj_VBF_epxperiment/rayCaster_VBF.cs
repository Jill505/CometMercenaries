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
                    //�Ұʨ���
                    Debug.Log("�Ұ�" + hit.collider.gameObject.name);
                }
                if (hit.collider.gameObject.tag == "chunk")
                {
                    Debug.Log("������chunk" + hit.collider.gameObject.name);

                    if (hit.collider.gameObject.GetComponent<chunk_VBF>().chunkType == "entry")
                    {
                        //���a�I���F�`�I �N����U�i��ܤJ�f��
                        GameObject.Find("VBF_core").GetComponent<exp_VBF_core>().submittingEntry = hit.collider.gameObject.GetComponent<chunk_VBF>().entrySort;
                        //Ĳ�o�P�B���
                        Debug.Log("�������d�J�f�G�J�f" + hit.collider.gameObject.GetComponent<chunk_VBF>().entrySort);
                    }
                }

                // hit.collider.GetComponent<SomeComponent>().SomeMethod();
            }
        }
    }
}
