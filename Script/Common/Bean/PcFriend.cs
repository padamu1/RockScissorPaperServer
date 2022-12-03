using SimulFactory.Common.Dto;
using SimulFactory.Common.Instance;
using SimulFactory.Core.Sql;
using SimulFactory.Game.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Common.Bean
{
    public class PcFriend
    {
        private Dictionary<string, FriendDto> friends;
        private Dictionary<string, FriendRequestDto> friendRequests;
        private PcInstance pc;
        public PcFriend(PcInstance pc)
        {
            this.pc = pc;
            friends = new Dictionary<string, FriendDto>();
            friendRequests = new Dictionary<string, FriendRequestDto>();
        }
        /// <summary>
        /// DB 로드
        /// </summary>
        public void LoadPcFriend()
        {
            PcFriendSql.LoadFriendData(pc,ref friends);
            PcFriendSql.LoadFriendRequestData(pc,ref friendRequests);
        }
        public void AddFriend(FriendDto friendDto)
        {
            if(friends.ContainsKey(friendDto.FriendName))
            {
                Console.WriteLine("{0} 에 이미 추가되어 있는 친구 : {1}",pc.GetUserData().UserName, friendDto.FriendName);
                return;
            }
            friends.Add(friendDto.FriendName, friendDto);
            pc.SendPacket(S_FriendData.Data(new List<FriendDto>() { friendDto }));
        }
        public void RemoveFriend(string friendName)
        {
            if(friends.ContainsKey(friendName))
            {
                friends.Remove(friendName);
            }
        }
        public FriendDto GetFriend(string friendName)
        {
            if(friends.ContainsKey(friendName))
            {
                return friends[friendName];
            }

            Console.WriteLine("{0} 가 {1} 의 친구가 아님", friendName, pc.GetUserData().UserName);
            return null;
        }
        public List<FriendDto> GetFriendList()
        {
            return friends.Values.ToList();
        }
        public void AddFriendRequest(FriendRequestDto friendRequestDto)
        {
            if(friendRequests.ContainsKey(friendRequestDto.FriendName))
            {
                Console.WriteLine("{0} 에 이미 요청이 추가되어 있는 친구 : {1}",pc.GetUserData().UserName, friendRequestDto.FriendName);
                return;
            }
            friendRequests.Add(friendRequestDto.FriendName, friendRequestDto);
            pc.SendPacket(S_FriendRequest.Data(pc, Define.FRIEND_RECEIVE_DATA_TYPE.Other, new List<FriendRequestDto>() { friendRequestDto }));
        }
        public void RemoveFriendRequest(string friendName)
        {
            PcFriendSql.DeleteFriendRequestData(pc, friendName);
            if(friendRequests.ContainsKey(friendName))
            {
                friendRequests.Remove(friendName);
            }
        }
        public FriendRequestDto GetFriendRequestDto(string friendName)
        {
            if(friendRequests.ContainsKey(friendName))
            {
                return friendRequests[friendName];
            }

            Console.WriteLine("{0} 가 {1} 에 요청하지 않음", friendName, pc.GetUserData().UserName);
            return null;
        }
        public List<FriendRequestDto> GetFriendRequestDtoList()
        {
            return friendRequests.Values.ToList();
        }
    }
}
