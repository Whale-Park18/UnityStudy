using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.Generic
{
    public enum StudentStates { RestAndSleep = 0, StudyHard, TakeAExam, PlayAGame, HitTheBottle }

    public class Student : BaseGameEntity
    {
        private int knowledge;                      // ���� : ���������� �����ϰ�, ���ǽǿ��� �Ҹ�

        private int stress;                         // ��Ʈ���� : ���������� �����ϰ�, ������ ����

        private int fatigue;                        // �Ƿ� : ���� ������ ��� ������ �����ϰ�, ������ ����
                                                    //        ���ǽǰ� PC�濡�� ������ ���� ���� ����

        private int totalScore;                     // ���� : ���ǽǿ��� �׽�Ʈ�� �� �� ���� ����
                                                    //   * ��� �л��� ��ǥ�� A+, ������ 100���� �Ǹ� ��ǥ �޼����� ���� 

        private Locations currentLocation;          // ���� ��ġ : ���� ������Ʈ�� �ӹ��� �ִ� ���

        private State<Student>[] states;            // Student�� ���� ��� ����
        private StateMachine<Student> stateMachine; // ���¸� ��� �����ϴ� StateMachine

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
            // ��� Ŭ������ SetUp �޼ҵ� ȣ�� (ID, �̸�, �� ����)
            base.Setup(name);

            // �����Ǵ� ������Ʈ �̸� ����
            gameObject.name = $"{ID:D2}_Student_{name}";

            // Student�� ���� �� �ִ� ���� ������ŭ �޸� �Ҵ�
            states                                  = new State<Student>[5];
            states[(int)StudentStates.RestAndSleep] = new StudentOwnedStates.RestAndSleep();
            states[(int)StudentStates.StudyHard]    = new StudentOwnedStates.StudyHard();
            states[(int)StudentStates.TakeAExam]    = new StudentOwnedStates.TakeAExam();
            states[(int)StudentStates.PlayAGame]    =new StudentOwnedStates.PlayAGame();
            states[(int)StudentStates.HitTheBottle] =new StudentOwnedStates.HitTheBottle();

            // ���¸� �����ϴ� StateMachine�� �޸𸮸� �Ҵ�, ù ���¸� 'RestAndSleep' ���·� ����
            stateMachine = new StateMachine<Student>();
            stateMachine.SetUp(this, states[(int)StudentStates.RestAndSleep]);

            // Student �Ӽ� �ʱ�ȭ
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
        /// ���¸� �����ϴ� �������̽�
        /// </summary>
        /// <param name="newState">������ ����</param>
        public void ChangeState(StudentStates newState)
        {
            stateMachine.ChangeState(states[(int)newState]);
        }
    }
}