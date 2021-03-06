using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Network
{
    public class EquipLastWeaponPacket
    {
        public static void Initialize()
        {
            PacketHandlers.RegisterEncoded(0x1E, true, new OnEncodedPacketReceive(EquipLastWeaponRequest));
        }

        public static void EquipLastWeaponRequest(NetState state, IEntity e, EncodedReader reader)
        {
            PlayerMobile from = state.Mobile as PlayerMobile;

            if (from != null && from.Backpack != null)
            {
                BaseWeapon toEquip = from.LastWeapon;
                BaseWeapon toDisarm = from.FindItemOnLayer(Layer.OneHanded) as BaseWeapon;

                if (toDisarm == null)
                    toDisarm = from.FindItemOnLayer(Layer.TwoHanded) as BaseWeapon;

                if (toDisarm != null)
                {
                    from.Backpack.DropItem(toDisarm);
                }

                if (toEquip != toDisarm && toEquip != null && toEquip.Movable && toEquip.IsChildOf(from.Backpack))
                {
                    from.EquipItem(toEquip);
                }
            }
        }
    }
}