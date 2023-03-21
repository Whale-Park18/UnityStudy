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
            // 1~4 ����Ű�� ���� ���� ����
            if (Input.GetKeyDown("1")) ChangeState(PlayerState.Idle);
            else if (Input.GetKeyDown("2")) ChangeState(PlayerState.Walk);
            else if (Input.GetKeyDown("3")) ChangeState(PlayerState.Run);
            else if (Input.GetKeyDown("4")) ChangeState(PlayerState.Attack);
        }

        /// <summary>
        /// �÷��̾��� ���¸� newState�� �����Ѵ�.
        /// </summary>
        /// <param name="newState">������ ����</param>
        private void ChangeState(PlayerState newState)
        {
            // ������ ����.ToString()�� ����ϸ� �������� ���ǵ� ���� �̸� string�� ��ȯ
            // playerState == PlayerState.Idle�� �� playerState.ToString()�� "Idle"�� ��ȯ

            // �������� ���ǵ� ���¿� ������ �̸��� �ڷ�ƾ �޼ҵ带 ����
            // playerState�� ���� ���¿� ���� �ڷ�ƾ �޼ҵ� ����

            StopCoroutine(playerState.ToString());
            playerState = newState;
            StartCoroutine(playerState.ToString());
        }

        private IEnumerator Idle()
        {
            Debug.Log("������ ���� ����");
            Debug.Log("ü��/������ �ʴ� 10�� �ڵ� ȸ��");

            while (true)
            {
                Debug.Log("�÷��̾ ������Դϴ�.");
                yield return null;
            }
        }

        private IEnumerator Walk()
        {
            Debug.Log("�̵��ӵ��� 2�� �����Ѵ�.");

            while (true)
            {
                Debug.Log("�÷��̾ �ȴ� ���Դϴ�.");
                yield return null;
            }
        }

        private IEnumerator Run()
        {
            Debug.Log("�̵��ӵ��� 5�� �����Ѵ�.");

            while (true)
            {
                Debug.Log("�÷��̾ �޸��� ���Դϴ�.");
                yield return null;
            }
        }

        private IEnumerator Attack()
        {
            Debug.Log("���� ���� ����");
            Debug.Log("�ڵ� ȸ���� �����˴ϴ�.");

            while (true)
            {
                Debug.Log("�÷��̾ �����ϴ� ���Դϴ�.");
                yield return null;
            }
        }
    }
}