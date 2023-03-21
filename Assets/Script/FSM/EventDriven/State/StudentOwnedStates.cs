using UnityEngine;

namespace WhalePark18.FSM.EventDriven.StudentOwnedStates
{
    public class RestAndSleep : State<Student>
    {
        public override void Enter(Student entity)
        {
            // 장소를 집으로 설정, 집에 오면 스트레스가 0이 된다.
            entity.CurrentLocation  = Locations.SweetHome;
            entity.Stress           = 0;

            entity.Print("집에 돌아온다. 스트레스가 풀린다.");
            entity.Print("침대에 누워 잠을 잔다.");
        }

        public override void Excute(Student entity)
        {
            entity.Print("zzZ zzZ zzZ");

            // 피로가 0이 아니면
            if(entity.Fatigue > 0)
            {
                // 피로 10씩 감소
                entity.Fatigue -= 10;
                if (entity.Fatigue < 0) entity.Fatigue = 0;
            }
            // 피로가 0이면
            else
            {
                // 'StudyHar'd 상태로 변경
                entity.ChangeState(StudentStates.StudyHard);
            }
        }

        public override void Exit(Student entity)
        {
            entity.Print("집 밖으로 나간다.");
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
            // 장소를 도서관으로 설정
            entity.CurrentLocation = Locations.Library;
            entity.Print("공부하기 위해 도서관에 왔다.");
        }

        public override void Excute(Student entity)
        {
            entity.Print("도서관에서 공부를 한다.");

            entity.Knowledage++;
            entity.Stress++;
            entity.Fatigue++;

            if(entity.Knowledage >= 3 && entity.Knowledage <= 10)
            {
                int isExit = Random.Range(0, 2);
                if(isExit == 1 || entity.Knowledage == 10)
                {
                    // 'TakeAExam' 상태로 변경
                    entity.ChangeState(StudentStates.TakeAExam);
                }
            }

            if(entity.Stress >= 20)
            {
                // 'PlayAGAme' 상태로 변경
                entity.ChangeState(StudentStates.PlayAGame);
            }

            if(entity.Fatigue >= 50)
            {
                // 'RestAndSleep' 상태로 변경
                entity.ChangeState(StudentStates.RestAndSleep);
            }
        }

        public override void Exit(Student entity)
        {
            entity.Print("도서관을 나간다.");
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
            entity.Print("강의실에 들어가 시험지를 받는다.");
        }

        public override void Excute(Student entity)
        {
            int examScore = 0;

            // 지식 수치가 10이면 획득 점수는 10점
            if(entity.Knowledage == 10)
            {
                examScore = 10;
            }
            else
            {
                // randIndex가 지식 수치보다 낮으면 6~10점, 높으면 1~5점
                // 즉, 지식이 높을 수록 높은 점수를 받을 확률이 높다.
                int randIndex = Random.Range(0, 10);
                examScore = randIndex < entity.Knowledage ? Random.Range(6, 11) : Random.Range(1, 6);
            }

            // 시험 직후 지식은 0으로 초기화, 피로는 5~10 증가
            entity.Knowledage = 0;
            entity.Fatigue += Random.Range(5, 11);

            // 시험에서 획득한 점수를 TotalScore에 추가, 결과 출력
            entity.TotalScore += examScore;
            entity.Print($"시험 성적({examScore}), 총점({entity.TotalScore})");

            if(entity.TotalScore >= 100)
            {
                GameController.Stop(entity);
                return;
            }

            // 시험 점수에 따라 다음 행동 설정
            if(examScore <= 3)
            {
                // 점수가 3점 이하이면 스트레스가 20 증가,
                // 'HitTheBottle' 상태로 변경
                entity.Stress += 20;
                entity.ChangeState(StudentStates.HitTheBottle);
            }
            else if(examScore <= 7)
            {
                // 4~7점 이하이면 'StudyHard' 상태로 변경
                entity.ChangeState(StudentStates.StudyHard);
            }
            else
            {
                // 8점 이상이면 'PlayAGame' 상태로 변경
                entity.ChangeState(StudentStates.PlayAGame);
            }
        }

        public override void Exit(Student entity)
        {
            entity.Print("시험을 마치고 강의실에서 나온다.");
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
            // 장소를 PC방으로 설정
            entity.CurrentLocation = Locations.PCRoom;
            entity.Print("PC방으로 들어간다.");
        }

        public override void Excute(Student entity)
        {
            entity.Print("게임을 즐긴다.");

            // 확률에 의해 스트레스가 증가/감소
            int randState = Random.Range(0, 10);

            if(randState == 0 || randState == 9)
            {
                // 20% 확률로 스트레스 20 증가, 'HitTheBottle' 상태로 변경
                entity.Stress += 20;
                entity.ChangeState(StudentStates.HitTheBottle);
            }
            else
            {
                // 80% 확률로 스트레스가 1 감소하고 피로가 2증가한다.
                entity.Stress--;
                entity.Fatigue += 2;
            
                if(entity.Stress <= 0)
                {
                    // 스트레스가 0이되면 'StudyHard' 상태로 변경
                    entity.ChangeState(StudentStates.StudyHard);
                }
            }

        }

        public override void Exit(Student entity)
        {
            entity.Print("PC방에서 나온다.");
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
            // 현재 위치를 술집으로 설정
            entity.CurrentLocation = Locations.Pub;
            entity.Print("술집으로 들어간다.");
        }

        public override void Excute(Student entity)
        {
            entity.Print("술을 마신다.");

            entity.Stress -= 5;
            entity.Fatigue += 5;

            if(entity.Stress == 0 || entity.Fatigue >= 50)
            {
                entity.ChangeState(StudentStates.RestAndSleep);
            }
        }

        public override void Exit(Student entity)
        {
            entity.Print("술집에서 나온다.");
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
                // 이미 PC방이라면
                if(entity.CurrentState == StudentStates.PlayAGame)
                {
                    // 이미 PC방이라면 "ALREADY_PCROOM" 답장
                    entity.Print($"Send Message {telegram.receiver}님이 {telegram.sender}님에게 ALREADY_PCROOM 전송");
                    MessageDispatcher.Instance.DispatchMessage(0, telegram.receiver, telegram.sender, "ALREADY_PCROOM");
                }
                // PC방이 아니라면
                else
                {
                    int stateIndex = Random.Range(0, 2);
                    if(stateIndex == 0)
                    {
                        // 50% 확률로 "GO_PCROOM" 답장
                        entity.Print($"Send Message {telegram.receiver}님이 {telegram.sender}님에게 GO_PCROOM 전송");
                        MessageDispatcher.Instance.DispatchMessage(0, telegram.receiver, telegram.sender, "GO_PCROOM");

                        // 'PlayAGame' 상태로 변경
                        entity.ChangeState(StudentStates.PlayAGame);
                    }
                    else
                    {
                        // 50% 확률로 "SORRY" 답장
                        entity.Print($"Send Message {telegram.receiver}님이 {telegram.sender}님에게 SORRY 전송");
                        MessageDispatcher.Instance.DispatchMessage(0, telegram.receiver, telegram.sender, "SORRY");
                    }
                }

                return true;
            }

            return false;
        }
    }
}
