using SimulFactory.Common.Bean;
using SimulFactory.Core;
using SimulFactory.Game;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Instance
{
    public class PcInstance
    {
        public static PcInstance? Instance = null;  // 유저별 하나씩 생성되는 객체
        private int _count = 0; // 지울꺼임
        private WebSocketController? controller;    // 정보 처리를 위한 객체
        public static PcInstance GetInstance()      // instance로 객체 선언
        {
            if (Instance == null)
                Instance = new PcInstance();
            return Instance;
        }
        private PcInstance()
        {
            UserData.GetInstance(); // 유저데이터 객체 처리
        }
        public void SetWebSocketController(WebSocketController controller)  // 정보 처리를 위한 컨트롤러 설정
        {
            this.controller = controller;
        }
        public WebSocketController GetWebSocketController()                 // 다른 클래스에서 컨트롤러 호출하기 위한 함수
        {
            return controller;
        }
        public void ProcessData(DataFormat dataFormat)                      // 클라이언트로부터 들어온 정보를 처리하기 위한 함수
        {
            byte evCode = dataFormat.evCode;
            Dictionary<byte, object> param = dataFormat.data;
            switch(evCode)
            {
                case (byte)Define.EVENT_CODE.LoginC:
                    Console.WriteLine(_count++);    // 지울꺼임
                    MatchSystem.GetInstance().AddPcInsatnce(this);  // 지울꺼임
                    C_Login.LoginC(param);
                    break;
                case (byte)Define.EVENT_CODE.StartMatchingC:
                    break;
                case (byte)Define.EVENT_CODE.MatchingResponseC:
                    break;
                case (byte)Define.EVENT_CODE.MatchingCancelC:
                    break;
            }
        }
    }
}
