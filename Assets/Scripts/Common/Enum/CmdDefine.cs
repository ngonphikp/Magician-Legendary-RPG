using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdDefine
{
    public const string
        ERROR_CODE = "ec",
        CMDID = "cmdid";
    public const int
        //___________________MODULE USER___________________
        REGISTER = 1001,
        LOGIN = 1002,
        PROFILE_INFO = 1003,
        UPDATE_DNAME = 1004,
        UPDATE_GENDER = 1005,
        UPDATE_LANGUAGE = 1006,
        UPDATE_STATUS = 1007,
        UPDATE_AVATAR = 1009,
        PUSH_NOTIFICATION = 1010,
        CHANGE_MONEY = 1011,
        GET_SERVER_LIST = 1012,
        GET_SETTING = 1013,
        GET_SERVER_INFO = 1014,
        GIFT_CODE = 1015,
        GET_CHANNEL_LIST = 1016,
        CHANGE_CHANNEL = 1017,
        LINK_ACCOUNT = 1019,
        SUPPORT = 1020,
        SWITCH_ACCOUNT = 1018,
        PING = 4005,
        //___________________MODULE HERO___________________
        HEROINFO = 16002,
        STARTLH = 16000,
        USERLH = 16001,
        UNPACKLH = 16010,
        UPCAPLH = 16009,
        GET_REWARD_STORY = 16011,
        UPLEVELHERO = 16003,
        EQUIP_ITEM = 16005,
        UNEQUIP_ITEM = 16007,
        quickEquipment = 16006,
        quickUnEquipment = 16008,
        SUMMON_HERO_INFO = 16013,
        SUMMON_HERO = 16014,
        BONUS_SUMMON = 16015,
        CHANGE_SUMMON = 16016,
        SPECIAL_SUMMON = 16017,
        //__________________ BATTLE SETUP  ________________
        listItem = 16004,
        LoadSceneResetHero = 16023,
        ResetHero = 16024,
        LoadSceneRetireHero = 16025,
        RetireHero = 16026,
        SwitchAutoRetireHero = 16027,
        //__________________ BATTLE SETUP  ________________
        BTSU_LOAD_HERO = 16018,
        BTSU_UPDATE_TEAM = 16019,

        //___________________MODULE BAG___________________
        ITEMINFO = 20000,
        UPGRADEITEMINFO = 20001,
        GETLISTGEMINBAG = 20002,
        ADDGEMINITEM = 20004,
        REMOVEGEMINITEM = 20005,
        quickUnEquipStone = 20008,
        UPGRADE_ITEM = 20003,
        INFOMONEY = 20009,
        LISTITEMBS = 20010,
        ARMORYITEM = 20011,
        GEMWORKINFO = 20012,
        GEMFISSION = 20013,
        GETLISTITEMSPECIAL = 20015,
        GITLISTPIECEINBAG = 20016,
        USEDITEMSPECIAL = 20018,
        GET_LIST_MONEY = 20020,
        //___________________MODULE CAMPAIGN___________________
        PLAY_IN_CAMPAIN = 22000,
        GET_CURRENT_STATION = 22001,
        //___________________MODULE FIGHTING___________________
        JOIN_GAME = 23000,
        MOVE_DOT_IN_FIGHTING = 23001,
        FLEE_FIGHTING = 23002,
        SAGE_SKILL_FIGHTING = 23003,
        HERO_SKILL_FIGHTING = 23004,
        CELESTIAL_SKILL_FIGHTING = 23005,
        Load_Scene_User_Mage = 24000,
        Equip_Stone_Mage = 24001,
        Get_Bage_Mage_Equipment = 24002,
        Equip_Mage_Equipment = 24003,
        Unequip_Mage_Equipment = 24004,
        Get_User_Mage_Skin = 24005,
        Equip_Mage_Skin = 24006,
        Sage_Get_Skill = 24007,
        Sage_Skill_Study = 24008,
        Sage_Skill_ResetAll = 24009,
        Sage_Skill_ResetLastColumn = 24010,
        //___________________MODULE GUILD___________________
        GET_LIST_GUILD = 25000,
        CREATE_GUILD = 25002,
        GET_COST_CREATE_GUILD = 25001,
        GET_GUILD_INFO = 25003,
        LEAVE_GUILD = 25004,
        GET_LOG_GUILD = 25005,
        CONTRIBUTE_GUILD = 25006,
        JOIN_GUILD = 25007,
        GO_TO_GUILD = 25010,
        SETTING_GUILD = 25011,
        CELESTIAL_GUILD = 25012,
        UNLOCK_CELESTIAL = 25013,
        GET_LIST_REQUEST_JOIN = 25014,
        REQUEST_ACCPET = 25008,
        CHANGE_OFFICE_GUILD = 25015,
        NOTI_USER_JOIN_GUILD = 25009,
        //___________________MODULE ADVENTURE___________________
        GET_AFK_TIME = 26000,
        ADVENTURE_INFO = 26001,
        GET_FAST_REWARD = 26002,
        FAST_REWARD_INFO = 26003,
        AFK_TIME_INFO = 26004,
        //___________________MODULE Celestial___________________
        GET_CELESTIAL_BEAST_INFO = 27000,
        GET_CELESTIAL_BEAST_LIST = 27001,
        CHANGE_CELESTIAL_BEAST = 27002,
        EQUIP_CELESTIAL_TABLET = 27003,
        GET_CELESTIAL_GEAR_BAG = 27004,
        EQUIP_CELESTIAL_GEAR = 27005,
        UNEQUIP_CELESTIAL_GEAR = 27006,
        //___________________MODULE Chat ___________________
        GET_MORE_MESSAGE = 31005,
        LOAD_SCENE_CHAT = 31000,
        SEND_MESSAGE_CHAT = 31004,
        RECEIVER_MESSAGE = 31003,
        LEAVE_SCENE_CHAT = 31001,
        NOTIFI_MESSAGE = 31002,
        //___________________MODULE MISSION_____________________

        GET_LIST_MISSION_INFO = 29001,
        GET_INFO_BOARDMISSION = 29000,
        REFRESH_MISSION_BOARD = 29002,
        ADD_MISSION = 29003,
        RECEVIE_MISSION = 29004,
        REVOKE_MISSION = 29005,
        DO_MISSION = 29006,

        //___________________MODULE HUNT_____________________
        LOAD_SCENE_HUNT = 32000,
        LEAVE_SCENE_HUNT = 32001,
        GET_HUNT_INFO = 32002,
        REFESH_HUNT = 32003,
        HUNT = 32004,
                    
        //___________________MODULE MAIL_____________________

        GET_LIST_MAIL = 40008,

        DELETE_ALL_MAIL = 40004,

        Mail_Collect_ALL = 40003,

        Mail_Collect_1 = 40002,

        Read_Mail = 40001,

        Mail_Get_New = 40009,
        //___________________IAP_____________________
        INFO_IAP_STORE = 30000,
        IAP_BUY = 30001,
        IAP_GET_REWARD = 30002,
    //__________________________ MODULE SHOP IN GAME   _________
        LIST_ITEMS_STORE = 50000,
        BUY_ITEM_STORE = 50001,
        REFRESH_STORE = 50002,
        //___________________ FRIEND _____________________
        GET_LIST_FRIEND = 61000,
        SEND_HEAD_TO_FRIEND = 61001,
        SAVE_HEAD_FROM_FRIEND = 61002,
        SEND_HEAD_ALL = 61003,
        GET_LIST_BLOCK_FRIEND = 61004,
        UNBLOCKED_FRIEND = 61005,
        BLOCK_FRIEND = 61006,
        DELETE_FRIEND = 61007,
        LIST_FRIEND_REQUEST = 61008,
        DELETE_ALL_FRIEND_REQUEST = 61009,
        DELETE_FRIEND_REQUEST = 61010,
        ACCEPT_FRIEND_REQUEST = 61011,
        ACCEPT_ALL_FRIEND_REQUEST = 61012,
        SEARCHING_FRIEND = 61013,
        ADD_FRIEND = 61014,
        FRIEND_INFO = 61005;


}
