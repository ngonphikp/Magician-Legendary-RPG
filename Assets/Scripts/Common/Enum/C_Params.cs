using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class C_Params
{
    public const string
        //IP_API = "192.168.1.75",
        IP_API = "159.65.143.31",
        PORT_API = "10800",
        ERROR_CODE = "ec",
        CMDID = "cmdid",
        UPGRADE = "upgrade",
        LEVEL = "level",
        BKLMLV = "bkLmLv",
        ACC = "acc",
        PR = "pr",
        CRITD = "critd",
        DODGE = "dodge",
        MP = "mp",
        MR = "mr",
        RESIST = "resist",
        HP = "hp",
        SPEED = "speed",
        AP = "ap",
        PD = "pd",
        CRIT = "crit",
        MD = "md",
        POWER = "power",
        DIFF = "diff",
        ID = "id",
        STAR = "star",
        HASHHERO = "hashHero",
        LISTHERO = "listHero",
        MAXBAGHERO = "maxBagHero",
        IDS = "id",
    #region SummonType
        KingdomSM = "K",
        ElementSM = "E",
    #endregion

    #region MoneyTypebyName



        CRYSTAL = "MON1000",
        GOLD = "MON1001",
        HEROIC_DEEDS = "MON1002",
        BREAKTHROUGH_MATERIALS = "MON1003",
        MANNUSCRIPT = "MON1004",
        HERO_RESIN = "MON1005",
        ELEMENT_BANNER = "MON1006",
        KINGDOM_BANNER = "MON1007",
        HERO_BANNER = "MON1008",
        FRIENDSHIP_BANNER = "MON1009",
        BONUS_BANNER = "MON1010",
        GLORY_POINT = "MON1011",
        ESSENCE = "MON1012",
        HONOR = "MON1013",
        ALLIANCE_COIN = "MON1014",
        RETIRE_COIN = "MON1015",
        HUNTER_COIN = "MON1016",
        BLESING_TICKET = "MON1017",
        SAGE_EXPERIENCE = "MON1018",
    #endregion

    #region ResourceType
        EXP = "EXP",
        MONEY = "MONEY",
        SPECIAL_ITEM = "SPECIAL_ITEM",
        HERO = "HERO",
        FRAGMENT_HERO = "FRAGMENT_HERO",
        WEAPON = "WEAPON",
        STONE = "GEM",
        GUILD = "GUI",
        VIP = "VIP",
        SUMMON = "SUM";
    #endregion
    public const int
        weapon = 0,
        necklace = 1,
        hat = 2,
        upperCloth = 3,
        glove = 4,
        lowerCloth = 5,
        belt = 6,
        shoe = 7,
        hp = 0,
        pd = 1,
        md = 2,
        acc = 3,
        pr = 4,
        mr = 5,
        speed = 6,
        crit = 7,
        critd = 8,
        ap = 9,
        mp = 10,
        resist = 11,
        dodge = 12,

        max_level_skill = 5;

    public static readonly Dictionary<int, string> AttrParams
   = new Dictionary<int, string>
   {
        {0, "HP"},
        {1, "STR"},
        {2, "INT"},
        {3, "ATK"},
        {4, "ARM" },
        {5, "MR" },
        {6, "DEF" },
        {7, "DEX" },
        {8, "AGI" },
        {9, "ELU" },
        {10, "APEN" },
        {11, "MPEN" },
        {12, "DPEN" },
        {13, "CRIT" },
        {14, "CRITBONUS" },
        {15, "TEN" },
   };
    public static readonly Dictionary<int, string> Represent
       = new Dictionary<int, string>
       {
        {0, "EXP"},
        {1, "MONEY"},
        {2, "SPECIAL_ITEM"},
        {3, "HERO"},
        {4, "FRAGMENT_HERO" },
        {5, "WEAPON" },
        {6, "STONE" },
        {7, "BANNER" },
        {8, "BORDER" },
        {9, "AVATAR" },
        {10, "KINGDOM" },
        {11, "ELEMENT" },
        {12, "MONEY_GUILD" }
       };


    public static readonly Dictionary<int, string> ChatType
      = new Dictionary<int, string>
      {
        {0, "Global Chat"},
        {1, "Chanel Chat"},
        {2, "Alliance Chat"},
        {3, "Private Chat"}

      };


    public static readonly Dictionary<int, string> Tier_Equips
       = new Dictionary<int, string>
       {
        {1, "Common"},
        {2, "Rare"},
        {3, "Elite"},
        {4, "Epic" },
        {5, "Legendary" },
        {6, "Fabled" },
        {7, "Mythic" },
        {8, "Ascended" },
        {9, "Heavenly" }
       };

    public static readonly Dictionary<string, M_Effect> Effect
        = new Dictionary<string, M_Effect>
        {
            {"SE001", new M_Effect("SE001", C_Enum.EffectCategory.HARD, "Stun") },                  //choáng
            {"SE002", new M_Effect("SE002", C_Enum.EffectCategory.HARD, "Rooted") },                //chói
            {"SE003", new M_Effect("SE003", C_Enum.EffectCategory.HARD, "Paralyzed") },             //tê liệt
            {"SE004", new M_Effect("SE004", C_Enum.EffectCategory.HARD, "Sleep") },                 //ngủ
            {"SE005", new M_Effect("SE005", C_Enum.EffectCategory.HARD, "Petrified") },             //hóa đá
            {"SE006", new M_Effect("SE006", C_Enum.EffectCategory.HARD, "Frozen") },               //đóng băng
            {"SE007", new M_Effect("SE007", C_Enum.EffectCategory.HARD, "Charmed") },               //quyến rũ
            {"SE008", new M_Effect("SE008", C_Enum.EffectCategory.HARD, "Confused") },              //hỗn loạn

            {"SE009", new M_Effect("SE009", C_Enum.EffectCategory.SOFT, "Silence") },               //câm lặng
            {"SE010", new M_Effect("SE010", C_Enum.EffectCategory.SOFT, "Disarm") },                //giải giới
            {"SE011", new M_Effect("SE011", C_Enum.EffectCategory.SOFT, "Cripple") },               //bị què
            {"SE012", new M_Effect("SE012", C_Enum.EffectCategory.SOFT, "Curse") },               //bị nguyền
            {"SE013", new M_Effect("SE013", C_Enum.EffectCategory.SOFT, "Blind") },               //bị mù
            {"SE014", new M_Effect("SE014", C_Enum.EffectCategory.SOFT, "Stat Buff") },           //tăng chỉ số
            {"SE015", new M_Effect("SE015", C_Enum.EffectCategory.SOFT, "Stat Debuff") },         //giảm chỉ số
            {"SE016", new M_Effect("SE016", C_Enum.EffectCategory.SOFT, "Poisoned") },            //độc
            {"SE017", new M_Effect("SE017", C_Enum.EffectCategory.SOFT, "Bleed") },               //chảy máu
            {"SE018", new M_Effect("SE018", C_Enum.EffectCategory.SOFT, "Leech Per Turn") },      //hút máu mỗi turn
            {"SE019", new M_Effect("SE019", C_Enum.EffectCategory.SOFT, "Burned") },              //Thiêu đốt
            {"SE020", new M_Effect("SE020", C_Enum.EffectCategory.SOFT, "Frostbitten") },         //Bỏng lạnh
            {"SE021", new M_Effect("SE021", C_Enum.EffectCategory.SOFT, "Infested") },            //Nhiễm khuẩn
            {"SE022", new M_Effect("SE022", C_Enum.EffectCategory.SOFT, "Sludged") },             //Bùn cát
            {"SE023", new M_Effect("SE023", C_Enum.EffectCategory.SOFT, "Magnetized") }           //Từ hóa
        };

    public static readonly Dictionary<string, string> Class_Equips
       = new Dictionary<string, string>
       {
            {"201", "Brute"},
            {"203", "Agile"},
            {"206", "Mage"}
       };

    public static readonly Dictionary<string, string> Weapon
        = new Dictionary<string, string>
        {
            {"1", "Sword"},
            {"2", "Dagger"},
            {"3", "Axe"},
            {"4", "Hammer" },
            {"5", "Mace" },
            {"6", "Claw" },
            {"7", "Longbow" },
            {"8", "Crossbow" },
            {"9", "Bomb" },
            {"10", "Staff" },
            {"11", "Book" },
            {"12", "Orb" }
       };
}
