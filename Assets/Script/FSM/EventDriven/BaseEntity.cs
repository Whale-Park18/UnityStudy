using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven
{
    public abstract class BaseGameEntity : MonoBehaviour
    {
        // ���� �����̱� ������ 1���� ����
        private static int m_iNextValidID = 0;

        // BaseEntity�� ��ӹ޴� ��� ���ӿ�����Ʈ�� ID ��ȣ�� �ο��޴µ�
        // �� ��ȣ�� 0���� �����ؼ� 1�� ���� (������ �ֹι�ȣó�� ���)
        private int id;
        public int ID
        {
            set
            {
                id = value;
                m_iNextValidID++;
            }

            get => id;
        }

        private string entityName;      // ������Ʈ �̸�
        private string personalColor;   // ������Ʈ ��(�ؽ�Ʈ ��¿�)

        // �ܺο��� ������Ʈ �̸� ������ ������ ���ֵ��� Get ������Ƽ ����
        public string EntityName => entityName;

        /// <summary>
        /// �Ļ� Ŭ�������� base.Setup()���� ȣ��
        /// </summary>
        /// <param name="name">������Ʈ �̸�</param>
        public virtual void Setup(string name)
        {
            // ���� ��ȣ ����
            ID = m_iNextValidID;

            // �̸� ����
            entityName = name;

            // ���� �� ����
            int color = Random.Range(0, 1_000_000);
            personalColor = $"#{color.ToString("X6")}";
        }

        /// <summary>
        /// GameController Ŭ�������� ��� ������Ʈ�� Update()�� ȣ���� ������Ʈ�� �����Ѵ�.
        /// </summary>
        public abstract void Updated();

        /// <summary>
        /// �޽����� �������� ��, �����ϴ� �޼ҵ� (MessageDispatcher Ŭ�������� ȣ��)
        /// </summary>
        /// <param name="telegram">�޽��� ����</param>
        /// <returns></returns>
        public abstract bool HandleMessage(Telegram telegram);

        /// <summary>
        /// Console View�� [�̸�: "���"] ���
        /// </summary>
        /// <param name="message">�޽���</param>
        public void Print(string message)
        {
            Debug.Log($"<color={personalColor}><b>{entityName}</b></color> : {message}");
        }
    }
}