using UnityEngine;

namespace WhalePark18.FSM.IfThen
{
    /// <summary>
    /// �÷��̾��� �ൿ
    /// </summary>
    public enum PlayerState { Idle = 0, Walk, Run, Attack }

    /// <summary>
    /// if������ ������ FSM(if�� ��� switch������ ��ü ����)
    /// </summary>
    public class IfThenFSM01 : MonoBehaviour
    {
        private PlayerState playerState;

        private void Awake()
        {
            ChangeState(PlayerState.Idle);
        }

        private void Update()
        {
            // 1~4 ����Ű�� ���� ���� ����
            if (Input.GetKeyDown("1"))      ChangeState(PlayerState.Idle);
            else if (Input.GetKeyDown("2")) ChangeState(PlayerState.Walk);
            else if (Input.GetKeyDown("3")) ChangeState(PlayerState.Run);
            else if (Input.GetKeyDown("4")) ChangeState(PlayerState.Attack);

            UpdateState();
        }

        /// <summary>
        /// playerState�� ���� ���� �÷��̾� �ൿ�� �����ϴ� �޼ҵ�
        /// </summary>
        private void UpdateState()
        {
            if (playerState == PlayerState.Idle)
            {
                Debug.Log("�÷��̾ ������Դϴ�.");
            }
            else if (playerState == PlayerState.Walk)
            {
                Debug.Log("�÷��̾ �ȴ� ���Դϴ�.");
            }
            else if (playerState == PlayerState.Run)
            {
                Debug.Log("�÷��̾ �޸��� ���Դϴ�.");
            }
            else if (playerState == PlayerState.Attack)
            {
                Debug.Log("�÷��̾ �����ϴ� ���Դϴ�.");
            }
        }

        /// <summary>
        /// �÷��̾��� ���¸� newState�� �����Ѵ�.
        /// </summary>
        /// <param name="newState">������ ����</param>
        private void ChangeState(PlayerState newState)
        {
            playerState = newState;
        }
    }
}