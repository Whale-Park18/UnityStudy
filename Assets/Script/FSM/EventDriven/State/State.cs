namespace WhalePark18.FSM.EventDriven
{
    public abstract class State<T> where T : BaseGameEntity
    {
        /// <summary>
        /// �ش� ���¸� ������ �� 1ȸ ȣ��
        /// </summary>
        /// <param name="entity">���¸� ���� ������Ʈ</param>
        public abstract void Enter(T entity);

        /// <summary>
        /// �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
        /// </summary>
        /// <param name="entity">���¸� ���� ������Ʈ</param>
        public abstract void Excute(T entity);

        /// <summary>
        /// �ش� ���¸� ������ �� 1ȸ ȣ��
        /// </summary>
        /// <param name="entity">���¸� ���� ������Ʈ</param>
        public abstract void Exit(T entity);

        /// <summary>
        /// �޽����� �޾��� �� 1ȸ ȣ��
        /// </summary>
        /// <param name="entity">���¸� ���� ������Ʈ</param>
        /// <param name="telegram">�޽��� ����</param>
        /// <returns></returns>
        public abstract bool OnMessage(T entity, Telegram telegram);
    }
}