using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.Generic
{
    public enum UnemployedStates { RestAndSleep = 0, PlayAGame, HitTheBottle, VisitBathroom, Global, }

    public class Unemployed : BaseGameEntity
    {
        private int         bored;                      // ������ : ������ �����ϰ�, ���� ���� �� ������ Ȯ���� ���� or ����
        private int         stress;                     // ��Ʈ���� : PC�濡�� ������ ��� �����ϰ�, �����̳� ������ ����
        private int         fatigue;                    // �Ƿ� : ���� ������ ��� ������ �����ϰ�, ������ ����
        private Locations   currentLocation;            // ���� ��ġ : ���� ������Ʈ�� �ӹ��� �ִ� ���

        private State<Unemployed>[] states;             // Student�� ���� ��� ����
        private StateMachine<Unemployed> stateMachine;  // ���¸� ��� �����ϴ� StateMachine

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

        // ���� ���� ������Ƽ
        public UnemployedStates CurrentState { private set; get; }

        public override void Setup(string name)
        {
            // ��� Ŭ������ SetUp �޼ҵ� ȣ�� (ID, �̸�, �� ����)
            base.Setup(name);

            // �����Ǵ� ������Ʈ �̸� ����
            gameObject.name = $"{ID:D2}_Unemployed_{name}";

            // Unemployed ���� �� �ִ� ���� ������ŭ �޸� �Ҵ�
            states = new State<Unemployed>[5];
            states[(int)UnemployedStates.RestAndSleep]  = new UnemployedOwnedStates.RestAndSleep();
            states[(int)UnemployedStates.PlayAGame]     = new UnemployedOwnedStates.PlayAGame();
            states[(int)UnemployedStates.HitTheBottle]  = new UnemployedOwnedStates.HitTheBottle();
            states[(int)UnemployedStates.VisitBathroom] = new UnemployedOwnedStates.VisitBathroom();
            states[(int)UnemployedStates.Global]        = new UnemployedOwnedStates.StateGlobal();

            // ���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ�, ù ���¸� 'RestAndSleep' ���·� ����
            stateMachine = new StateMachine<Unemployed>();
            stateMachine.SetUp(this, states[(int)UnemployedStates.RestAndSleep]);

            // ���� ���� ����
            stateMachine.SetGlobalState(states[(int)UnemployedStates.Global]);

            // Unemployed �Ӽ� �ʱ�ȭ
            stress = 0;
            fatigue = 0;
            currentLocation = Locations.SweetHome;
        }

        public override void Updated()
        {
            stateMachine.Excute();
        }

        /// <summary>
        /// ���¸� �����ϴ� �������̽�
        /// </summary>
        /// <param name="newState">������ ����</param>
        public void ChangeState(UnemployedStates newState)
        {
            CurrentState = newState;

            stateMachine.ChangeState(states[(int)newState]);
        }

        /// <summary>
        /// ���� ���¸� ���� ���·� �ǵ����� �޼ҵ�
        /// </summary>
        public void RevertToPreviousState()
        {
            stateMachine.RevertToPreviousState();
        }
    }
}