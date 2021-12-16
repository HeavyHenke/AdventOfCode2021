namespace AoC2021;

public class Day16
{
    public object A()
    {
        var hex = "E20D72805F354AE298E2FCC5339218F90FE5F3A388BA60095005C3352CF7FBF27CD4B3DFEFC95354723006C401C8FD1A23280021D1763CC791006E25C198A6C01254BAECDED7A5A99CCD30C01499CFB948F857002BB9FCD68B3296AF23DD6BE4C600A4D3ED006AA200C4128E10FC0010C8A90462442A5006A7EB2429F8C502675D13700BE37CF623EB3449CAE732249279EFDED801E898A47BE8D23FBAC0805527F99849C57A5270C064C3ECF577F4940016A269007D3299D34E004DF298EC71ACE8DA7B77371003A76531F20020E5C4CC01192B3FE80293B7CD23ED55AA76F9A47DAAB6900503367D240522313ACB26B8801B64CDB1FB683A6E50E0049BE4F6588804459984E98F28D80253798DFDAF4FE712D679816401594EAA580232B19F20D92E7F3740D1003880C1B002DA1400B6028BD400F0023A9C00F50035C00C5002CC0096015B0C00B30025400D000C398025E2006BD800FC9197767C4026D78022000874298850C4401884F0E21EC9D256592007A2C013967C967B8C32BCBD558C013E005F27F53EB1CE25447700967EBB2D95BFAE8135A229AE4FFBB7F6BC6009D006A2200FC3387D128001088E91121F4DED58C025952E92549C3792730013ACC0198D709E349002171060DC613006E14C7789E4006C4139B7194609DE63FEEB78004DF299AD086777ECF2F311200FB7802919FACB38BAFCFD659C5D6E5766C40244E8024200EC618E11780010B83B09E1BCFC488C017E0036A184D0A4BB5CDD0127351F56F12530046C01784B3FF9C6DFB964EE793F5A703360055A4F71F12C70000EC67E74ED65DE44AA7338FC275649D7D40041E4DDA794C80265D00525D2E5D3E6F3F26300426B89D40094CCB448C8F0C017C00CC0401E82D1023E0803719E2342D9FB4E5A01300665C6A5502457C8037A93C63F6B4C8B40129DF7AC353EF2401CC6003932919B1CEE3F1089AB763D4B986E1008A7354936413916B9B080";
        var data = hex.SelectMany(ToBinary);
        var reader = new Reader(data);

        var rootPacket = new Packet(reader);

        int versionSum = 0;
        rootPacket.VisitAllPackets(p => versionSum += p.Version);

        return versionSum;
    }

    public object B()
    {
        var hex = "E20D72805F354AE298E2FCC5339218F90FE5F3A388BA60095005C3352CF7FBF27CD4B3DFEFC95354723006C401C8FD1A23280021D1763CC791006E25C198A6C01254BAECDED7A5A99CCD30C01499CFB948F857002BB9FCD68B3296AF23DD6BE4C600A4D3ED006AA200C4128E10FC0010C8A90462442A5006A7EB2429F8C502675D13700BE37CF623EB3449CAE732249279EFDED801E898A47BE8D23FBAC0805527F99849C57A5270C064C3ECF577F4940016A269007D3299D34E004DF298EC71ACE8DA7B77371003A76531F20020E5C4CC01192B3FE80293B7CD23ED55AA76F9A47DAAB6900503367D240522313ACB26B8801B64CDB1FB683A6E50E0049BE4F6588804459984E98F28D80253798DFDAF4FE712D679816401594EAA580232B19F20D92E7F3740D1003880C1B002DA1400B6028BD400F0023A9C00F50035C00C5002CC0096015B0C00B30025400D000C398025E2006BD800FC9197767C4026D78022000874298850C4401884F0E21EC9D256592007A2C013967C967B8C32BCBD558C013E005F27F53EB1CE25447700967EBB2D95BFAE8135A229AE4FFBB7F6BC6009D006A2200FC3387D128001088E91121F4DED58C025952E92549C3792730013ACC0198D709E349002171060DC613006E14C7789E4006C4139B7194609DE63FEEB78004DF299AD086777ECF2F311200FB7802919FACB38BAFCFD659C5D6E5766C40244E8024200EC618E11780010B83B09E1BCFC488C017E0036A184D0A4BB5CDD0127351F56F12530046C01784B3FF9C6DFB964EE793F5A703360055A4F71F12C70000EC67E74ED65DE44AA7338FC275649D7D40041E4DDA794C80265D00525D2E5D3E6F3F26300426B89D40094CCB448C8F0C017C00CC0401E82D1023E0803719E2342D9FB4E5A01300665C6A5502457C8037A93C63F6B4C8B40129DF7AC353EF2401CC6003932919B1CEE3F1089AB763D4B986E1008A7354936413916B9B080";
        var data = hex.SelectMany(ToBinary);
        var reader = new Reader(data);

        var rootPacket = new Packet(reader);
        return rootPacket.GetValue(); 
    }

    private static string ToBinary(char c)
    {
        if (char.IsDigit(c))
            return Convert.ToString(c - '0', 2).PadLeft(4, '0');
        return Convert.ToString(c - 'A' + 10, 2).PadLeft(4, '0');
    }
    
    class Reader
    {
        private readonly List<char> _data;
        public int Pos { get; private set; }

        public Reader(IEnumerable<char> data)
        {
            _data = data.ToList();
            Pos = 0;
        }

        public bool ReadBit()
        {
            return _data[Pos++] == '1';
        }
        
        public int Read(int numBits)
        {
            int result = 0;
            while (numBits > 0)
            {
                result <<= 1;
                if (ReadBit())
                    result |= 1;

                numBits--;
            }

            return result;

        }
        
        public long ReadLiteral()
        {
            long result = 0;
            bool isLast = false;
            while (isLast == false)
            {
                isLast = ReadBit() == false;

                result <<= 4;
                result |= (uint)Read(4);
            }

            return result;
        }

    }

    class Packet
    {
        public int Version;
        public int Type;

        public long LiteralValue;
        public List<Packet>? SubPackets;

        public Packet(Reader r)
        {
            Version = r.Read(3);
            Type = r.Read(3);

            if (Type == 4)
                LiteralValue = r.ReadLiteral();
            else
                SubPackets = ReadSubPackets(r);
        }

        public long GetValue()
        {
            if (Type == 4)
                return LiteralValue;
            if (Type == 0)
                return GetSubValues().Sum();
            if (Type == 1)
                return GetSubValues().Aggregate(1L, (a, b) => a * b);
            if (Type == 2)
                return GetSubValues().Min();
            if (Type == 3)
                return GetSubValues().Max();
            if (Type == 5)
                return SubPackets![0].GetValue() > SubPackets[1].GetValue() ? 1 : 0;
            if (Type == 6)
                return SubPackets![0].GetValue() < SubPackets[1].GetValue() ? 1 : 0;
            if (Type == 7)
                return SubPackets![0].GetValue() == SubPackets[1].GetValue() ? 1 : 0;
            throw new Exception("Unexpected type :" + Type);
        }

        private IEnumerable<long> GetSubValues()
        {
            return SubPackets!.Select(p => p.GetValue());
        }

        private static List<Packet> ReadSubPackets(Reader r)
        {
            var subPackets = new List<Packet>();

            var lengthTypeId = r.ReadBit();

            if (lengthTypeId == false)
            {
                int subPacketLength = r.Read(15);

                var endPos = r.Pos + subPacketLength;
                while (r.Pos < endPos)
                {
                    subPackets.Add(new Packet(r));
                }
            }
            else
            {
                var numSubPackets = r.Read(11);
                for (int i = 0; i < numSubPackets; i++)
                {
                    subPackets.Add(new Packet(r));
                }
            }

            return subPackets;
        }

        
        public void VisitAllPackets(Action<Packet> a)
        {
            a(this);
            if (SubPackets == null) return;
            foreach (var p in SubPackets)
                p.VisitAllPackets(a);
        }
    }
}