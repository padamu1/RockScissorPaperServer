using SimulFactory.Common.Dto;
using SimulFactory.Common.Instance;
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
            
        }
        public void AddFriend(FriendDto friendDto)
        {
            if(friends.ContainsKey(friendDto.FirendName))
            {
                Console.WriteLine("{0} 에 이미 추가되어 있는 친구 : {1}",pc.GetUserData().UserName, friendDto.FirendName);
                return;
            }
            friends.Add(friendDto.FirendName, friendDto);
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
        public void AddFriendRequest(FriendRequestDto friendRequestDto)
        {
            if(friendRequests.ContainsKey(friendRequestDto.FirendName))
            {
                Console.WriteLine("{0} 에 이미 요청이 추가되어 있는 친구 : {1}",pc.GetUserData().UserName, friendRequestDto.FirendName);
                return;
            }
            friendRequests.Add(friendRequestDto.FirendName, friendRequestDto);
        }
        public void RemoveFriendRequest(string friendName)
        {
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
        /// <summary>
        /// 요청 수락 / 거절 (true / false)
        /// </summary>
        /// <param name="friendName"></param>
        /// <param name="result"></param>
        public void ReceiveRequest(string friendName, bool result)
        {
            if(result)
            {
                FriendRequestDto friendRequestDto = GetFriendRequestDto(friendName);
                if(friendRequestDto == null)
                {
                    // 없는 유저
                    return;
                }

                AddFriend(new FriendDto()
                {
                    FriendNo = friendRequestDto.FriendNo,
                    FirendName = friendRequestDto.FirendName,
                });

                RemoveFriendRequest(friendName);
            }
            else
            {
                RemoveFriendRequest(friendName);
            }
        }
    }
}
