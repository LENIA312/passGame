using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    private float speed = 5f;

    private float Defspeed = 5f;

    bool moving = true;



    void Update()
    {
            //transform.position -= new Vector3(0, Time.deltaTime * speed);
            //transform.position -= new Vector3(0, Time.deltaTime * speed);

            if (transform.localPosition.y <= -16.5)
            {
                transform.localPosition = new Vector3(0, 33);
            }

            GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;

    }
    public void SetMoveFlg(bool flg)
    {
        speed = flg == false ? 0 : Defspeed;
    }
}
