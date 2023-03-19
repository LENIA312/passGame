using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    private float speed = 1;

    void Update()
    {
        transform.position -= new Vector3(0, Time.deltaTime * speed);

        if (transform.localPosition.y <= -16.5)
        {
            transform.localPosition = new Vector3(0, 33);
        }
    }
}
