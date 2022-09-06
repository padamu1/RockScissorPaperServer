using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Bean
{
    public class Define
    {
        public enum EVENT_CODE
        {
            LoginC = 0,             // 로그인 요청
            StartMatchingC = 1,     // 매칭 요청
            MatchingResponseC = 2,  // 매칭 응답
            MatchingCancelC = 3,    // 매칭 취소 요청
            
            LoginS = 0,             // 로그인 응답
            UserInfoS = 1,          // 유저 정보 내려줌
            StartMatchingS = 2,     // 매칭 시작 응답 -> 성공 / 실패
            MatchingSuccessS = 3,   // 매칭 성공시 클라이언트에 내려줌
            MatchingResponseS = 4,  // 매칭 수락 응답에 대한 답변 -> 성공 / 실패
            MatchingCancelS = 5,    // 매칭 취소 요청 응답
        }
        public enum WEB_SOCKET_STATE
        {
            None = 0,
            Connecting = 1,
            Open = 2,
            CloseSent = 3,
            CloseReceived = 4,
            Closed = 5,
            Aborted = 6
        }

        public enum PAYLOAD_DATA_TYPE
        {
            Unknown = -1,
            Continuation = 0,
            Text = 1,
            Binary = 2,
            ConnectionClose = 8,
            Ping = 9,
            Pong = 10
        }

        public enum MATCH_STATE
        {
            MATCH_START_WAIT = 0,       // 매칭 시작 대기중
            MATCH_START_SUCCESS = 1,    // 매칭 시작에 성공
            MATCH_START_FAILED = 2,     // 매칭 시작에 실패
        }
        public const int MATCH_SYSTEM_DELAY_TIME = 2000;
        public const int MATCH_USER_WAIT_TIME = 1000;
    }
}
