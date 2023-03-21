namespace WhalePark18.FSM.Generic
{
    public abstract class State<T> where T : class
    {
        /// <summary>
        /// �ش� ���¸� ������ �� 1ȸ ȣ��
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Enter(T entity);

        /// <summary>
        /// �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Excute(T entity);

        /// <summary>
        /// �ش� ���¸� ������ �� 1ȸ ȣ��
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Exit(T entity);
    }
}