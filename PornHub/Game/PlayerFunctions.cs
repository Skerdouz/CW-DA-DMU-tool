using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PornHub.HyperMEM;
using static MDriver.MEME.Requests.Vector2;
using static MDriver.MEME.Requests.Vector3;
using static MDriver.MEME.Requests.Vector4;

namespace PornHub.Game
{
    class PlayerFunctions
    {

        public static string GetPlayerName(int ID)
        {
            //Si ça merde 2x, c'est le ASCII
            return MEM.ReadString(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Name), 15);
        }

        public static string GetPlayerGunID(int ID)
        {
            //Debug.WriteLine((Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_CurrentUsedWeaponID)).ToString("X"));
            return MEM.ReadInt16(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_CurrentUsedWeaponID)).ToString();
        }

        public static int GetPlayerShots(int ID)
        {
            return MEM.ReadInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_NumShots));
        }

        public static void SetPlayerShots(int ID, int Shots)
        {
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_NumShots), Shots);
        }

        public static int GetPlayerKills(int ID)
        {
            return MEM.ReadInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_NumKills));
        }

        public static void SetPlayerKills(int ID, Int32 kills)
        {
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_NumKills), kills);
        }

        public static void SetGodMode(int ID, bool GOD)
        {
            if(GOD)
            {
                MEM.WriteByte(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_GodMode), 0xA0);
                //Debug.WriteLine((Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_GodMode)).ToString("X"));
            }
            else
            {
                MEM.WriteByte(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_GodMode), 0x20);
            }            
        }

        public static void SetPoints(int ID, int Points)
        {
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Points), Points);
        }

        public static void SetRapidFire(int ID)
        {
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_RapidFire1), 0);
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_RapidFire2), 0);
        }

        public static void UnlimitedAmmo(int ID)
        {
            for (ulong i = 0; i < 5; i++)
            {
                MEM.WriteInt32((Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID)) + Offsets.PC_Ammo + (0x4 * i), 256);
            }
        }

        public static void SetAmmo(int ID, int ammount)
        {
            for (ulong i = 0; i < 2; i++)
            {
                MEM.WriteInt32((Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID)) + Offsets.PC_Ammo + (0x4 * i), ammount);
            }
        }

        public static void RemoveAmmo(int ID)
        {
            for (ulong i = 0; i < 5; i++)
            {
                MEM.WriteInt32((Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID)) + Offsets.PC_Ammo + (0x4 * i), 0);
            }
        }

        public static void SetPlayerSpeed(int ID, float Speed)
        {
            MEM.WriteFloat(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_RunSpeed), Speed);
        }

        public static void SetCriticalKills(int ID)
        {
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + 0x10CC), -1);
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + 0x10D0), -1);
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + 0x10E4), -1);
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + 0x10E8), -1);
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + 0x10C4), -1);
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + 0x10C8), -1);
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + 0x10D4), -1);
            MEM.WriteInt32(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + 0x10D8), -1);
        }

        public static void SendToJail(int ID)
        {
            MEM.WriteVector3f(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Vec3, new Vector3f(860f, 5515f, -1795f));
        }

        public static Vector3f[] Teleports = new Vector3f[] { new Vector3f(742.0477f, -413.0537f, -33.49968f), new Vector3f(32.41827f, 434.6326f, 1.125f), new Vector3f(-1054.955f, 322.5447f, -47.84928f), new Vector3f(1004.036f, -680.9578f, -255.875f), new Vector3f(-1348.923f, 1804.032f, -85.33665f), new Vector3f(-1805.112f, 269.1054f, -382.875f), new Vector3f(525.3798f, -78.01015f, -543.875f) };
        public static string[] TPS = new string[] { "Spawn", "Nacht", "Pond", "Power", "Facility", "Hanger", "PAP Machine" };
        public static int[] Camos = new int[] { 66, 67, 68, 69 };

        public static void Teleport(int ID, Vector3f Location)
        {
            MEM.WriteVector3f(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Vec3, Location);
        }

        public static Vector3f GetLocation(int ID)
        {
            return MEM.ReadVector3f(Offsets.PlayerCompPtr + (Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Vec3);
        }

        public static Vector2f GetPlayerAngles(int ID)
        {
            return MEM.ReadVector2f(Offsets.PlayerPedPtr + (Offsets.PP_ArraySize_Offset * (ulong)ID) + Offsets.PP_Heading_XY);
        }
        public static void SetZMPos(int ID)
        {
            var PlayerLcoation = MEM.ReadVector3f(Offsets.PlayerPedPtr + (Offsets.PP_ArraySize_Offset * (ulong)ID) + Offsets.PP_Coords);

            var HeadingXY = MEM.ReadVector2f(Offsets.PlayerPedPtr + (Offsets.PP_ArraySize_Offset * (ulong)ID) + Offsets.PP_Heading_XY);

            var Radians = (Math.PI / 180) * (HeadingXY.X);

            float MX = 150 * (float)Math.Cos(Radians);
            float MY = 150 * (float)Math.Sin(Radians);
            
            Threads.ZmLocation = new Vector3f(PlayerLcoation.X += MX, PlayerLcoation.Y += MY, PlayerLcoation.Z);            
        }
        public static void Move2Angle(int ID, Vector2f Angle, float Distance)
        {
            var PlayerLcoation = MEM.ReadVector3f(Offsets.PlayerPedPtr + (Offsets.PP_ArraySize_Offset * (ulong)ID) + Offsets.PP_Coords);
        
            var HeadingXY = Angle;
        
            var Radians = (Math.PI / 180) * (HeadingXY.Y);
        
            float MX = Distance * (float)Math.Cos(Radians);
            float MY = Distance * (float)Math.Sin(Radians);
        
            Teleport(ID, new Vector3f(PlayerLcoation.X += MX, PlayerLcoation.Y += MY, PlayerLcoation.Z -= ((Angle.X * 6) / (Form1.ThisForm.bar_noclip.Maximum - (Form1.ThisForm.bar_noclip.Value - 1)))));
        }



        public static string[] GunNames = new string[] { "" };
        public static int[] GunList = new int[] { 0 };

        public static string[] GunNamesZM = new string[] {  };
        public static int[] GunListZM = new int[] {  };
       
        public static string[] GunNamesMP = new string[] { };
        public static int[] GunListMP = new int[] { };

        public static void GiveWeapon0(int ID, int GunIndex)
        {
            MEM.WriteInt64(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + (Offsets.PC_SetWeaponID0 + (Offsets.PC_GunStruct * 0))), GunList[GunIndex]);
            MEM.WriteByte(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Camo0), 69); //Make Video
        }

        public static void GiveWeapon1(int ID, int GunIndex)
        {
            MEM.WriteInt64(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + (Offsets.PC_SetWeaponID0  + (Offsets.PC_GunStruct * 1))), GunList[GunIndex]);
            MEM.WriteByte(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Camo1), 118);
        }

        public static void GiveWeapon2(int ID, int GunIndex)
        {
            MEM.WriteInt64(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + (Offsets.PC_SetWeaponID0 + (Offsets.PC_GunStruct * 2))), GunList[GunIndex]);
            MEM.WriteByte(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + Offsets.PC_Camo2), 118);
        }

        public static void GiveWeapon3(int ID, int GunIndex)
        {
            MEM.WriteInt64(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + (Offsets.PC_SetWeaponID0 + (Offsets.PC_GunStruct * 3))), GunList[GunIndex]);
        }

        public static void GiveWeapon4(int ID, int GunIndex)
        {
            MEM.WriteInt64(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + (Offsets.PC_SetWeaponID0 + (Offsets.PC_GunStruct * 4))), GunList[GunIndex]);
        }

        public static void GiveWeapon5(int ID, int GunIndex)
        {
            MEM.WriteInt64(Offsets.PlayerCompPtr + ((Offsets.PC_ArraySize_Offset * (ulong)ID) + (Offsets.PC_SetWeaponID0 + (Offsets.PC_GunStruct * 5))), GunList[GunIndex]);
        }
    }
}
