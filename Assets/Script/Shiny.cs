using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiny : MonoBehaviour
{
    public Material material;
    public float r = 0, g = 0, b = 0;

    int index = 0;

    public bool reverse = false;

    Vector3 point;
    private void Start()
    {
        point = GameObject.Find("Mouse").GetComponent<Mouse>().point;
    }

    private void Update()
    {
        float h, s, v;
        Color.RGBToHSV(material.color, out h, out s, out v);

        h = h >= 1 ? 0 : h + Time.deltaTime * .01f;

        Color col = Color.HSVToRGB(h, 1, 1);

        material.color = col;

        this.transform.Translate((point - this.transform.position)*0.01f, Space.World);

    }

    public void SwitchCase(float color)
    {
        if (color >= 1)
        {
            if (!reverse)
            {
                index++;
            }
        }
        if (color <= 0)
        {
            if (reverse)
            {
                color = 0;
                index--;
            }
        }
    }

    private void OnDestroy()
    {
        material.color = Color.white;
    }
}
