using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a hiryu corpse" )]
	public class LesserHiryu : BaseMount
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

		public override bool StatLossAfterTame { get { return true; } }

		private static int GetHue()
		{
			int rand = Utility.Random( 527 );

			/*

			500	527	No Hue Color	94.88%	0
			10	527	Green			1.90%	0x8295
			10	527	Green			1.90%	0x8163	(Very Close to Above Green)	//this one is an approximation
			5	527	Dark Green		0.95%	0x87D4
			1	527	Valorite		0.19%	0x88AB
			1	527	Midnight Blue	0.19%	0x8258

			 * */

			if( rand <= 0 )
				return 0x8258;
			else if( rand <= 1 )
				return 0x88AB;
			else if( rand <= 6 )
				return 0x87D4;
			else if( rand <= 16 )
				return 0x8163;
			else if( rand <= 26 )
				return 0x8295;


			return 0;
		}

		public override string DefaultName{ get{ return "a lesser hiryu"; } }

        public override int InternalItemItemID { get { return 0x3E94; } }

		[Constructable]
		public LesserHiryu()
			: base( 243, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Hue = GetHue();

			SetStr( 301, 410 );
			SetDex( 171, 270 );
			SetInt( 301, 325 );

			SetHits( 401, 600 );
			SetMana( 60 );

			SetDamage( 18, 23 );

			SetSkill( SkillName.Anatomy, 75.1, 80.0 );
			SetSkill( SkillName.MagicResist, 85.1, 100.0 );
			SetSkill( SkillName.Tactics, 100.1, 110.0 );
			SetSkill( SkillName.Wrestling, 100.1, 120.0 );

			Fame = 10000;
			Karma = -10000;

			Tamable = false;
			ControlSlots = 3;
			MinTameSkill = 98.7;
		}

		public override int GetAngerSound()
		{
			return 0x4FE;
		}

		public override int GetIdleSound()
		{
			return 0x4FD;
		}

		public override int GetAttackSound()
		{
			return 0x4FC;
		}

		public override int GetHurtSound()
		{
			return 0x4FF;
		}

		public override int GetDeathSound()
		{
			return 0x4FB;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Gems, 4 );
		}

		public override int TreasureMapLevel { get { return 3; } }
		public override int Meat { get { return 16; } }
		public override int Hides { get { return 60; } }
		public override FoodType FavoriteFood { get { return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }

		
		public LesserHiryu( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if( version == 0 )
				Timer.DelayCall( TimeSpan.Zero, delegate { Hue = GetHue(); } );

			if( version <= 1 )
				Timer.DelayCall( TimeSpan.Zero, delegate { if( InternalItem != null ) { InternalItem.Hue = this.Hue; } } );

			if( version < 2 )
			{
				for ( int i = 0; i < Skills.Length; ++i )
				{
					Skills[i].Cap = Math.Max( 100.0, Skills[i].Cap * 0.9 );

					if ( Skills[i].Base > Skills[i].Cap )
					{
						Skills[i].Base = Skills[i].Cap;
					}
				}
			}
		}
	}
}