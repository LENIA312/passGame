using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace passGame {
    public class ballManager : MonoBehaviour
    {
        [SerializeField] float ballSpeed;

        state ballState;
        GameObject currentHitObject;

        // �{�[���̃X�e�[�^�X
        public enum state
        {
            none,
            dribble,
            pass,
            miss,
        }

        // �{�[���̏�����
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

        // �{�[�����ˁA�^�[�Q�b�g�̃|�W�V���������Ɉړ�
        void BallDeparture(Vector3 taeget)
        {
            ballState = state.pass;

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
