namespace Beyond_the_Sea
{
    internal class Enemy
    {
        public string name = "ENEMY";
        public float health = 10;
        public float damage = 2;

        public int level = 1;

        public void SetValues(string name, float health, float damage, int level)
        {
            this.name = name; this.health = health; this.damage = damage; this.level = level;
        }

        public void Attack(Player player)
        {
            player.health -= damage;
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
        }
    }
}
