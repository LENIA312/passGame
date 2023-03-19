using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace passGame {
    public class ballManager : MonoBehaviour
    {
        [SerializeField] float ballSpeed;

        [SerializeField] AudioClip passSE = default;

        [SerializeField] AudioClip boundSE = default;

        [SerializeField] AudioSource audioSource = default;

        [HideInInspector] public state ballState;

        GameObject currentHitObject;

        // �{�[���̃X�e�[�^�X
        public enum state
        {
            none,
            dribble,
            pass,
            miss,
            goal,
            brock,
        }

        // �{�[���̏�����
        public void Setup(state state,GameObject defaultHitObject)
        {
            ballState = state;
            currentHitObject = defaultHitObject;
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
        }

        // �{�[���̍��W�X�V�A�h���u�����ɌĂ΂��
        void BallPositionUpdate()
        {
            if (currentHitObject != null && ballState == state.dribble)
            {
                transform.position = currentHitObject.GetComponent<Playe_Test>().dribblePos.position;
            }

            if (currentHitObject != null && ballState == state.brock)
            {
                transform.position = currentHitObject.GetComponent<slidingManager>().ballPos.position;
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

            audioSource.PlayOneShot(passSE);
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
                case state.brock:
                    value = false;
                    break;
                default:
                    break;
            }

            Debug.Log($"currentValue : {value}");
            return value;
        }

        // object hit
        private void OnTriggerEnter2D(Collider2D collision)
        {

            // outarea
            if (collision.name == outareaDefine.outAreaR ||
                collision.name == outareaDefine.outAreaL ||
                collision.name == outareaDefine.outAreaD ||
                collision.name == outareaDefine.outAreaU )
            {
                Missing();
            }

            // player hit judge
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

            // enemy hit judge
            collision.TryGetComponent<EnemyManager>(out EnemyManager enemy);
            if(enemy != null)
            {
                Destroy(this.gameObject);
                ballState = state.miss;
            }

            // goal hit judge
            collision.TryGetComponent<GoalManager>(out GoalManager goal);
            if (goal != null && ballState == state.pass)
            {
                Destroy(this.gameObject);
                ballState = state.goal;
            }

            collision.TryGetComponent<slidingManager>(out slidingManager slide);
            if (slide != null)
            {
                currentHitObject = collision.gameObject;
                ballState = state.brock;
            }
        }

        // ball status return
        public state GetBallState()
        {
            return ballState;
        }

        public bool GetHavingPlayer()
        {
            return currentHitObject.GetComponent<Playe_Test>().playerCheck;
        }

        void OnBecameInvisible()
        {
            //Missing();
        }

        void Missing()
        {
            audioSource.PlayOneShot(boundSE);
            ballState = state.miss;
            Debug.Log($"miss");
        }
    }
}
