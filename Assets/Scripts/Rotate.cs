using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float y;

    private void Update()
    {
        y += Time.deltaTime * 360;
        transform.eulerAngles = new Vector3(-45,y, 45);
    }
}
