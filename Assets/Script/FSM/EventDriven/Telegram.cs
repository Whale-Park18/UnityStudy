namespace WhalePark18.FSM.EventDriven
{
    /// <summary>
    /// �޽��� ������ �����ϴ� ����ü
    /// </summary>
    /// <remarks>
    /// * �� ���������� �ܼ��� ���ڿ��� �����ϴµ� �߰����� ������ �� ���� ���� �޽��� ��ü�� ������ ����ü, Ŭ������ ����
    /// * �޽����� � �ൿ�� ����, ���� ���� ��� ���� ���еȴٸ� �ൿ, ��� ��� ���� �����͸� ������ ������ �� ����
    /// </remarks>
    public struct Telegram
    {
        public float    dispatchTime;   // ���� �ð�
        public string   sender;         // �߽���
        public string   receiver;       // ������
        public string   message;        // �޽���

        public void SetTelegram(float time, string sender, string receiver, string message)
        {
            this.dispatchTime = time;
            this.sender = sender;
            this.receiver = receiver;
            this.message = message;
        }
    }
}