using Sfs2X.Core;
using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleUser
{
    public static void OnMessage(SFSObject sfsObject)
    {
        short cmdid = (short)sfsObject.GetInt(CmdDefine.CMDID);

        Debug.Log(sfsObject.GetDump());

        switch (cmdid)
        {
            case CmdDefine.GETINFO:
                handleGetInfo(sfsObject);
                break;
            case CmdDefine.SELECTION:
                handleSelection(sfsObject);
                break;
            case CmdDefine.ARRANGE:
                handleArrange(sfsObject);
                break;
            case CmdDefine.TAVERN:
                handleTavern(sfsObject);
                break;
            case CmdDefine.UPLEVEL:
                handleUpLevel(sfsObject);
                break;
            case CmdDefine.ENDGAME:
                handleEndGame(sfsObject);
                break;
            default:

                break;
        }
    }


    public static void handleEndGame(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE END GAME\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            FightingGame.instance.RecEndGame();
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void handleUpLevel(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE UP LEVEL\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            InforGame.instance.RecUpLevel();
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void handleGetInfo(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE GET INFO\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            List<M_Character> lstNhanVat = new List<M_Character>();
            ISFSArray nhanvats = packet.GetSFSArray("nhanvats");
            for(int i = 0; i < nhanvats.Size(); i++)
            {
                M_Character nhanVat = new M_Character(nhanvats.GetSFSObject(i), C_Enum.ReadType.SERVER);
                nhanVat.UpdateById();
                nhanVat.UpdateLevel();
                nhanVat.type = C_Enum.CharacterType.Hero;
                lstNhanVat.Add(nhanVat);
            }

            List<M_Milestone> tick_milestones = new List<M_Milestone>();
            ISFSArray milestones = packet.GetSFSArray("tick_milestones");
            for (int i = 0; i < milestones.Size(); i++)
            {
                tick_milestones.Add(new M_Milestone(milestones.GetSFSObject(i)));
            }

            tick_milestones.Add(new M_Milestone(milestones.Size(), 0));

            LoginGame.instance.RecInfo(lstNhanVat, tick_milestones);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void handleSelection(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE SELECTION\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            List<M_Character> lstNhanVat = new List<M_Character>();
            ISFSArray nhanvats = packet.GetSFSArray("nhanvats");
            for (int i = 0; i < nhanvats.Size(); i++)
            {
                M_Character nhanVat = new M_Character(nhanvats.GetSFSObject(i), C_Enum.ReadType.SERVER);
                nhanVat.UpdateById();
                nhanVat.UpdateLevel();
                nhanVat.type = C_Enum.CharacterType.Hero;
                lstNhanVat.Add(nhanVat);
            }

            SelectionGame.instance.RecSelection(lstNhanVat);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void handleArrange(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE ARRANGE\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            ArrangeGame.instance.RecArrange();
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }

    public static void handleTavern(SFSObject packet)
    {
        Debug.Log("=========================== HANDLE TAVERN\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            C_Enum.CardType type = (C_Enum.CardType)packet.GetInt("type_tavern");

            M_Character nhanvat = new M_Character(packet.GetSFSObject("nhanvat"), C_Enum.ReadType.SERVER);
            nhanvat.lv = 1;
            nhanvat.UpdateById();
            nhanvat.Current_ep = nhanvat.max_ep = 100;
            nhanvat.Current_hp = nhanvat.max_hp = nhanvat.hp;
            nhanvat.UpdateLevel();
            nhanvat.type = C_Enum.CharacterType.Hero;            
                                    
            TavernGame.instance.RecCard(type, nhanvat);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
