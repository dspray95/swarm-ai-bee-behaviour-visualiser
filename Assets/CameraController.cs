using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float zoomSensitivity = 10f;
    public float dragSensitivity = 10f;
    float size;
    Vector3 mouseDown;

	// Use this for initialization
	void Start () {
        size = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {

        size -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        Camera.main.orthographicSize = size;

        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = Input.mousePosition;
            Cursor.visible = false;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseDown);
            Vector3 move = new Vector3(pos.x * -dragSensitivity, pos.y * -dragSensitivity, 0);
            transform.Translate(move, Space.World);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.visible = true;
        }
        
    }
}
