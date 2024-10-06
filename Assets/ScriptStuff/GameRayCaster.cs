using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRayCaster : MonoBehaviour
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
                Debug.Log("Clicked on object: " + hit.collider.name);

                if (hit.collider.gameObject.tag == "")
                {
                    //啟動其選單
                    Debug.Log("啟動" + hit.collider.gameObject.name);
                }
                // hit.collider.GetComponent<SomeComponent>().SomeMethod();
            }
        }
    }
}
