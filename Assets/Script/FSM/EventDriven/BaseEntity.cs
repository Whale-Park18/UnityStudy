using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven
{
    public abstract class BaseGameEntity : MonoBehaviour
    {
        // 정적 변수이기 때문에 1개만 존재
        private static int m_iNextValidID = 0;

        // BaseEntity를 상속받는 모든 게임오브젝트는 ID 번호를 부여받는데
        // 이 번호는 0부터 시작해서 1씩 증가 (현실의 주민번호처럼 사용)
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

        private string entityName;      // 에이전트 이름
        private string personalColor;   // 에이전트 색(텍스트 출력용)

        // 외부에서 에이전트 이름 정보를 열람할 수있도록 Get 프로퍼티 정의
        public string EntityName => entityName;

        /// <summary>
        /// 파생 클래스에서 base.Setup()으로 호출
        /// </summary>
        /// <param name="name">에이전트 이름</param>
        public virtual void Setup(string name)
        {
            // 고유 번호 설정
            ID = m_iNextValidID;

            // 이름 설정
            entityName = name;

            // 고유 색 설정
            int color = Random.Range(0, 1_000_000);
            personalColor = $"#{color.ToString("X6")}";
        }

        /// <summary>
        /// GameController 클래스에서 모든 에이전트의 Update()를 호출해 에이전트를 구동한다.
        /// </summary>
        public abstract void Updated();

        /// <summary>
        /// 메시지를 전송했을 때, 수신하는 메소드 (MessageDispatcher 클래스에서 호출)
        /// </summary>
        /// <param name="telegram">메시지 정보</param>
        /// <returns></returns>
        public abstract bool HandleMessage(Telegram telegram);

        /// <summary>
        /// Console View에 [이름: "대사"] 출력
        /// </summary>
        /// <param name="message">메시지</param>
        public void Print(string message)
        {
            Debug.Log($"<color={personalColor}><b>{entityName}</b></color> : {message}");
        }
    }
}