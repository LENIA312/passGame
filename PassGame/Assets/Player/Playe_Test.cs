using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playe_Test : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float moveLength;

    public Transform dribblePos;

    private Vector2 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.position = new Vector2(initialPosition.x, Mathf.Sin(Time.time * speed) * moveLength + initialPosition.y);
    }
}
