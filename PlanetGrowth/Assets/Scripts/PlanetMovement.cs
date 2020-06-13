using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    public LayerMask mouseDragLayer;
    public LayerMask cardLayer;
    public float rotationSpeed = 1.0f;
    public float horizontalSpeed = 0.1f;
    public float minY = 1.5f;
    public float maxY = 12.5f;

    private Vector3 originalPosition;
    private Vector3 lastPosition;

    private bool dragging = false;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("w") | Input.GetKey("s") | Input.GetKey("a") | Input.GetKey("d"))
        {
            dragging = false;
            if (Input.GetKey("w")) {
                transform.rotation *= Quaternion.Euler(10 * rotationSpeed * Time.deltaTime , 0, 0);
            }
            else if (Input.GetKey("s")){
                transform.rotation *= Quaternion.Euler(-10 * rotationSpeed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey("a")) {
                transform.position += new Vector3(50 * horizontalSpeed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey("d")) {
                transform.position += new Vector3(-50 * horizontalSpeed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 1000.0f, mouseDragLayer | cardLayer) && hit.transform.gameObject.CompareTag("MouseControl"))
                {
                    // start dragging
                    lastPosition = hit.point;
                    dragging = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
                dragging = false;
            if (dragging && Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, 1000.0f, mouseDragLayer) && hit.transform.gameObject.CompareTag("MouseControl"))
                {
                    var delta = hit.point - lastPosition;

                    // Upwards and downward movement of Cursor while dragging if no card is selected
                    transform.rotation *= Quaternion.Euler(delta.y * rotationSpeed, 0, 0);

                    //transform.position += new Vector3(0, delta.y, 0);
                    // Horizontal movement of Cursor while dragging if no card is selected
                    //transform.rotation *= Quaternion.Euler(0, -delta.x * rotationSpeed, 0);
                    transform.position += new Vector3(delta.x, 0, 0);
                    lastPosition = hit.point;
                }
            }

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
        }
    }
}
