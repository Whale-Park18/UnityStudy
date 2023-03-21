using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.Generic
{
    public enum UnemployedStates { RestAndSleep = 0, PlayAGame, HitTheBottle, VisitBathroom, Global, }

    public class Unemployed : BaseGameEntity
    {
        private int         bored;                      // 지루함 : 집에서 증가하고, 술을 먹을 때 임의의 확률로 증가 or 감소
        private int         stress;                     // 스트레스 : PC방에서 임의의 경우 증가하고, 술집이나 집에서 감소
        private int         fatigue;                    // 피로 : 집을 제외한 모든 곳에서 증가하고, 집에서 감소
        private Locations   currentLocation;            // 현재 위치 : 현재 에이전트가 머물고 있는 장소

        private State<Unemployed>[] states;             // Student가 갖는 모든 상태
        private StateMachine<Unemployed> stateMachine;  // 상태를 대신 관리하는 StateMachine

        public int Boared
        {
            set => bored = Mathf.Max(0, value);
            get => bored;
        }

        public int Stress
        {
            set => stress= Mathf.Max(0, value);
            get => stress;
        }

        public int Fatigue
        {
            set => fatigue = Mathf.Max(0, value);
            get => fatigue;
        }

        public Locations CurrentLocation
        {
            set => currentLocation = value;
            get => currentLocation;
        }

        // 현재 상태 프로퍼티
        public UnemployedStates CurrentState { private set; get; }

        public override void Setup(string name)
        {
            // 기반 클래스의 SetUp 메소드 호출 (ID, 이름, 색 설정)
            base.Setup(name);

            // 생성되는 오브젝트 이름 설정
            gameObject.name = $"{ID:D2}_Unemployed_{name}";

            // Unemployed 가질 수 있는 상태 개수만큼 메모리 할당
            states = new State<Unemployed>[5];
            states[(int)UnemployedStates.RestAndSleep]  = new UnemployedOwnedStates.RestAndSleep();
            states[(int)UnemployedStates.PlayAGame]     = new UnemployedOwnedStates.PlayAGame();
            states[(int)UnemployedStates.HitTheBottle]  = new UnemployedOwnedStates.HitTheBottle();
            states[(int)UnemployedStates.VisitBathroom] = new UnemployedOwnedStates.VisitBathroom();
            states[(int)UnemployedStates.Global]        = new UnemployedOwnedStates.StateGlobal();

            // 상태를 관리하는 StateMachine에 메모리를 할당, 첫 상태를 'RestAndSleep' 상태로 설정
            stateMachine = new StateMachine<Unemployed>();
            stateMachine.SetUp(this, states[(int)UnemployedStates.RestAndSleep]);

            // 전역 상태 설정
            stateMachine.SetGlobalState(states[(int)UnemployedStates.Global]);

            // Unemployed 속성 초기화
            stress = 0;
            fatigue = 0;
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
        public void ChangeState(UnemployedStates newState)
        {
            CurrentState = newState;

            stateMachine.ChangeState(states[(int)newState]);
        }

        /// <summary>
        /// 현재 상태를 이전 상태로 되돌리는 메소드
        /// </summary>
        public void RevertToPreviousState()
        {
            stateMachine.RevertToPreviousState();
        }
    }
}