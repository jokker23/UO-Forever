#region References

using Server.Items;

#endregion

namespace Server.Mobiles
{
    [CorpseName("a snow elemental corpse")]
    public class SnowElemental : BaseCreature
    {
        public override string DefaultName { get { return "a snow elemental"; } }

        [Constructable]
        public SnowElemental() : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 163;
            BaseSoundID = 263;

            SetStr(326, 355);
            SetDex(166, 185);
            SetInt(71, 95);

            SetHits(196, 213);

            SetDamage(11, 17);

            Alignment = Alignment.Elemental;


            SetSkill(SkillName.MagicResist, 50.1, 65.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 80.1, 100.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 50;

            PackItem(new BlackPearl(3));
            Item ore = new IronOre(3);
            ore.ItemID = 0x19B8;
            PackItem(ore);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override bool BleedImmune { get { return true; } }
        public override int TreasureMapLevel { get { return Utility.RandomList(2, 3); } }
        public override int DefaultBloodHue { get { return -1; } }

        public SnowElemental(Serial serial) : base(serial)
        {}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override bool HasAura { get { return true; } }
    }
}