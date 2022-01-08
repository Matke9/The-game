public class Unit
{
    public int minDamage;
    public int maxDamage;
    public int Hp;
    public int Armour;
    public int Speed;
    public bool Ranged;
    public string faction;
    public string[] Abilities;

    public Unit(int mind, int maxd, int hp, string fct, bool rngd, int armr, int sp, string[] ab)
    {
        this.minDamage = mind;
        this.maxDamage = maxd;
        this.Hp = hp;
        this.Armour = armr;
        this.Speed = sp;
        this.Ranged = rngd;
        this.faction = fct;
        this.Abilities = ab;
    }

}