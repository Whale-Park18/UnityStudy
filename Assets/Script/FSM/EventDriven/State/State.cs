namespace WhalePark18.FSM.EventDriven
{
    public abstract class State<T> where T : BaseGameEntity
    {
        /// <summary>
        /// 해당 상태를 시작할 때 1회 호출
        /// </summary>
        /// <param name="entity">상태를 가진 에이전트</param>
        public abstract void Enter(T entity);

        /// <summary>
        /// 해당 상태를 업데이트할 떄 매 프레임 호출
        /// </summary>
        /// <param name="entity">상태를 가진 에이전트</param>
        public abstract void Excute(T entity);

        /// <summary>
        /// 해당 상태를 종료할 때 1회 호출
        /// </summary>
        /// <param name="entity">상태를 가진 에이전트</param>
        public abstract void Exit(T entity);

        /// <summary>
        /// 메시지를 받았을 때 1회 호출
        /// </summary>
        /// <param name="entity">상태를 가진 에이전트</param>
        /// <param name="telegram">메시지 정보</param>
        /// <returns></returns>
        public abstract bool OnMessage(T entity, Telegram telegram);
    }
}