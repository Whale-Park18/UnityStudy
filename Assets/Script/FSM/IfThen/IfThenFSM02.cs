using UnityEngine;

namespace WhalePark18.FSM.IfThen
{
    public class IfThenFSM02 : MonoBehaviour
    {
        private PlayerState playerState;
        
        // 상태가 바뀔 때 true가 되어 상태 업데이트 내부에서 1회만 실행되는 내용을 실행하고 flase로 변경
        private bool isChanged = false;

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
        /// <remarks>
        /// 01버전에서 단순히 상태로 진입했을 때, 1회만 하는 행동을 추가했을 뿐이지만
        /// 실제 게임 캐릭터를 구현한다면 현재 상태에서 다른 상태로 변경되는 조건, 
        /// 해당 상태로 변경될 때 초기화하는 내용, 특정 행동에서만 하는 특수 행동 등
        /// 더욱 다양하고 복잡한 내용들이 추가되어야 하기 때문에 코드의 양이 기하급수적으로
        /// 늘어날 것이다.
        /// </remarks>
        private void UpdateState()
        {
            if (playerState == PlayerState.Idle)
            {
                // 상태로 진입할 때 1회 호출하는 내용
                if(isChanged)
                {
                    Debug.Log("비전투 모드로 변경");
                    Debug.Log("체력/마력이 초당 10씩 자동 회복");

                    isChanged = false;
                }

                Debug.Log("플레이어가 대기중입니다.");
            }
            else if (playerState == PlayerState.Walk)
            {
                // 상태로 진입할 때 1회 호출하는 내용
                if (isChanged)
                {
                    Debug.Log("이동속도를 2로 설정한다.");

                    isChanged = false;
                }

                Debug.Log("플레이어가 걷는 중입니다.");
            }
            else if (playerState == PlayerState.Run)
            {
                // 상태로 진입할 때 1회 호출하는 내용
                if(isChanged)
                {
                    Debug.Log("이동속도를 5로 설정한다.");

                    isChanged = false;
                }

                Debug.Log("플레이어가 달리는 중입니다.");
            }
            else if (playerState == PlayerState.Attack)
            {
                // 상태로 진입할 때 1회 호출하는 내용
                if(isChanged)
                {
                    Debug.Log("전투 모드로 변경");
                    Debug.Log("자동 회복이 중지됩니다.");

                    isChanged = false;
                }

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
            isChanged = true;
        }
    }
}