using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.Generic
{

    public class StateMachine<T> where T : class
    {
        private T           ownerEntity;    // StateMachine ������
        private State<T>    currentState;   // ���� ����
        private State<T>    previousState;  // ���� ����
        private State<T>    globalState;    // ���� ����

        /// <summary>
        /// StateMachine �ʱ� ���� �޼ҵ�
        /// </summary>
        /// <param name="owner">StateMachine ���� ��ü</param>
        /// <param name="entryState">�ʱ� ����</param>
        public void SetUp(T owner, State<T> entryState)
        {
            ownerEntity     = owner;
            currentState    = null;
            previousState   = null;
            globalState     = null;

            // entryState ���·� ���� ����
            ChangeState(entryState);
        }

        /// <summary>
        /// ���� ���� �޼ҵ�
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
        /// ���¸� �����ϴ� �޼ҵ�
        /// </summary>
        /// <param name="newState">������ ����</param>
        public void ChangeState(State<T> newState)
        {
            // ���� �ٲٴ� ���°� ����ִٸ� �ٲ��� �ʴ´�.
            if (newState == null) return;

            // ���� ������� ���°� �ִٸ� Exit() �޼ҵ� ȣ��
            if (currentState != null)
            {
                // ���°� ����Ǹ� ���� ���´� ���� ���°� �Ǳ� ������ previousState�� ����
                previousState = currentState;

                currentState.Exit(ownerEntity);
            }

            // ���ο� ���·� �����ϰ�, ���� �ٲ� ������ Enter() �޼ҵ� ȣ��
            currentState = newState;
            currentState.Enter(ownerEntity);
        }

        /// <summary>
        /// ���� ���¸� �����ϴ� �޼ҵ�
        /// </summary>
        /// <param name="newState">���� ���¿� �ʱ�ȭ�� ���ο� ����</param>
        public void SetGlobalState(State<T> newState)
        {
            globalState = newState;
        }

        /// <summary>
        /// ���� ���¸� ���� ���·� �ǵ����� �޼ҵ�
        /// </summary>
        public void RevertToPreviousState()
        {
            ChangeState(previousState);
        }
    }
}