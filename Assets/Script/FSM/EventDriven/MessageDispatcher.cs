using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven
{
    /// <summary>
    /// �޽��� ���� Ŭ����
    /// </summary>
    public class MessageDispatcher
    {
        private static readonly MessageDispatcher instance = new MessageDispatcher();
        public static MessageDispatcher Instance => instance;

        // ���� �߼۵Ǿ�� �ϴ� �޽��� ����
        // �޽����� �ٷ� �����ų� �����ؼ� ������ �Ǵµ� ���� �ð��� ª�� ������ �����ϸ� ������ ����
        // <���� �ð�, Telegram Ÿ�� �޽��� ����>
        private SortedDictionary<float, Telegram> prioritySD;

        public void SetUp()
        {
            if(prioritySD == null)
            {
                prioritySD = new SortedDictionary<float, Telegram>();
            }
        }

        /// <summary>
        /// ������Ʈ���� �޽����� ���� �� ȣ���ϴ� �޼ҵ�
        /// </summary>
        /// <param name="delayTime">�߼� �ð�</param>
        /// <param name="senderName">�߽���</param>
        /// <param name="receiverName">������</param>
        /// <param name="message">�޽���</param>
        public void DispatchMessage(float delayTime, string senderName, string receiverName, string message)
        {
            // ������ �����˻�
            BaseGameEntity receiver = EntityDatabase.Instance.GetEntityFromID(receiverName);

            // �������� �ʴ� ������Ʈ�� ��� ���
            if(receiver == null)
            {
                Debug.Log($"<color=red>Warning! No Receiver with ID of <b><i>{receiverName}</i></b> found</color>");
                return;
            }

            // �޽��� ������ ���� �� �ʱ�ȭ
            Telegram telegram = new Telegram();
            telegram.SetTelegram(0, senderName, receiverName, message);

            // ���� �ð��� ���� �޽����� �ٷ� ����
            if(delayTime <= 0)
            {
                Discharge(receiver, telegram);
            }
            // ���� �ð��� �ִ� �޽����� ������ �� �ð��� ����Ͽ� prioritySD�� ����
            else
            {
                // ���� �ð����� ���� �� �ڿ� ������ �� �ð� ���
                telegram.dispatchTime = Time.time + delayTime;
                // SortDisctionary�� �����Ͽ� ����
                prioritySD.Add(telegram.dispatchTime, telegram);
            }
        }

        /// <summary>
        /// �����ڿ��� �޽��� ����
        /// </summary>
        /// <param name="recevier">������</param>
        /// <param name="telegram">�޽��� ����</param>
        public void Discharge(BaseGameEntity recevier, Telegram telegram)
        {
            recevier.HandleMessage(telegram);
        }

        /// <summary>
        /// ���� �߼۵Ǵ� �޽��� ���� (������ ������Ʈ �޼ҵ忡�� ȣ��)
        /// </summary>
        public void DispatchDelayedMessages()
        {
            foreach(KeyValuePair<float, Telegram> entity in prioritySD)
            {
                if(entity.Key <= Time.time)
                {
                    BaseGameEntity receiver = EntityDatabase.Instance.GetEntityFromID(entity.Value.receiver);

                    Discharge(receiver, entity.Value);  // receiver���� �޽��� ����
                    prioritySD.Remove(entity.Key);      // �켱���� Dictionary �ڷᱸ������ ��� ���� �޽��� ���� ����

                    return;
                }
            }
        }
    }
}