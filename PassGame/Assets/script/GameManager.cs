using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace passGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject missWindow = default;

        [SerializeField] ballManager ball = default;

        [SerializeField] Playe_Test firstTouchPlayer = default;

        // Start is called before the first frame update
        void Start()
        {
            ball.Setup(ballManager.state.dribble,firstTouchPlayer.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            var currentState = ball.GetBallState();
            missWindow.SetActive(currentState == ballManager.state.miss);
        }
    }
}
