﻿#region References
using System;

using Server.Factions;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.Seventh;
#endregion

namespace Server.Items
{
	public class ZombieSkinDye : Item
	{
		[Constructable]
		public ZombieSkinDye(int hue)
			: base(5163)
		{
			Name = "Zombieland Skin Dye";
			Hue = hue;
			Weight = 2.0;
			Stackable = false;
		}

        public ZombieSkinDye(Serial serial)
			: base(serial)
		{ }

		protected override void OnExpansionChanged(Expansion old)
		{
			base.OnExpansionChanged(old);

			if (EraML)
			{
				Stackable = true;
			}
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (IsChildOf(from.Backpack))
			{
				if (Sigil.ExistsOn(from))
				{
					from.SendLocalizedMessage(1010465); // You cannot disguise yourself while holding a sigil.
				}
				else if (!from.CanBeginAction(typeof(IncognitoSpell)))
				{
					from.SendLocalizedMessage(501698); // You cannot disguise yourself while incognitoed.
				}
				else if (!from.CanBeginAction(typeof(PolymorphSpell)))
				{
					from.SendLocalizedMessage(501699); // You cannot disguise yourself while polymorphed.
				}
				else if (TransformationSpellHelper.UnderTransformation(from))
				{
					from.SendLocalizedMessage(501699); // You cannot disguise yourself while polymorphed.
				}
				else if (from.IsBodyMod || from.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
				{
					from.SendLocalizedMessage(501605); // You are already disguised.
				}
				else
				{
					from.BodyMod = (from.Female ? 184 : 183);
					from.HueMod = Hue;

				    if (from is PlayerMobile && Hue == 61)
				    {
				        ((PlayerMobile) from).ZombiePaintExperationBooger = TimeSpan.FromDays(7.0);
				    }
				    else
				    {
                        ((PlayerMobile)from).ZombiePaintExperationVesper = TimeSpan.FromDays(7.0);			        
				    }

					from.SendMessage("You have painted yourself with zombie juices.  Yuck.");

					Consume();
				}
			}
			else
			{
				from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write(0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			reader.ReadInt();
		}
	}
}