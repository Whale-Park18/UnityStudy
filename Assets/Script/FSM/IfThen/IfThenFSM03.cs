using System.Collections;
using UnityEngine;

namespace WhalePark18.FSM.IfThen
{
    public class IfThenFSM03 : MonoBehaviour
    {
        private PlayerState playerState;

        private void Awake()
        {
            ChangeState(PlayerState.Idle);
        }

        private void Update()
        {
            // 1~4 숫자키를 눌러 상태 변경
            if (Input.GetKeyDown("1")) ChangeState(PlayerState.Idle);
            else if (Input.GetKeyDown("2")) ChangeState(PlayerState.Walk);
            else if (Input.GetKeyDown("3")) ChangeState(PlayerState.Run);
            else if (Input.GetKeyDown("4")) ChangeState(PlayerState.Attack);
        }

        /// <summary>
        /// 플레이어의 상태를 newState로 변경한다.
        /// </summary>
        /// <param name="newState">변경할 상태</param>
        private void ChangeState(PlayerState newState)
        {
            // 열거형 변수.ToString()을 사용하면 열거형에 정의된 변수 이름 string을 반환
            // playerState == PlayerState.Idle일 때 playerState.ToString()은 "Idle"를 반환

            // 열거형에 정의된 상태와 동일한 이름의 코루틴 메소드를 정의
            // playerState의 현재 상태에 따라 코루틴 메소드 실행

            StopCoroutine(playerState.ToString());
            playerState = newState;
            StartCoroutine(playerState.ToString());
        }

        private IEnumerator Idle()
        {
            Debug.Log("비전투 모드로 변경");
            Debug.Log("체력/마력이 초당 10씩 자동 회복");

            while (true)
            {
                Debug.Log("플레이어가 대기중입니다.");
                yield return null;
            }
        }

        private IEnumerator Walk()
        {
            Debug.Log("이동속도를 2로 설정한다.");

            while (true)
            {
                Debug.Log("플레이어가 걷는 중입니다.");
                yield return null;
            }
        }

        private IEnumerator Run()
        {
            Debug.Log("이동속도를 5로 설정한다.");

            while (true)
            {
                Debug.Log("플레이어가 달리는 중입니다.");
                yield return null;
            }
        }

        private IEnumerator Attack()
        {
            Debug.Log("전투 모드로 변경");
            Debug.Log("자동 회복이 중지됩니다.");

            while (true)
            {
                Debug.Log("플레이어가 공격하는 중입니다.");
                yield return null;
            }
        }
    }
}