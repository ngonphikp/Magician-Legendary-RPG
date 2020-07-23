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
            default:

                break;
        }
    }

    public static void handleGetInfo(SFSObject packet)
    {
        Debug.Log("______________HANDLE GET INFO_____________\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            List<M_NhanVat> lstNhanVat = new List<M_NhanVat>();
            ISFSArray nhanvats = packet.GetSFSArray("nhanvats");
            for(int i = 0; i < nhanvats.Size(); i++)
            {         
                lstNhanVat.Add(new M_NhanVat(nhanvats.GetSFSObject(i)));
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
        Debug.Log("______________HANDLE SELECTION_____________\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            List<M_NhanVat> lstNhanVat = new List<M_NhanVat>();
            ISFSArray nhanvats = packet.GetSFSArray("nhanvats");
            for (int i = 0; i < nhanvats.Size(); i++)
            {
                lstNhanVat.Add(new M_NhanVat(nhanvats.GetSFSObject(i)));
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
        Debug.Log("______________HANDLE ARRANGE_____________\n" + packet.GetDump());
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
        Debug.Log("______________HANDLE TAVERN_____________\n" + packet.GetDump());
        short ec = packet.GetShort(CmdDefine.ERROR_CODE);
        if (ec == ErrorCode.SUCCESS)
        {
            C_Enum.CardType type = (C_Enum.CardType)packet.GetInt("type_tavern");

            M_NhanVat nhanvat = new M_NhanVat(packet.GetSFSObject("nhanvat"));
                                    
            TavernGame.instance.RecCard(type, nhanvat);
        }
        else
        {
            Debug.Log("ErrorCode: " + ec);
        }
    }
}
