#region References

using System.Collections.Generic;

#endregion

namespace Server.Mobiles
{
    [CorpseName("a succubus corpse")]
    public class Succubus : BaseCreature
    {
        public override string DefaultName { get { return "a succubus"; } }

        [Constructable]
        public Succubus() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Body = 149;
            BaseSoundID = 0x4B0;

            Alignment = Alignment.Demon;

            SetStr(488, 620);
            SetDex(121, 170);
            SetInt(498, 657);

            SetHits(312, 353);

            SetDamage(18, 28);


            SetSkill(SkillName.EvalInt, 90.1, 100.0);
            SetSkill(SkillName.Magery, 99.1, 100.0);
            SetSkill(SkillName.Meditation, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 100.5, 150.0);
            SetSkill(SkillName.Tactics, 80.1, 90.0);
            SetSkill(SkillName.Wrestling, 80.1, 90.0);

            Fame = 24000;
            Karma = -24000;

            VirtualArmor = 80;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);
            AddLoot(LootPack.MedScrolls, 2);
        }

        public override int Meat { get { return 1; } }
        public override int TreasureMapLevel { get { return 5; } }

        public void DrainLife()
        {
            var list = new List<Mobile>();

            foreach (Mobile m in GetMobilesInRange(2))
            {
                if (m == this || !CanBeHarmful(m))
                {
                    continue;
                }

                if (m is BaseCreature &&
                    (((BaseCreature) m).Controlled || ((BaseCreature) m).Summoned || ((BaseCreature) m).Team != Team))
                {
                    list.Add(m);
                }
                else if (m.Player)
                {
                    list.Add(m);
                }
            }

            foreach (Mobile m in list)
            {
                DoHarmful(m);

                m.FixedParticles(0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist);
                m.PlaySound(0x231);

                m.SendMessage("You feel the life drain out of you!");

                int toDrain = Utility.RandomMinMax(10, 40);

                Hits += toDrain;
                m.Damage(toDrain, this);
            }
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.1 >= Utility.RandomDouble())
            {
                DrainLife();
            }
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (0.1 >= Utility.RandomDouble())
            {
                DrainLife();
            }
        }

        public Succubus(Serial serial) : base(serial)
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
    }
}