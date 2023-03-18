using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class ballManager : MonoBehaviour
{
    [SerializeField] float ballSpeed;

    state ballState;
    
    // �{�[���m�X�e�[�^�X
    public enum state
    {
        dribble,
        pass,
    }

    // �{�[���̏�����
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

    // �{�[�����ˁA�^�[�Q�b�g�̃|�W�V���������Ɉړ�
    void BallDeparture(Vector3 taeget)
    {
        // �����̐����iZ�����̏����Ɛ��K���j
        Vector3 shotForward = Vector3.Scale((taeget - transform.position), new Vector3(1, 1, 0)).normalized;

        // �e�ɑ��x��^����
        GetComponent<Rigidbody2D>().velocity = shotForward * ballSpeed;
    }

    // �}�E�X�̃N���b�N���W�擾
    Vector3 GetVect()
    {
        Vector3 mousePosition = Input.mousePosition;
        // �J�[�\���ʒuz���W��10��
        mousePosition.z = 10;
        // �J�[�\���ʒu�����[���h���W�ɕϊ�
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);

        return target;
    }

    //�@�{�[���̃X�e�[�^�X�ύX
    public void SetBallState(state state)
    {
        this.ballState = state;
    }

    // �p�X�o���\��
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
