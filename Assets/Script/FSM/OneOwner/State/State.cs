namespace WhalePark18.FSM.OneOwner
{
    public abstract class State
    {
        /// <summary>
        /// �ش� ���¸� ������ �� 1ȸ ȣ��
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Enter(Student entity);

        /// <summary>
        /// �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Excute(Student entity);

        /// <summary>
        /// �ش� ���¸� ������ �� 1ȸ ȣ��
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Exit(Student entity);
    }
}