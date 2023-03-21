using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven
{
    public class EntityDatabase
    {
        private static readonly EntityDatabase instance = new EntityDatabase();
        public static           EntityDatabase Instance => instance;

        // ��� ������Ʈ�� ������ ����Ǵ� �ڷᱸ��
        // <������Ʈ �̸�, BaseGameEntity Ÿ���� ������Ʈ ����>
        private Dictionary<string, BaseGameEntity> entityDictionary;

        public void SetUp()
        {
            if(entityDictionary == null)
            {
                entityDictionary = new Dictionary<string, BaseGameEntity>();
            }
        }

        /// <summary>
        /// ������Ʈ DB�� ������Ʈ �߰��ϴ� �޼ҵ�
        /// </summary>
        /// <param name="newEntity"></param>
        public void RegisterEntity(BaseGameEntity newEntity)
        {
            entityDictionary.Add(newEntity.EntityName, newEntity);
        }

        /// <summary>
        /// ������Ʈ �̸��� �������� ������Ʈ ���� �˻�(BaseGameEntity)
        /// </summary>
        /// <param name="entityName">�˻��� ������Ʈ �̸�</param>
        /// <returns>�˻��� ������Ʈ</returns>
        public BaseGameEntity GetEntityFromID(string entityName)
        {
            foreach(KeyValuePair<string, BaseGameEntity> entity in entityDictionary)
            {
                if(entity.Key == entityName)
                {
                    return entity.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// ������Ʈ DB�� removeEntity ������ �����ϴ� �޼ҵ�
        /// </summary>
        /// <param name="removeEntity">������ ������Ʈ</param>
        public void RemoveEntity(BaseGameEntity removeEntity)
        {
            entityDictionary.Remove(removeEntity.EntityName);
        }
    }
}