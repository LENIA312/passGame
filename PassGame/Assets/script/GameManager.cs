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

        [SerializeField] float canDribbleTime;

        [SerializeField] GoalManager goalObject = default;

        [SerializeField] slidingManager slidingEnemy = default;

        [SerializeField] Playe_Test[] players = default;

        [SerializeField] scroll[] bg = default;

        [SerializeField] GameObject GoalWindow = default;

        [SerializeField] AudioSource audioSource = default;

        [SerializeField] AudioClip goalWhistleSE = default;

        private const float START = 0.0f;
        private const float INTERVAL = 2.0f;

        float time;
        float dribbleTime;

        gameStatus gameState;
        drribleStatus drribleState;

        bool genelateGoal = false;

        GoalManager goalObj = null;

        enum drribleStatus
        {
            can,
            over,
            brock,
        }

        enum gameStatus
        {
            wait,
            game,
            end,
            goal,
            goalChance,
        }

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 120; //60FPSÇ…ê›íË

            ball.Setup(ballManager.state.dribble, firstTouchPlayer.gameObject);

            InvokeRepeating("UpdateMakePrefab", START, INTERVAL);
            time = 0;
            dribbleTime = 0;

            gameState = gameStatus.game;
            drribleState = drribleStatus.can;

        }

        // Update is called once per frame
        void Update()
        {
            if (gameState == gameStatus.goalChance)
            {
                // ÉSÅ[Éãí‚é~
                if (goalObj != null)
                {
                    if (goalObj.transform.position.y < 4f)
                    {
                        Debug.Log("STPPED");

                        goalObj.SetMoveFlg(false);

                        foreach (var player in players)
                        {
                            player.SetMoveFlg(false);
                        }

                        foreach (var bgs in bg)
                        {
                            bgs.SetMoveFlg(false);
                        }
                    }
                }

                // ÉSÅ[ÉãîªíË
                if (ball.ballState == ballManager.state.goal)
                {
                    gameState = gameStatus.goal;
                }
            }

                if (gameState == gameStatus.game)
            {
                GameEnd();
                var currentState = ball.ballState;
                missWindow.SetActive(currentState == ballManager.state.miss);

                time += Time.deltaTime;
                if (time > 1f)
                {
                    // ÉSÅ[ÉãÇê∂ê¨
                    if (goalObj == null)
                    {
                        genelateGoal = true;
                    }
                }

                if (genelateGoal)
                {
                    genelateGoal = false;
                    var goal = Instantiate(goalObject, new Vector3(0, 6.2f, 0), Quaternion.identity);
                    goal.Setup();

                    goalObj = goal;
                    time = 0;
                    gameState = gameStatus.goalChance;
                    CancelInvoke("UpdateMakePrefab");
                }

                // Dribbleéûä‘êßå¿
                var state = ball.GetBallState();
                if (state == ballManager.state.dribble)
                {
                    dribbleTime += Time.deltaTime;
                }
                else
                {
                    dribbleTime = 0;
                }

                if (dribbleTime > canDribbleTime)
                {
                    missWindow.SetActive(true);
                    gameState = gameStatus.end;
                    drribleState = drribleStatus.over;
                }

                if(drribleState == drribleStatus.over)
                {
                    GenelateSliding();
                }
            }

            // ÉSÅ[Éãèàóù
            if(gameState == gameStatus.goal)
            {
                GoalWindow.gameObject.SetActive(true);
                audioSource.PlayOneShot(goalWhistleSE);
                Debug.Log("GOAL");
            }
        }

        void GenelateSliding()
        {
            var slidingObj = Instantiate(slidingEnemy, new Vector3(0, 0, 0), Quaternion.identity);
            slidingObj.Setup(ball);

            drribleState = drribleStatus.brock;
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
                Application.Quit();//ÉQÅ[ÉÄÉvÉåÉCèIóπ
            }
        }
    }
}
