using MySql.Data.MySqlClient;
using SimulFactory.Common.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulFactory.Core.Sql
{
    public class ShopItemTable
    {
        static readonly Lazy<ShopItemTable> instanceHolder = new Lazy<ShopItemTable>(() => new ShopItemTable());
        public static ShopItemTable GetInstance()
        {
            return instanceHolder.Value;
        }
        private Dictionary<int, ShopItemDto> shopItemDic = new Dictionary<int, ShopItemDto>();
        public bool LoadShopTable()
        {
            // 사용할 커넥션 가져오기
            using (MySqlConnection connection = SqlController.GetMySqlConnection())
            {
                string insertQuery = "Select * From shop_item";
                try //예외 처리
                {
                    // 커넥션 연결
                    connection.Open();

                    // 커맨드 설정
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    MySqlDataReader dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        ShopItemDto shopItemDto = new ShopItemDto();
                        shopItemDto.Uid = dr.GetInt32("uid");
                        shopItemDto.Name = dr.GetString("name");
                        shopItemDto.PriceType = dr.GetInt32("price_type");
                        shopItemDto.PriceValue = dr.GetInt32("price_value");
                        string rewardUids = dr.GetString("reward_uids");
                        string rewardAmounts = dr.GetString("reward_amounts");
                        shopItemDto.RewardUids = rewardUids == "" ? new int[0]:rewardUids.Split(',').Select(n=>int.Parse(n)).ToArray();
                        shopItemDto.RewardAmounts = rewardAmounts == "" ? new int[0] : rewardAmounts.Split(',').Select(n => int.Parse(n)).ToArray();
                        shopItemDic.Add(shopItemDto.Uid, shopItemDto);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }
        public ShopItemDto GetShopItemDto(int uid)
        {
            if(shopItemDic.ContainsKey(uid))
            {
                return shopItemDic[uid];
            }
            Console.WriteLine("ShopItem uid : {0}가 null임",uid);
            return null;
        }
    }
}
