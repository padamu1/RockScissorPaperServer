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
            LoginC = 0,                     // 로그인 요청
            StartMatchingC = 1,             // 매칭 요청
            MatchingResponseC = 2,          // 매칭 응답
            MatchingCancelC = 3,            // 매칭 취소 요청
            PingC = 4,                      // 서버에 주기적으로 접속 확인을 위한 핑을 보냄
            UserBattleButtonClickedC = 5,   // 배틀 중 유저가 클릭한 버튼을 보냄
            UserNameC = 6,                  // 유저 닉네임 변경 요청
            LoginCompleteC = 7,             // 유저 로그인 완료 메시지

            LoginS = 0,             // 로그인 응답
            UserInfoS = 1,          // 유저 정보 내려줌
            StartMatchingS = 2,     // 매칭 시작 응답 -> 성공 / 실패
            MatchingSuccessS = 3,   // 매칭 성공시 클라이언트에 내려줌
            MatchingResponseS = 4,  // 매칭 수락 응답에 대한 답변 -> 성공 / 실패
            MatchingCancelS = 5,    // 매칭 취소 요청 응답
            MatchingResultS = 6,    // 매칭 결과 전송
            UserBattleResponseS = 7,// 상대편이 낸 결과를 받음
            RoundResultS = 8,       // 라운드 결과 전송
            UserNameS = 9,          // 유저 닉네임 변경 요청 응답
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

        public enum MATCH_READY_STATE
        {
            MATCH_START_BEFORE_WAIT = 0,        // 매칭 시작 메시지 보내기 전
            MATCH_START_WAIT = 1,               // 매칭 시작 대기중
            MATCH_START_SUCCESS = 2,            // 매칭 시작에 성공
            MATCH_START_FAILED = 3,             // 매칭 시작에 실패
        }
        public enum MATCH_STATE
        { 
            MATCH_READY = 0,
            MATCH_START = 1,
        }

        public enum ROCK_SCISSOR_PAPER
        {
            Break = -2,
            None = -1,
            Rock = 0,
            Scissor = 1,
            Paper = 2,
            Tie = 3,   // 무승부일때
        }

        public enum MATCH_TYPE
        {
            Normal = 0,
            Multi = 1,
            Card = 2,
        }

        public readonly static int NORMAL_MODE_SEARCH_USER_COUNT = 2;
        public readonly static int NORMAL_MODE_SEARCH_USER_MIN_COUNT = 2;
        public readonly static int MULTI_MODE_SEARCH_USER_COUNT = 4;
        public readonly static int MULTI_MODE_SEARCH_USER_MIN_COUNT = 2;
        public readonly static int MATCH_SYSTEM_DELAY_TIME = 2000;
        public readonly static int MATCH_USER_WAIT_TIME = 10000;
        public readonly static int MAX_ROUND_COUNT = 7;
        public readonly static int MAX_WIN_COUNT = 4;
        public readonly static int INIT_RATING = 1000;                 // 레이팅 초기값
        public readonly static int DEFAULT_SEARCH_RATING = 150;
        public readonly static int INCREASE_SEARCH_RATING = 150;
        public readonly static float K_FACTOR = 32;
        public readonly static int RANDOM_NICKNAME_START_LENGHT = 6;
        public readonly static int RANDOM_NICKNAME_END_LENGHT = 10; 
        public readonly static int MULTI_MATCH_WAIT_COUNT = 10;        // 멀티 매칭 최대 대기 카운트
        public readonly static string[] RANDOM_STRING = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "N", "M", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    }
}
