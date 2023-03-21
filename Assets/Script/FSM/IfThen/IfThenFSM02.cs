using UnityEngine;

namespace WhalePark18.FSM.IfThen
{
    public class IfThenFSM02 : MonoBehaviour
    {
        private PlayerState playerState;
        
        // ���°� �ٲ� �� true�� �Ǿ� ���� ������Ʈ ���ο��� 1ȸ�� ����Ǵ� ������ �����ϰ� flase�� ����
        private bool isChanged = false;

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
        /// <remarks>
        /// 01�������� �ܼ��� ���·� �������� ��, 1ȸ�� �ϴ� �ൿ�� �߰����� ��������
        /// ���� ���� ĳ���͸� �����Ѵٸ� ���� ���¿��� �ٸ� ���·� ����Ǵ� ����, 
        /// �ش� ���·� ����� �� �ʱ�ȭ�ϴ� ����, Ư�� �ൿ������ �ϴ� Ư�� �ൿ ��
        /// ���� �پ��ϰ� ������ ������� �߰��Ǿ�� �ϱ� ������ �ڵ��� ���� ���ϱ޼�������
        /// �þ ���̴�.
        /// </remarks>
        private void UpdateState()
        {
            if (playerState == PlayerState.Idle)
            {
                // ���·� ������ �� 1ȸ ȣ���ϴ� ����
                if(isChanged)
                {
                    Debug.Log("������ ���� ����");
                    Debug.Log("ü��/������ �ʴ� 10�� �ڵ� ȸ��");

                    isChanged = false;
                }

                Debug.Log("�÷��̾ ������Դϴ�.");
            }
            else if (playerState == PlayerState.Walk)
            {
                // ���·� ������ �� 1ȸ ȣ���ϴ� ����
                if (isChanged)
                {
                    Debug.Log("�̵��ӵ��� 2�� �����Ѵ�.");

                    isChanged = false;
                }

                Debug.Log("�÷��̾ �ȴ� ���Դϴ�.");
            }
            else if (playerState == PlayerState.Run)
            {
                // ���·� ������ �� 1ȸ ȣ���ϴ� ����
                if(isChanged)
                {
                    Debug.Log("�̵��ӵ��� 5�� �����Ѵ�.");

                    isChanged = false;
                }

                Debug.Log("�÷��̾ �޸��� ���Դϴ�.");
            }
            else if (playerState == PlayerState.Attack)
            {
                // ���·� ������ �� 1ȸ ȣ���ϴ� ����
                if(isChanged)
                {
                    Debug.Log("���� ���� ����");
                    Debug.Log("�ڵ� ȸ���� �����˴ϴ�.");

                    isChanged = false;
                }

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
            isChanged = true;
        }
    }
}