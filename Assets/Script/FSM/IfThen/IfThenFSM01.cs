using UnityEngine;

namespace WhalePark18.FSM.IfThen
{
    /// <summary>
    /// 플레이어의 행동
    /// </summary>
    public enum PlayerState { Idle = 0, Walk, Run, Attack }

    /// <summary>
    /// if문으로 구현된 FSM(if문 대신 switch문으로 대체 가능)
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
            // 1~4 숫자키를 눌러 상태 변경
            if (Input.GetKeyDown("1"))      ChangeState(PlayerState.Idle);
            else if (Input.GetKeyDown("2")) ChangeState(PlayerState.Walk);
            else if (Input.GetKeyDown("3")) ChangeState(PlayerState.Run);
            else if (Input.GetKeyDown("4")) ChangeState(PlayerState.Attack);

            UpdateState();
        }

        /// <summary>
        /// playerState에 따라 현재 플레이어 행동을 실행하는 메소드
        /// </summary>
        private void UpdateState()
        {
            if (playerState == PlayerState.Idle)
            {
                Debug.Log("플레이어가 대기중입니다.");
            }
            else if (playerState == PlayerState.Walk)
            {
                Debug.Log("플레이어가 걷는 중입니다.");
            }
            else if (playerState == PlayerState.Run)
            {
                Debug.Log("플레이어가 달리는 중입니다.");
            }
            else if (playerState == PlayerState.Attack)
            {
                Debug.Log("플레이어가 공격하는 중입니다.");
            }
        }

        /// <summary>
        /// 플레이어의 상태를 newState로 변경한다.
        /// </summary>
        /// <param name="newState">변경할 상태</param>
        private void ChangeState(PlayerState newState)
        {
            playerState = newState;
        }
    }
}