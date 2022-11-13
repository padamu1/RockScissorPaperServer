namespace SimulFactory.Common.Db
{
    public class ShopItemDto
    {
        public int Uid { get; set; }
        public string Name { get; set; }
        public int PriceType { get; set; }
        public int PriceValue { get; set; }
        public int[] RewardUids { get; set; }
        public int[] RewardAmounts { get; set; }
    }
}
