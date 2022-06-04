using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Vector3 point;
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(Camera.main.transform.position, (mousePos - Camera.main.transform.position) * 100,Color.blue);

        if (Physics.Raycast(Camera.main.transform.position,(mousePos - Camera.main.transform.position)*100, out hit))
        {
            hit.point = point;
        }
        

    }
}
