using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    float moveSpeed;

    public void Setup(float moveSpeed)
    {
        this.gameObject.SetActive(true);
        this.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;

        gameObject.transform.position = new Vector2(pos.x,pos.y -= moveSpeed);  // ç¿ïWÇê›íË
    }
}
