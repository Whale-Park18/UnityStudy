using UnityEngine;

namespace WhalePark18.FSM.EventDriven.StudentOwnedStates
{
    public class RestAndSleep : State<Student>
    {
        public override void Enter(Student entity)
        {
            // ��Ҹ� ������ ����, ���� ���� ��Ʈ������ 0�� �ȴ�.
            entity.CurrentLocation  = Locations.SweetHome;
            entity.Stress           = 0;

            entity.Print("���� ���ƿ´�. ��Ʈ������ Ǯ����.");
            entity.Print("ħ�뿡 ���� ���� �ܴ�.");
        }

        public override void Excute(Student entity)
        {
            entity.Print("zzZ zzZ zzZ");

            // �Ƿΰ� 0�� �ƴϸ�
            if(entity.Fatigue > 0)
            {
                // �Ƿ� 10�� ����
                entity.Fatigue -= 10;
                if (entity.Fatigue < 0) entity.Fatigue = 0;
            }
            // �Ƿΰ� 0�̸�
            else
            {
                // 'StudyHar'd ���·� ����
                entity.ChangeState(StudentStates.StudyHard);
            }
        }

        public override void Exit(Student entity)
        {
            entity.Print("�� ������ ������.");
        }

        public override bool OnMessage(Student entity, Telegram telegram)
        {
            return false;
        }
    }

    public class StudyHard : State<Student>
    {
        public override void Enter(Student entity)
        {
            // ��Ҹ� ���������� ����
            entity.CurrentLocation = Locations.Library;
            entity.Print("�����ϱ� ���� �������� �Դ�.");
        }

        public override void Excute(Student entity)
        {
            entity.Print("���������� ���θ� �Ѵ�.");

            entity.Knowledage++;
            entity.Stress++;
            entity.Fatigue++;

            if(entity.Knowledage >= 3 && entity.Knowledage <= 10)
            {
                int isExit = Random.Range(0, 2);
                if(isExit == 1 || entity.Knowledage == 10)
                {
                    // 'TakeAExam' ���·� ����
                    entity.ChangeState(StudentStates.TakeAExam);
                }
            }

            if(entity.Stress >= 20)
            {
                // 'PlayAGAme' ���·� ����
                entity.ChangeState(StudentStates.PlayAGame);
            }

            if(entity.Fatigue >= 50)
            {
                // 'RestAndSleep' ���·� ����
                entity.ChangeState(StudentStates.RestAndSleep);
            }
        }

        public override void Exit(Student entity)
        {
            entity.Print("�������� ������.");
        }

        public override bool OnMessage(Student entity, Telegram telegram)
        {
            return false;
        }
    }

    public class TakeAExam : State<Student>
    {
        public override void Enter(Student entity)
        {
            entity.CurrentLocation = Locations.LectureRoom;
            entity.Print("���ǽǿ� �� �������� �޴´�.");
        }

        public override void Excute(Student entity)
        {
            int examScore = 0;

            // ���� ��ġ�� 10�̸� ȹ�� ������ 10��
            if(entity.Knowledage == 10)
            {
                examScore = 10;
            }
            else
            {
                // randIndex�� ���� ��ġ���� ������ 6~10��, ������ 1~5��
                // ��, ������ ���� ���� ���� ������ ���� Ȯ���� ����.
                int randIndex = Random.Range(0, 10);
                examScore = randIndex < entity.Knowledage ? Random.Range(6, 11) : Random.Range(1, 6);
            }

            // ���� ���� ������ 0���� �ʱ�ȭ, �Ƿδ� 5~10 ����
            entity.Knowledage = 0;
            entity.Fatigue += Random.Range(5, 11);

            // ���迡�� ȹ���� ������ TotalScore�� �߰�, ��� ���
            entity.TotalScore += examScore;
            entity.Print($"���� ����({examScore}), ����({entity.TotalScore})");

            if(entity.TotalScore >= 100)
            {
                GameController.Stop(entity);
                return;
            }

            // ���� ������ ���� ���� �ൿ ����
            if(examScore <= 3)
            {
                // ������ 3�� �����̸� ��Ʈ������ 20 ����,
                // 'HitTheBottle' ���·� ����
                entity.Stress += 20;
                entity.ChangeState(StudentStates.HitTheBottle);
            }
            else if(examScore <= 7)
            {
                // 4~7�� �����̸� 'StudyHard' ���·� ����
                entity.ChangeState(StudentStates.StudyHard);
            }
            else
            {
                // 8�� �̻��̸� 'PlayAGame' ���·� ����
                entity.ChangeState(StudentStates.PlayAGame);
            }
        }

        public override void Exit(Student entity)
        {
            entity.Print("������ ��ġ�� ���ǽǿ��� ���´�.");
        }

        public override bool OnMessage(Student entity, Telegram telegram)
        {
            return false;
        }
    }

    public class PlayAGame : State<Student>
    {
        public override void Enter(Student entity)
        {
            // ��Ҹ� PC������ ����
            entity.CurrentLocation = Locations.PCRoom;
            entity.Print("PC������ ����.");
        }

        public override void Excute(Student entity)
        {
            entity.Print("������ ����.");

            // Ȯ���� ���� ��Ʈ������ ����/����
            int randState = Random.Range(0, 10);

            if(randState == 0 || randState == 9)
            {
                // 20% Ȯ���� ��Ʈ���� 20 ����, 'HitTheBottle' ���·� ����
                entity.Stress += 20;
                entity.ChangeState(StudentStates.HitTheBottle);
            }
            else
            {
                // 80% Ȯ���� ��Ʈ������ 1 �����ϰ� �Ƿΰ� 2�����Ѵ�.
                entity.Stress--;
                entity.Fatigue += 2;
            
                if(entity.Stress <= 0)
                {
                    // ��Ʈ������ 0�̵Ǹ� 'StudyHard' ���·� ����
                    entity.ChangeState(StudentStates.StudyHard);
                }
            }

        }

        public override void Exit(Student entity)
        {
            entity.Print("PC�濡�� ���´�.");
        }

        public override bool OnMessage(Student entity, Telegram telegram)
        {
            return false;
        }
    }

    public class HitTheBottle : State<Student>
    {
        public override void Enter(Student entity)
        {
            // ���� ��ġ�� �������� ����
            entity.CurrentLocation = Locations.Pub;
            entity.Print("�������� ����.");
        }

        public override void Excute(Student entity)
        {
            entity.Print("���� ���Ŵ�.");

            entity.Stress -= 5;
            entity.Fatigue += 5;

            if(entity.Stress == 0 || entity.Fatigue >= 50)
            {
                entity.ChangeState(StudentStates.RestAndSleep);
            }
        }

        public override void Exit(Student entity)
        {
            entity.Print("�������� ���´�.");
        }

        public override bool OnMessage(Student entity, Telegram telegram)
        {
            return false;
        }
    }

    public class GlobalMessageReceive : State<Student>
    {
        public override void Enter(Student entity)
        {
        }

        public override void Excute(Student entity)
        {
        }

        public override void Exit(Student entity)
        {
        }

        public override bool OnMessage(Student entity, Telegram telegram)
        {
            entity.Print($"Receive Message: sender({telegram.sender}), receiver({telegram.receiver}");

            if(telegram.message.Equals("GO_PCROOM"))
            {
                // �̹� PC���̶��
                if(entity.CurrentState == StudentStates.PlayAGame)
                {
                    // �̹� PC���̶�� "ALREADY_PCROOM" ����
                    entity.Print($"Send Message {telegram.receiver}���� {telegram.sender}�Կ��� ALREADY_PCROOM ����");
                    MessageDispatcher.Instance.DispatchMessage(0, telegram.receiver, telegram.sender, "ALREADY_PCROOM");
                }
                // PC���� �ƴ϶��
                else
                {
                    int stateIndex = Random.Range(0, 2);
                    if(stateIndex == 0)
                    {
                        // 50% Ȯ���� "GO_PCROOM" ����
                        entity.Print($"Send Message {telegram.receiver}���� {telegram.sender}�Կ��� GO_PCROOM ����");
                        MessageDispatcher.Instance.DispatchMessage(0, telegram.receiver, telegram.sender, "GO_PCROOM");

                        // 'PlayAGame' ���·� ����
                        entity.ChangeState(StudentStates.PlayAGame);
                    }
                    else
                    {
                        // 50% Ȯ���� "SORRY" ����
                        entity.Print($"Send Message {telegram.receiver}���� {telegram.sender}�Կ��� SORRY ����");
                        MessageDispatcher.Instance.DispatchMessage(0, telegram.receiver, telegram.sender, "SORRY");
                    }
                }

                return true;
            }

            return false;
        }
    }
}
