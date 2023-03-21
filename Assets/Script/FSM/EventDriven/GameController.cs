using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven
{
    public enum Locations { SweetHome = 0, Library, LectureRoom, PCRoom, Pub };

    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private string[]    arrayStudents;      // Student���� �̸� �迭
        [SerializeField]
        private GameObject  studentPrefab;      // Student Ÿ���� ������

        [SerializeField]
        private string[]    arrayUnemployeds;  // Unemployed���� �̸� �迭
        [SerializeField]
        private GameObject  unemployedPrefab;   // Unemployed Ÿ�� ������

        private List<BaseGameEntity>  entitys;  // ��� ��� ���� ��� ������Ʈ ����Ʈ

        public static bool IsGameStop { set; get; } = false;

        private void Awake()
        {
            entitys = new List<BaseGameEntity>();

            // Student ������Ʈ
            for(int i = 0; i < arrayStudents.Length; i++)
            {
                // ������Ʈ ����, �ʱ�ȭ �޼ҵ� ȣ��
                GameObject  clone   = Instantiate(studentPrefab);
                Student     entity  = clone.GetComponent<Student>();
                entity.Setup(arrayStudents[i]);

                // ������Ʈ���� ��� ��� ���� ����Ʈ�� ����
                entitys.Add(entity);
            }

            // Unemployed ������Ʈ
            for (int i = 0; i < arrayUnemployeds.Length; i++)
            {
                // ������Ʈ ����, �ʱ�ȭ �޼ҵ� ȣ��
                GameObject clone    = Instantiate(unemployedPrefab);
                Unemployed entity   = clone.GetComponent<Unemployed>();
                entity.Setup(arrayUnemployeds[i]);

                // ������Ʈ���� ��� ��� ���� ����Ʈ�� ����
                entitys.Add(entity);
            }

            // ������ ������Ʈ���� EntityDataBase�� ���
            EntityDatabase.Instance.SetUp();
            for(int i = 0; i < entitys.Count; i++)
            {
                EntityDatabase.Instance.RegisterEntity(entitys[i]);
            }

            // �޽��� ������ �ʱ�ȭ
            MessageDispatcher.Instance.SetUp();
        }

        private void Update()
        {
            if (IsGameStop) return;

            // ���� �߼۵Ǿ�� �ϴ� �޽��� ���� �� ����
            MessageDispatcher.Instance.DispatchDelayedMessages();

            // ��� ������Ʈ�� Updated()�� ȣ���� ������Ʈ ����
            for(int i = 0; i < entitys.Count; i++)
            {
                entitys[i].Updated();
            }
        }

        public static void Stop(BaseGameEntity entity)
        {
            IsGameStop = true;
            entity.Print("totalScore 100�� ȹ������ ���α׷��� �����մϴ�.");
        }
    }
}