using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    float moveSpeed = 5f;

    bool moving;

    public void Setup()
    {
        gameObject.SetActive(true);
        moving = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * moveSpeed;
    }

    public void SetMoveFlg(bool flg)
    {
        moveSpeed = 0;
    }
}
