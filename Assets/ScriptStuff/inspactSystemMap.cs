using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inspactSystemMap : MonoBehaviour
{
    static public bool bigMapTracking = false;


    public float dragSpeed = 2.2f;
    public Vector3 mousePrePos = Vector3.zero;
    public float zoomSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        bigMapTracking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bigMapTracking)
        {

            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.LeftAlt))
            {
                mousePrePos = Input.mousePosition;
                return;

            }

            if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.LeftAlt))
            {
                Vector3 currentMousePos = Input.mousePosition;
                Vector3 difference = mousePrePos - currentMousePos;

                Vector3 move = new Vector3(difference.x * dragSpeed * Time.deltaTime, difference.y * dragSpeed * Time.deltaTime, 0);

                Camera.main.transform.Translate(move, Space.World);

                mousePrePos = currentMousePos;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0)
            {
                Camera.main.orthographicSize -= scroll * zoomSpeed;

                dragSpeed = (Camera.main.orthographicSize/5)*2.2f;

                //限制Camera大小
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2f, 10f);
            }

            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                Camera.main.orthographicSize = 5f;
                dragSpeed = 2.2f;
            }
        }
    }
}
