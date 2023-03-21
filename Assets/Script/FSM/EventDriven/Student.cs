using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven
{
    public enum StudentStates { RestAndSleep = 0, StudyHard, TakeAExam, PlayAGame, HitTheBottle, GMessageReceive }

    public class Student : BaseGameEntity
    {
        private int             knowledge;          // 지식 : 도서관에서 증가하고, 강의실에서 소모
        private int             stress;             // 스트레스 : 도서관에서 증가하고, 집에서 감소
        private int             fatigue;            // 피로 : 집을 제외한 모든 곳에서 증가하고, 집에서 감소
                                                    //        강의실과 PC방에서 임의의 경우로 대폭 증가
        private int             totalScore;         // 점수 : 강의실에서 테스트를 볼 때 임의 증가
                                                    //   * 모든 학생의 목표는 A+, 점수가 100점이 되면 목표 달성으로 중지 
        private Locations       currentLocation;    // 현재 위치 : 현재 에이전트가 머물고 있는 장소
        private StudentStates   currentState;       // 현재 상태

        private State<Student>[] states;            // Student가 갖는 모든 상태
        private StateMachine<Student> stateMachine; // 상태를 대신 관리하는 StateMachine

        public int Knowledage
        {
            set => knowledge = Mathf.Max(0, value);
            get => knowledge;
        }
        public int Stress
        {
            set => stress = Mathf.Max(0, value);
            get => stress;
        }
        public int Fatigue
        {
            set => fatigue = Mathf.Max(0, value);
            get => fatigue;
        }
        public int TotalScore
        {
            set => totalScore = Mathf.Max(0, value);
            get => totalScore;
        }
        public Locations CurrentLocation
        {
            set => currentLocation = value;
            get => currentLocation;
        }
        public StudentStates CurrentState => currentState;

        public override void Setup(string name)
        {
            // 기반 클래스의 SetUp 메소드 호출 (ID, 이름, 색 설정)
            base.Setup(name);

            // 생성되는 오브젝트 이름 설정
            gameObject.name = $"{ID:D2}_Student_{name}";

            // Student가 가질 수 있는 상태 개수만큼 메모리 할당
            states                                      = new State<Student>[6];
            states[(int)StudentStates.RestAndSleep]     = new StudentOwnedStates.RestAndSleep();
            states[(int)StudentStates.StudyHard]        = new StudentOwnedStates.StudyHard();
            states[(int)StudentStates.TakeAExam]        = new StudentOwnedStates.TakeAExam();
            states[(int)StudentStates.PlayAGame]        = new StudentOwnedStates.PlayAGame();
            states[(int)StudentStates.HitTheBottle]     = new StudentOwnedStates.HitTheBottle();
            states[(int)StudentStates.GMessageReceive]  = new StudentOwnedStates.GlobalMessageReceive();

            // 상태를 관리하는 StateMachine에 메모리를 할당, 첫 상태를 'RestAndSleep' 상태로 설정
            stateMachine = new StateMachine<Student>();
            stateMachine.SetUp(this, states[(int)StudentStates.RestAndSleep]);
            // 전역 상태 설정
            stateMachine.SetGlobalState(states[(int)StudentStates.GMessageReceive]);

            // Student 속성 초기화
            knowledge = 0;
            stress = 0;
            fatigue = 0;
            totalScore = 0;
            currentLocation = Locations.SweetHome;
        }

        public override void Updated()
        {
            stateMachine.Excute();
        }

        /// <summary>
        /// 상태를 변경하는 인터페이스
        /// </summary>
        /// <param name="newState">변경할 상태</param>
        public void ChangeState(StudentStates newState)
        {
            currentState = newState;
            stateMachine.ChangeState(states[(int)newState]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="telegram"></param>
        /// <returns></returns>
        public override bool HandleMessage(Telegram telegram)
        {
            return stateMachine.HandleMessage(telegram);
        }
    }
}