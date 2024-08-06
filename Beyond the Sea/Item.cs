namespace Beyond_the_Sea
{
    internal class Items
    {
        // AVAILABLE ITEMS
        public Item apple = new()
        {
            name = "APPLE",
            icon = " ó ",
            description = "A delicious looking, fresh, red Apple. Just waiting to be eaten",
            level = 1,
            effectiveness = 5,
            type = Item.ItemTypes.FOOD,
            rarity = Item.Rarities.COMMON
        };


        // ITEM PARENT CLASS
        public class Item
        {
            public string name = "NULL";
            public string icon = "...";
            public string description = "[DISCRIPTION HERE]";
            public string[] bonuses = new string[3];

            public int level = 1;
            public int effectiveness = 1;
            public ItemTypes type = ItemTypes.FOOD;
            public Rarities rarity = Rarities.COMMON;

            public enum Rarities { COMMON, UNCOMMON, RAR, EPIC, MYTHIC, LEGENDARY, unobtainable }
            public enum ItemTypes { FOOD, ARMOUR, MELEEWEAPON, MAGICWEAPON, VALUEABLE }

            public void Use(Program.Player player)
            {
                switch (type)
                {
                    // Healing
                    case ItemTypes.FOOD:
                        player.health += effectiveness;
                        break;
                    // Defense
                    case ItemTypes.ARMOUR:

                        player.meleeDefense += effectiveness;
                        player.magicDefense += effectiveness;
                        break;
                    // Melee Damage
                    case ItemTypes.MELEEWEAPON:
                        player.meleeDamage += effectiveness;
                        break;
                    // Magic Damage
                    case ItemTypes.MAGICWEAPON:
                        player.magicDamage += effectiveness;
                        break;
                }
            }
        }
    }
}
