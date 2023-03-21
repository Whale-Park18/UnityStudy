using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.Generic
{

    public class StateMachine<T> where T : class
    {
        private T           ownerEntity;    // StateMachine 소유주
        private State<T>    currentState;   // 현재 상태
        private State<T>    previousState;  // 이전 상태
        private State<T>    globalState;    // 전역 상태

        /// <summary>
        /// StateMachine 초기 설정 메소드
        /// </summary>
        /// <param name="owner">StateMachine 소유 객체</param>
        /// <param name="entryState">초기 상태</param>
        public void SetUp(T owner, State<T> entryState)
        {
            ownerEntity     = owner;
            currentState    = null;
            previousState   = null;
            globalState     = null;

            // entryState 상태로 상태 변경
            ChangeState(entryState);
        }

        /// <summary>
        /// 상태 실행 메소드
        /// </summary>
        public void Excute()
        {
            if(globalState != null)
            {
                globalState.Excute(ownerEntity);
            }

            if(currentState != null)
            {
                currentState.Excute(ownerEntity);
            }
        }

        /// <summary>
        /// 상태를 변경하는 메소드
        /// </summary>
        /// <param name="newState">변경할 상태</param>
        public void ChangeState(State<T> newState)
        {
            // 새로 바꾸는 상태가 비어있다면 바꾸지 않는다.
            if (newState == null) return;

            // 현재 재생중인 상태가 있다면 Exit() 메소드 호출
            if (currentState != null)
            {
                // 상태가 변경되면 현재 상태는 이전 상태가 되기 때문에 previousState에 저장
                previousState = currentState;

                currentState.Exit(ownerEntity);
            }

            // 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
            currentState = newState;
            currentState.Enter(ownerEntity);
        }

        /// <summary>
        /// 전역 상태를 설정하는 메소드
        /// </summary>
        /// <param name="newState">전역 상태에 초기화할 새로운 상태</param>
        public void SetGlobalState(State<T> newState)
        {
            globalState = newState;
        }

        /// <summary>
        /// 현재 상태를 이전 상태로 되돌리는 메소드
        /// </summary>
        public void RevertToPreviousState()
        {
            ChangeState(previousState);
        }
    }
}