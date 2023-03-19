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

        [SerializeField] EnemyManager enemyObject = default;

        [SerializeField] float moveSpeedEnemy;

        private const float START = 0.0f;
        private const float INTERVAL = 2.0f;

        float time;

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 120; //60FPSに設定
            GameEnd();

            ball.Setup(ballManager.state.dribble, firstTouchPlayer.gameObject);

            InvokeRepeating("UpdateMakePrefab", START, INTERVAL);
            time = 0;
        }

        // Update is called once per frame
        void Update()
        {
            var currentState = ball.GetBallState();
            missWindow.SetActive(currentState == ballManager.state.miss);

            time += Time.deltaTime;
            if (time > 60f)
            {
                // ゴールを生成
            }
        }

        private void UpdateMakePrefab()
        {

            //Instantiate(enemyObjecy, new Vector3(Random.Range(-5.0f, 5.0f), 0,0), Quaternion.identity);
            var enemy = Instantiate(enemyObject, new Vector3(0, 5.74f, 0), Quaternion.identity);
            enemy.Setup(moveSpeedEnemy);
        }

        void GameEnd()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();//ゲームプレイ終了
            }
        }
    }
}
