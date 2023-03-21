using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven.UnemployedOwnedStates
{
    public class RestAndSleep : State<Unemployed>
    {
        public override void Enter(Unemployed entity)
        {
            // 장소를 집으로 설정, 집에 오면 스트레스, 피로가 0이 된다.
            entity.CurrentLocation = Locations.SweetHome;

            entity.Stress = 0;
            entity.Fatigue = 0;

            entity.Print("집에서 들어와 침대에 눕는다.");
        }

        public override void Excute(Unemployed entity)
        {
            string state = Random.Range(0, 2) == 0 ? "zzZ zzZ zzZ" : "TV를 본다.";
            entity.Print(state);

            // 70% 확률로 지루함 1 증가, 30% 확룰로 지루함 1 감소
            entity.Boared += Random.Range(0, 100) < 70 ? 1 : -1;

            // 지루함이 4이상이면
            if (entity.Boared >= 4)
            {
                // "고박사" 에이전트에게 PC방에 같이 가자고 메시지 전송
                string receiver = "고박사";
                entity.Print($"Send Message: {entity.EntityName}님이 {receiver}님에게 GO_PCROOM 전송");
                MessageDispatcher.Instance.DispatchMessage(0, entity.EntityName, receiver, "GO_PCROOM");

                // 'PlayAGame" 상태로 변경
                entity.ChangeState(UnemployedStates.PlayAGame);
            }
        }

        public override void Exit(Unemployed entity)
        {
            entity.Print("집을 나간다.");
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
            // 장소를 PC방으로 설정
            entity.CurrentLocation = Locations.PCRoom;
            entity.Print("PC방으로 들어간다.");
        }

        public override void Excute(Unemployed entity)
        {
            entity.Print("게임을 즐긴다.");

            int randState = Random.Range(0, 10);
            if(randState == 0 || randState == 9)
            {
                // 20% 확률로 스트레스 20 증가, 'HitTheBottle' 상태로 변경
                entity.Stress += 20;
                entity.ChangeState(UnemployedStates.HitTheBottle);
            }
            else
            {
                // 80% 확률로 지루함 1 감소, 피곤함 2 증가
                entity.Boared--;
                entity.Fatigue += 2;

                if(entity.Boared <= 0 || entity.Fatigue >= 50)
                {
                    // 지루함이 0이하이거나 피곤함이 50이상이면 'RestAndSleep' 상태로 변경
                    entity.ChangeState(UnemployedStates.RestAndSleep);
                }
            }
        }

        public override void Exit(Unemployed entity)
        {
            entity.Print("PC방을 나온다.");
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
            // 장소를 술집으로 설정
            entity.CurrentLocation = Locations.Pub;
            entity.Print("술집으로 들어간다.");
        }

        public override void Excute(Unemployed entity)
        {
            // 50% 확률로 지루함 1 증가
            // 스트레스 4 감소
            // 피곤함 4 증가
            entity.Boared += Random.Range(0, 2) == 0 ? 1 : -1;
            entity.Stress -= 4;
            entity.Fatigue += 4;

            if(entity.Stress <= 0 || entity.Fatigue >= 50)
            {
                // 스트레스가 0 이하이거나 피곤함이 50 이상이면 'RestAndSleep' 상태로 변경
                entity.ChangeState(UnemployedStates.RestAndSleep);
            }
        }

        public override void Exit(Unemployed entity)
        {
            entity.Print("술집에서 나온다.");
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
            entity.Print("화장실에 들어간다.");
        }

        public override void Excute(Unemployed entity)
        {
            entity.Print("볼일을 본다.");

            // 바로 직전 상태로 되돌아간다.
            entity.RevertToPreviousState();
        }

        public override void Exit(Unemployed entity)
        {
            entity.Print("화장실에서 나간다.");
        }

        public override bool OnMessage(Unemployed entity, Telegram telegram)
        {
            return false;
        }
    }

    /// <summary>
    /// 전역 상태 클래스
    /// </summary>
    /// <remarks>
    /// 현재 상태와 별개로 실행되며, 화장실을 갈 것인지 결정한다.
    /// </remarks>
    public class StateGlobal : State<Unemployed>
    {
        public override void Enter(Unemployed entity)
        {
            
        }

        public override void Excute(Unemployed entity)
        {
            // 현재 상태가 'VisitBathroom' 이면 종료
            if (entity.CurrentState == UnemployedStates.VisitBathroom) return;

            // 10% 확률로 'VisitBathroom' 상태로 변경
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