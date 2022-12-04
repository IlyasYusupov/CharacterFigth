using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterFigth
{
    public class Character /*: ICharacter*/
    {
        protected string name;
        protected int strength;
        protected int dexterity;
        protected int luck;
        protected int constitution;
        protected int intellect;
        protected int lvl;
        protected int xp;
        protected int skillPoints;

        protected int healthPoint;
        protected int physicalAttake;
        protected int magicalAttake;
        protected int manaPool;

        public Character(string Name, int strength, int dexterity, int constitution, int intellect, int healthPoint,
                        int physicalAttake, int magicalAttake, int manaPool, int XP, int LVL, int skillPoints, int luck)
        {
            this.name = Name;
            this.lvl = LVL;
            this.xp = XP;
            this.strength = strength;
            this.dexterity = dexterity;
            this.constitution = constitution;
            this.intellect = intellect;
            this.healthPoint = healthPoint;
            this.physicalAttake = physicalAttake;
            this.magicalAttake = magicalAttake;
            this.manaPool = manaPool;
            this.skillPoints = skillPoints;
            this.luck = luck;
        }

        //public void PhysicalAttack()
        //{

        //}

        //public void SelfHeal()
        //{
        //    if()
        //    {

        //    }
        //}


        public string Name { get => name; set => name = value; }

        public int LVL { get => lvl; set => lvl = value; }

        public int XP { get => xp; set => xp = value; }

        public int Strength { get => strength; set => strength = value; }

        public int Dexterity { get => dexterity; set => dexterity = value; }

        public int Luck { get => luck; set => luck = value; }

        public int Constitution { get => constitution; set => constitution = value; }

        public int Intellect { get => intellect; set => intellect = value; }

        public int HealthPoint { get => healthPoint; set => healthPoint = value; }

        public int PhysicalAttake { get => physicalAttake; set => physicalAttake = value; }

        public int MagicalAttake { get => magicalAttake; set => magicalAttake = value; }

        public int ManaPool { get => manaPool; set => manaPool = value; }

        public int SkillPoints { get => skillPoints; set => skillPoints = value; }

        public static Dictionary<string, int> CharacterParameters = new Dictionary<string, int>()
        {
            {"MinStrength", 1},
            {"MinDexterity", 1},
            {"MinConstitution", 1},
            {"MinIntellect", 1},
            {"MinLuck", 1}
        };

        public static Dictionary<int, int> LVLXP = new Dictionary<int, int>()
        {
            {2, 100},
            {3, 300},
            {4, 800},
            {5, 1800},
            {6, 3000}
        };
    }
}
