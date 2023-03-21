using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven
{
    /// <summary>
    /// 메시지 관리 클래스
    /// </summary>
    public class MessageDispatcher
    {
        private static readonly MessageDispatcher instance = new MessageDispatcher();
        public static MessageDispatcher Instance => instance;

        // 지연 발송되어야 하는 메시지 관리
        // 메시지는 바로 보내거나 지연해서 보내게 되는데 지연 시간이 짧은 순으로 정렬하면 관리에 용이
        // <지연 시간, Telegram 타입 메시지 정보>
        private SortedDictionary<float, Telegram> prioritySD;

        public void SetUp()
        {
            if(prioritySD == null)
            {
                prioritySD = new SortedDictionary<float, Telegram>();
            }
        }

        /// <summary>
        /// 에이전트에게 메시지를 보낼 때 호출하는 메소드
        /// </summary>
        /// <param name="delayTime">발송 시간</param>
        /// <param name="senderName">발신자</param>
        /// <param name="receiverName">수신자</param>
        /// <param name="message">메시지</param>
        public void DispatchMessage(float delayTime, string senderName, string receiverName, string message)
        {
            // 수신자 정보검색
            BaseGameEntity receiver = EntityDatabase.Instance.GetEntityFromID(receiverName);

            // 존재하지 않는 에이전트면 경고문 출력
            if(receiver == null)
            {
                Debug.Log($"<color=red>Warning! No Receiver with ID of <b><i>{receiverName}</i></b> found</color>");
                return;
            }

            // 메시지 데이터 생성 및 초기화
            Telegram telegram = new Telegram();
            telegram.SetTelegram(0, senderName, receiverName, message);

            // 지연 시간이 없는 메시지는 바로 전송
            if(delayTime <= 0)
            {
                Discharge(receiver, telegram);
            }
            // 지연 시간이 있는 메시지는 보내야 할 시간을 기록하여 prioritySD에 저장
            else
            {
                // 현재 시간으로 부터 얼마 뒤에 가능한 지 시간 계산
                telegram.dispatchTime = Time.time + delayTime;
                // SortDisctionary에 저장하여 관리
                prioritySD.Add(telegram.dispatchTime, telegram);
            }
        }

        /// <summary>
        /// 수신자에게 메시지 전송
        /// </summary>
        /// <param name="recevier">수신자</param>
        /// <param name="telegram">메시지 정보</param>
        public void Discharge(BaseGameEntity recevier, Telegram telegram)
        {
            recevier.HandleMessage(telegram);
        }

        /// <summary>
        /// 지연 발송되는 메시지 관리 (게임의 업데이트 메소드에서 호출)
        /// </summary>
        public void DispatchDelayedMessages()
        {
            foreach(KeyValuePair<float, Telegram> entity in prioritySD)
            {
                if(entity.Key <= Time.time)
                {
                    BaseGameEntity receiver = EntityDatabase.Instance.GetEntityFromID(entity.Value.receiver);

                    Discharge(receiver, entity.Value);  // receiver에게 메시지 전송
                    prioritySD.Remove(entity.Key);      // 우선순위 Dictionary 자료구조에서 방금 보낸 메시지 정보 삭제

                    return;
                }
            }
        }
    }
}