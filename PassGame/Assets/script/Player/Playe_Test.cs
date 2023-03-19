using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace passGame
{
    public class Playe_Test : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float moveLength;

        [SerializeField] public bool playerCheck;

        public Transform dribblePos;

        bool moving;

        private Vector2 initialPosition;

        void Start()
        {
            initialPosition = transform.position;
            moving = true;
        }

        void Update()
        {
            if (moving)
            {
                transform.position = new Vector2(initialPosition.x, Mathf.Sin(Time.time * speed) * moveLength + initialPosition.y);
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }

        public void SetMoveFlg(bool flg)
        {
            moving = flg;
        }
    }
}
