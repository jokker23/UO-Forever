using System;
using Server;

namespace Server.Items
{
	public class Boomstick : WildStaff
	{
		public override int LabelNumber{ get{ return 1075032; } } // Boomstick

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public Boomstick() : base()
		{
			Hue = 0x25;
		}

		public Boomstick( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}