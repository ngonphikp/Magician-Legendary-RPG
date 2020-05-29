using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Enum
{
    public enum status
    {
        no,
        library,
        user
    }
    public enum ScenceName
    {
        ChooseServer,
        LoginGame,
        HomeGame,
        ListHero,
        HeroInfo,
        HeroInfoP,
        HeroInfoN,
        Loading,
        Blacksmith,
        BlacksmithArmory,
        BlacksmithGemworks,
        Fighting,
        Alliance,
        FightingP,
        FightingP2,
        FightingP3,
        ChatScene,
        ShopInGame,
        RewardScene,
        BattleSetUp,
        BatttePickHero
    }
    public enum itemPosition
    {
        weapon = 0,
        necklace = 1,
        hat = 2,
        upperCloth = 3,
        glove = 4,
        lowerCloth = 5,
        belt = 6,
        shoe = 7
    }

    public enum typeItemPiece
    {
        R1 = 1,  // triệu hồi ngẫu nhiên tướng
        R2 = 2,  // triệu hồi ngẫu nhiên theo element
        R3 = 3  // triệu hồi theo chỉ định 
    }

    public enum MoneyType
    {
        CRYSTAL = 0,
        GOLD = 1,
        HEROIC_DEEDS = 2,
        BREAKTHROUGH_MATERIAL = 3,
        MANUSCRIPTS = 4,
        HERO_RESIN = 5,
        ELEMENT_BANNER = 6,
        KINGDOM_BANNER = 7,
        HERO_BANNER = 8,
        FRIENDSHIP_BANNER = 9
    }

    public static Dictionary<string, string> MoneyTypebyId = new Dictionary<string, string>{
        {"MON1000","Crystal"},
        {"MON1001","Gold"},
        {"MON1002","Heroic deeds"},
        {"MON1003","Breakthrough material"},
        {"MON1004","Manuscript"},
        {"MON1005","Hero resin"},
        {"MON1006","Element banner"},
        {"MON1007","Kingdom banner"},
        {"MON1008","Hero banner"},
        {"MON1009","Friendship banner"},
        {"MON1010","Bonus banner"},
        {"MON1011","Glory point"},
        {"MON1012","Essence"},
        {"MON1013","Honor"},
        {"MON1014","Alliance coin"},
        {"MON1015","Retire coin"},
        {"MON1016","Hunter coin"},
        {"MON1017","Blessing ticket"},
        {"MON1018","Sage experience"},
        {"G1000", "Celestial Soul"},
        {"G1001", "Celestial Book"},
    };

    public static Dictionary<string, string> ResourceType = new Dictionary<string, string>()
    {
        {"EXP","EXP"},
        {"MONEY","MON"},
        {"SPECIAL_ITEM","SPI"},
        {"HERO","T"},
        {"FRAGMENT_HERO","FRA"},
        {"WEAPON","EQU"},
        {"STONE","GEM"},
        {"GUILD","GUI"},
        {"VIP","VIP"},
        {"SUMMON","SUM"},
    };
    public enum PlayType
    {
        PvM = 0,
        PvP = 1,
        PvB = 2
    }

    public enum CharacterType
    {
        Hero = 0,
        Creep = 1,
        MBoss = 2,
        Boss = 3,
        Sage = 4,
        Cel = 5
    }

    public enum SageType
    {
        Ultimate,
        Passive,
        Active
    }

    public enum idAction
    {
        SKILLING = 0,
        BEATEN = 1,
        APPLY_EFFECT = 2,
        REMOVED_EFFECT = 3,
        DODGE = 4,

        DIE = 6,
        HEALTH_CHANGE = 7,
        ENERGY_CHANGE = 8,
    }

    public enum EffectCategory
    {
        HARD,
        SOFT,
    }

    public enum TypeText
    {
        HP1, // Tăng HP
        HP2, // Giảm HP
        EP1, // Tăng EP
        EP2, // Giảm EP
        DG,  // Tránh né
        Miss,// Đánh trượt
    }

    public enum Represent
    {
        NULL = -1,
        EXP = 0,
        MONEY = 1,
        SPECIAL_ITEM = 2,
        HERO = 3,
        FRAGMENT_HERO = 4,
        WEAPON = 5,
        STONE = 6,
        BANNER = 7,
        BORDER = 8,
        AVATAR = 9,
        KINGDOM = 10,
        ELEMENT = 11,
        MONEY_GUILD = 12
    }

    public enum SexType
    {
        MALE = 0,
        FEMALE = 1
    }

    public enum MONEY_GUILD_TYPE
    {
        CELESTIAL_SOUL = 0,
        CELESTIAL_BOOK = 1
    }


    public enum GuildSetting
    {
        NOTICE = 0,
        LEVEL_REQUEST = 1,
        LANGUAGE = 2,
        VERIFICATION = 3,
        GUILD_MASTER = 4,
        GUILD_VICEROY = 5,
        GUILD_LEADER = 6
    }

    public enum Verification_Guild
    {
        ANY_ONE_MAY_JOIN = 0,
        REQUIRES_APPROVAL = 1
    }

    public enum ChatType
    {
        GLOBAL = 0,
        CHANNEL = 1,
        GUILD = 2,
        PRIVATE = 3
    }


    public enum TYPE_STORE
    {
        GENERAL = 0,
        ALLIANCE = 1,
        MEMOIR = 2,
        HUNTER = 3
    }


    public enum ETeamType
    {
        CAMPAIGN = 0,
        TOWER = 1,
        MONTER_HUNT = 2,
        MISSION_OUTPOST = 3


    }

    public enum TypeResult
    {
        WIN = 0,
        LOSE = 1,
        TIE = 2
    }
}
