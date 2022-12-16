using SimulFactory.Common.Bean;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Base;
using SimulFactory.Game.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaperServer.Game.Matching
{
    /// <summary>
    /// 유저 객체를 가지고 쓰레드를 돌다가 매칭기록에 이상한 유저가 발견되면 로그로 남겨주는 클래스
    /// </summary>
    public class AbusingCheckManager : ThreadBase
    {
        private Dictionary<long, Dictionary<long, int[]>> matchUsersLogs = new Dictionary<long, Dictionary<long, int[]>>();
        /// <summary>
        /// 각 유저 넘버에 해당 매칭에 참여했던 유저의 정보를 기록하는 메서드 - 1:1만 기록
        /// </summary>
        /// <param name="match"></param>
        public void AddUserLog(long winUserNo, long defeatUserNo)
        {
            lock(matchUsersLogs)
            {
                // 각 유저 로그 유/무 확인
                if (!matchUsersLogs.ContainsKey(winUserNo))
                {
                    matchUsersLogs.Add(winUserNo, new Dictionary<long, int[]>());
                }
                if (!matchUsersLogs.ContainsKey(defeatUserNo))
                {
                    matchUsersLogs.Add(defeatUserNo, new Dictionary<long, int[]>());
                }

                // 서로의 로그에 추가
                if (!matchUsersLogs[winUserNo].ContainsKey(defeatUserNo))
                {
                    matchUsersLogs[winUserNo].Add(defeatUserNo, new int[2]);
                }
                if (!matchUsersLogs[defeatUserNo].ContainsKey(winUserNo))
                {
                    matchUsersLogs[defeatUserNo].Add(winUserNo, new int[2]);
                }

                // 0은 승리 -> 내가 이김
                matchUsersLogs[winUserNo][defeatUserNo][0]++;
                // 1은 패배 -> 내가 짐
                matchUsersLogs[defeatUserNo][winUserNo][1]++;
            }
        }
        public void CheckAbusing()
        {
            lock(matchUsersLogs)
            {
                foreach(KeyValuePair<long, Dictionary<long, int[]>> user in matchUsersLogs)
                {
                    foreach(KeyValuePair<long, int[]> log in user.Value)
                    {
                        if (Math.Abs(log.Value[0] - log.Value[1]) >= Define.ABUSING_CHECK_COUNT)
                        {
                            Console.WriteLine("{0} 유저의 {1} 유저와의 어뷰징 의심 => 상대전적 승리 : {2}, 패배 : {3}", user.Key, log.Key, log.Value[0], log.Value[1]);
                        }
                    }
                }
            }
        }
    }
}
