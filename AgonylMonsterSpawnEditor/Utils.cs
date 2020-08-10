using System;
using System.Collections.Generic;
using System.IO;

namespace AgonylMonsterSpawnEditor
{
    public static class Utils
    {
        public static Dictionary<uint, Monster> MonsterList;
        public static Dictionary<uint, Npc> NpcList;
        public static Dictionary<uint, Map> MapList;

        public static byte[] SkipAndTakeLinqShim(ref byte[] originalBytes, int take, int skip = 0)
        {
            if (skip + take > originalBytes.Length)
            {
                return new byte[] { };
            }

            var outByte = new byte[take];
            Array.Copy(originalBytes, skip, outByte, 0, take);
            return outByte;
        }

        public static ushort BytesToUInt16(byte[] bytes)
        {
            return BitConverter.ToUInt16(bytes, 0);
        }

        public static uint BytesToUInt32(byte[] bytes)
        {
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static string GetMyDirectory()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        public static void LoadMonsterData()
        {
            var file = GetMyDirectory() + Path.DirectorySeparatorChar + "MON.txt";
            var monsterDataFile = File.ReadAllText(file);
            var data = monsterDataFile.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            MonsterList = new Dictionary<uint, Monster>();
            for (var i = 1; i < Convert.ToInt32(data[0].Trim(';').Split('=')[1]); i++)
            {
                var currentLine = data[i].Split(',');
                if (currentLine.Length < 2)
                {
                    continue;
                }

                var id = Convert.ToUInt32(currentLine[0]);
                if (MonsterList.ContainsKey(id))
                {
                    continue;
                }

                var item = new Monster()
                {
                    Id = id,
                    Name = currentLine[1].Trim(),
                };
                MonsterList.Add(item.Id, item);
            }
        }

        public static void LoadNpcData()
        {
            var npcDataFile = File.ReadAllText(GetMyDirectory() + Path.DirectorySeparatorChar + "NPC.txt");
            var data = npcDataFile.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            NpcList = new Dictionary<uint, Npc>();
            for (var i = 1; i < Convert.ToInt32(data[0].Split('=')[1]); i++)
            {
                var currentLine = data[i].Trim(';').Split(',');
                if (currentLine.Length < 2)
                {
                    continue;
                }

                var id = Convert.ToUInt32(currentLine[0]);
                if (NpcList.ContainsKey(id))
                {
                    continue;
                }

                var item = new Npc()
                {
                    Id = id,
                    Name = currentLine[1].Trim(),
                };

                NpcList.Add(item.Id, item);
            }
        }

        public static void LoadMapData()
        {
            var mapDataFile = File.ReadAllText(GetMyDirectory() + Path.DirectorySeparatorChar + "MC.txt");
            var data = mapDataFile.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            MapList = new Dictionary<uint, Map>();
            for (var i = 1; i < Convert.ToInt32(data[0].Trim(';').Split('=')[1]); i++)
            {
                var currentLine = data[i].Split(',');
                if (currentLine.Length < 7)
                {
                    continue;
                }

                var id = Convert.ToUInt32(currentLine[0]);
                if (MapList.ContainsKey(id))
                {
                    continue;
                }

                var item = new Map()
                {
                    Id = id,
                    Name = currentLine[6].Trim(),
                };

                MapList.Add(item.Id, item);
            }
        }

        public static bool IsEmptyData(ushort data)
        {
            return data == 0 || data == 0xffff;
        }

        public static bool IsEmptyData(uint data)
        {
            return data == 0 || data == 0xffffffff;
        }

        public static bool IsEmptyData(string data)
        {
            return string.IsNullOrEmpty(data);
        }

        public static void ReplaceBytesAt(ref byte[] source, int startIndex, byte[] toReplace)
        {
            if (startIndex >= source.Length)
            {
                return;
            }

            for (var i = 0; i < toReplace.Length; i++)
            {
                if (startIndex >= source.Length)
                {
                    break;
                }

                source[startIndex] = toReplace[i];
                startIndex++;
            }
        }
    }
}
