using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class ballManager : MonoBehaviour
{
    [SerializeField] float ballSpeed;

    state ballState;
    
    // ボールノステータス
    public enum state
    {
        dribble,
        pass,
    }

    // ボールの初期化
    public void Setup(state state)
    {
        ballState = state;
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

        // debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ballState = ballState == state.dribble ? state.pass : state.dribble;
            Debug.Log($"currentState : {ballState}");
        }
    }

    // ボール発射、ターゲットのポジション方向に移動
    void BallDeparture(Vector3 taeget)
    {
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
            case state.dribble:
                value = true;
                break;

            case state.pass:
                value = false;
                break;
            default:
                break;
        }

        Debug.Log($"currntValue : {value}");
        return value;
    }
}
