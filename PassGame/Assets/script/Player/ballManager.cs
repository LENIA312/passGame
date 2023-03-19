using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace passGame {
    public class ballManager : MonoBehaviour
    {
        [SerializeField] float ballSpeed;

        state ballState;
        GameObject currentHitObject;

        // ボールのステータス
        public enum state
        {
            none,
            dribble,
            pass,
            miss,
        }

        // ボールの初期化
        public void Setup(state state)
        {
            ballState = state;
            currentHitObject = null;
        }

        private void Start()
        {
            Setup(state.none);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                if (CheckCanPass())
                {
                    BallDeparture(GetVect());
                }
            }
            else
            {
                BallPositionUpdate();
            }

            // debug
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ballState = ballState == state.dribble ? state.pass : state.dribble;
                Debug.Log($"currentState : {ballState}");
            }
        }

        void BallPositionUpdate()
        {
            if (currentHitObject != null && ballState == state.dribble)
            {
                transform.position = currentHitObject.GetComponent<Playe_Test>().dribblePos.position;
            }
        }

        // ボール発射、ターゲットのポジション方向に移動
        void BallDeparture(Vector3 taeget)
        {
            ballState = state.pass;

            // 向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale((taeget - transform.position), new Vector3(1, 1, 0)).normalized;

            // 弾に速度を与える
            GetComponent<Rigidbody2D>().velocity = shotForward * ballSpeed;
        }

        // マウスのクリック座標取得
        Vector3 GetVect()
        {
            Vector3 mousePosition = Input.mousePosition;
            // カーソル位置z座標を10に
            mousePosition.z = 10;
            // カーソル位置をワールド座標に変換
            Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);

            return target;
        }

        //　ボールのステータス変更
        public void SetBallState(state state)
        {
            this.ballState = state;
        }

        // パス出し可能か
        bool CheckCanPass()
        {
            bool value = false;

            switch (this.ballState)
            {
                case state.none:
                case state.dribble:
                    value = true;
                    break;

                case state.pass:
                case state.miss:
                    value = false;
                    break;
                default:
                    break;
            }

            Debug.Log($"currentValue : {value}");
            return value;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.name == outareaDefine.outAreaR ||
                collision.name == outareaDefine.outAreaL)
            {
                ballState = state.miss;
                Debug.Log($"miss");
            }

            collision.TryGetComponent<Playe_Test>(out Playe_Test comp);
            if (comp != null)
            {
                if (currentHitObject != collision.gameObject)
                {
                    // player hit.
                    var pos = collision.GetComponent<Playe_Test>().dribblePos;
                    ballState = state.dribble;
                    //transform.position = pos.position;
                    currentHitObject = collision.gameObject;
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    Debug.Log($"dribble");
                    Debug.Log($"hitPlayer");
                }
            }
        }

        Vector3 GethitPoint()
        {
            Vector3 target = Vector3.zero;

            return target;
        }

        public state GetBallState()
        {
            return ballState;
        }
    }
}
