using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven.UnemployedOwnedStates
{
    public class RestAndSleep : State<Unemployed>
    {
        public override void Enter(Unemployed entity)
        {
            // ��Ҹ� ������ ����, ���� ���� ��Ʈ����, �Ƿΰ� 0�� �ȴ�.
            entity.CurrentLocation = Locations.SweetHome;

            entity.Stress = 0;
            entity.Fatigue = 0;

            entity.Print("������ ���� ħ�뿡 ���´�.");
        }

        public override void Excute(Unemployed entity)
        {
            string state = Random.Range(0, 2) == 0 ? "zzZ zzZ zzZ" : "TV�� ����.";
            entity.Print(state);

            // 70% Ȯ���� ������ 1 ����, 30% Ȯ��� ������ 1 ����
            entity.Boared += Random.Range(0, 100) < 70 ? 1 : -1;

            // �������� 4�̻��̸�
            if (entity.Boared >= 4)
            {
                // "��ڻ�" ������Ʈ���� PC�濡 ���� ���ڰ� �޽��� ����
                string receiver = "��ڻ�";
                entity.Print($"Send Message: {entity.EntityName}���� {receiver}�Կ��� GO_PCROOM ����");
                MessageDispatcher.Instance.DispatchMessage(0, entity.EntityName, receiver, "GO_PCROOM");

                // 'PlayAGame" ���·� ����
                entity.ChangeState(UnemployedStates.PlayAGame);
            }
        }

        public override void Exit(Unemployed entity)
        {
            entity.Print("���� ������.");
        }

        public override bool OnMessage(Unemployed entity, Telegram telegram)
        {
            return false;
        }
    }

    public class PlayAGame : State<Unemployed>
    {
        public override void Enter(Unemployed entity)
        {
            // ��Ҹ� PC������ ����
            entity.CurrentLocation = Locations.PCRoom;
            entity.Print("PC������ ����.");
        }

        public override void Excute(Unemployed entity)
        {
            entity.Print("������ ����.");

            int randState = Random.Range(0, 10);
            if(randState == 0 || randState == 9)
            {
                // 20% Ȯ���� ��Ʈ���� 20 ����, 'HitTheBottle' ���·� ����
                entity.Stress += 20;
                entity.ChangeState(UnemployedStates.HitTheBottle);
            }
            else
            {
                // 80% Ȯ���� ������ 1 ����, �ǰ��� 2 ����
                entity.Boared--;
                entity.Fatigue += 2;

                if(entity.Boared <= 0 || entity.Fatigue >= 50)
                {
                    // �������� 0�����̰ų� �ǰ����� 50�̻��̸� 'RestAndSleep' ���·� ����
                    entity.ChangeState(UnemployedStates.RestAndSleep);
                }
            }
        }

        public override void Exit(Unemployed entity)
        {
            entity.Print("PC���� ���´�.");
        }

        public override bool OnMessage(Unemployed entity, Telegram telegram)
        {
            return false;
        }
    }

    public class HitTheBottle : State<Unemployed>
    {
        public override void Enter(Unemployed entity)
        {
            // ��Ҹ� �������� ����
            entity.CurrentLocation = Locations.Pub;
            entity.Print("�������� ����.");
        }

        public override void Excute(Unemployed entity)
        {
            // 50% Ȯ���� ������ 1 ����
            // ��Ʈ���� 4 ����
            // �ǰ��� 4 ����
            entity.Boared += Random.Range(0, 2) == 0 ? 1 : -1;
            entity.Stress -= 4;
            entity.Fatigue += 4;

            if(entity.Stress <= 0 || entity.Fatigue >= 50)
            {
                // ��Ʈ������ 0 �����̰ų� �ǰ����� 50 �̻��̸� 'RestAndSleep' ���·� ����
                entity.ChangeState(UnemployedStates.RestAndSleep);
            }
        }

        public override void Exit(Unemployed entity)
        {
            entity.Print("�������� ���´�.");
        }

        public override bool OnMessage(Unemployed entity, Telegram telegram)
        {
            return false;
        }
    }

    public class VisitBathroom : State<Unemployed>
    {
        public override void Enter(Unemployed entity)
        {
            entity.Print("ȭ��ǿ� ����.");
        }

        public override void Excute(Unemployed entity)
        {
            entity.Print("������ ����.");

            // �ٷ� ���� ���·� �ǵ��ư���.
            entity.RevertToPreviousState();
        }

        public override void Exit(Unemployed entity)
        {
            entity.Print("ȭ��ǿ��� ������.");
        }

        public override bool OnMessage(Unemployed entity, Telegram telegram)
        {
            return false;
        }
    }

    /// <summary>
    /// ���� ���� Ŭ����
    /// </summary>
    /// <remarks>
    /// ���� ���¿� ������ ����Ǹ�, ȭ����� �� ������ �����Ѵ�.
    /// </remarks>
    public class StateGlobal : State<Unemployed>
    {
        public override void Enter(Unemployed entity)
        {
            
        }

        public override void Excute(Unemployed entity)
        {
            // ���� ���°� 'VisitBathroom' �̸� ����
            if (entity.CurrentState == UnemployedStates.VisitBathroom) return;

            // 10% Ȯ���� 'VisitBathroom' ���·� ����
            int bathroomState = Random.Range(0, 100);
            if(bathroomState < 10)
            {
                entity.ChangeState(UnemployedStates.VisitBathroom);
            }
        }

        public override void Exit(Unemployed entity)
        {
            
        }

        public override bool OnMessage(Unemployed entity, Telegram telegram)
        {
            return false;
        }
    }
}