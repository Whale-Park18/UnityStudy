namespace WhalePark18.FSM.EventDriven
{
    /// <summary>
    /// 메시지 정보를 저장하는 구조체
    /// </summary>
    /// <remarks>
    /// * 이 예제에서는 단순히 문자열만 전달하는데 추가적인 정보를 더 보낼 때는 메시지 자체를 별도의 구조체, 클래스로 구현
    /// * 메시지가 어떤 행동을 하자, 어디로 가자 등과 같이 구분된다면 행동, 장소 등과 같이 데이터를 나눠서 전달할 수 있음
    /// </remarks>
    public struct Telegram
    {
        public float    dispatchTime;   // 지연 시간
        public string   sender;         // 발신자
        public string   receiver;       // 수신자
        public string   message;        // 메시지

        public void SetTelegram(float time, string sender, string receiver, string message)
        {
            this.dispatchTime = time;
            this.sender = sender;
            this.receiver = receiver;
            this.message = message;
        }
    }
}