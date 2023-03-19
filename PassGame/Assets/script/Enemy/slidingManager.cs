using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using passGame;

public class slidingManager : MonoBehaviour
{
    [SerializeField] public Transform ballPos;

    float slidingSpeed = 10f;
    bool havingFlg;

    public void Setup(ballManager ball)
    {
        gameObject.SetActive(true);

        var ballPos = ball.transform.position;
        var newPos = transform.position;

        this.havingFlg = ball.GetHavingPlayer();

        newPos.y = ballPos.y;
        newPos.x = GetXPos();

        ScaleReverse();
        transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (havingFlg)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * slidingSpeed;
            Debug.Log($"call_L");
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * slidingSpeed;
            Debug.Log($"call_R");
        }
    }

    float GetXPos()
    {
        float value = 0f;
        
        if (havingFlg)
        {
            value = 5.55f;
        }
        else
        {
            value = -5.55f;
        }

        return value;
    }

    void ScaleReverse()
    {
        if (!havingFlg)
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
