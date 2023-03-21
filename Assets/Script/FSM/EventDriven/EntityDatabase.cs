using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.FSM.EventDriven
{
    public class EntityDatabase
    {
        private static readonly EntityDatabase instance = new EntityDatabase();
        public static           EntityDatabase Instance => instance;

        // 모든 에이전트의 정보가 저장되는 자료구조
        // <에이전트 이름, BaseGameEntity 타입의 에이전트 정보>
        private Dictionary<string, BaseGameEntity> entityDictionary;

        public void SetUp()
        {
            if(entityDictionary == null)
            {
                entityDictionary = new Dictionary<string, BaseGameEntity>();
            }
        }

        /// <summary>
        /// 에이전트 DB에 에이전트 추가하는 메소드
        /// </summary>
        /// <param name="newEntity"></param>
        public void RegisterEntity(BaseGameEntity newEntity)
        {
            entityDictionary.Add(newEntity.EntityName, newEntity);
        }

        /// <summary>
        /// 에이전트 이름을 기준으로 에이전트 정보 검색(BaseGameEntity)
        /// </summary>
        /// <param name="entityName">검색할 에이전트 이름</param>
        /// <returns>검색된 에이전트</returns>
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
        /// 에이전트 DB에 removeEntity 정보를 삭제하는 메소드
        /// </summary>
        /// <param name="removeEntity">삭제할 에이전트</param>
        public void RemoveEntity(BaseGameEntity removeEntity)
        {
            entityDictionary.Remove(removeEntity.EntityName);
        }
    }
}