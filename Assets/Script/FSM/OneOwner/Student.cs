using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.OneOwner
{
    public enum StudentStates { RestAndSleep = 0, StudyHard, TakeAExam, PlayAGame, HitTheBottle }

    public class Student : BaseGameEntity
    {
        private int knowledge;              // 지식 : 도서관에서 증가하고, 강의실에서 소모

        private int stress;                 // 스트레스 : 도서관에서 증가하고, 집에서 감소

        private int fatigue;                // 피로 : 집을 제외한 모든 곳에서 증가하고, 집에서 감소
                                            //        강의실과 PC방에서 임의의 경우로 대폭 증가

        private int totalScore;             // 점수 : 강의실에서 테스트를 볼 때 임의 증가
                                            //   * 모든 학생의 목표는 A+, 점수가 100점이 되면 목표 달성으로 중지 

        private Locations currentLocation;  // 현재 위치 : 현재 에이전트가 머물고 있는 장소

        private State[] states;             // Student가 갖는 모든 상태
        private State   currentState;       // 현재 상태

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

        public override void Setup(string name)
        {
            // 기반 클래스의 SetUp 메소드 호출 (ID, 이름, 색 설정)
            base.Setup(name);

            // 생성되는 오브젝트 이름 설정
            gameObject.name = $"{ID:D2}_Student_{name}";

            // Student가 가질 수 있는 상태 개수만큼 메모리 할당
            states                                  = new State[5];
            states[(int)StudentStates.RestAndSleep] = new StudentOwnedStates.RestAndSleep();
            states[(int)StudentStates.StudyHard]    = new StudentOwnedStates.StudyHard();
            states[(int)StudentStates.TakeAExam]    = new StudentOwnedStates.TakeAExam();
            states[(int)StudentStates.PlayAGame]    =new StudentOwnedStates.PlayAGame();
            states[(int)StudentStates.HitTheBottle] =new StudentOwnedStates.HitTheBottle();


            // Student 속성 초기화
            knowledge = 0;
            stress = 0;
            fatigue = 0;
            totalScore = 0;
            currentLocation = Locations.SweetHome;

            // 현재 상태를 집에서 쉬는 'RestAndSleep' 상태로 변경
            ChangeState(StudentStates.RestAndSleep);
        }

        public override void Updated()
        {
            if(currentState != null)
            {
                currentState.Excute(this);
            }
        }

        public void ChangeState(StudentStates newState)
        {
            // 새로 바꾸는 상태가 비어있다면 바꾸지 않는다.
            if (states[(int)newState] == null) return;

            // 현재 재생중인 상태가 있다면 Exit() 메소드 호출
            if(currentState != null)
            {
                currentState.Exit(this);
            }

            // 새로운 상태로 변경하고, 새로 바뀐 상태의 Enter() 메소드 호출
            currentState = states[(int)newState];
            currentState.Enter(this);
        }
    }
}