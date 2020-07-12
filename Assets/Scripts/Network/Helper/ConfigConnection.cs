using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigConnection 
{
    static readonly WriteOnce<int> tcpPort = new WriteOnce<int>();
    public static WriteOnce<int> TCPPort { get{ return tcpPort; } }
    static readonly WriteOnce<int> wsPort = new WriteOnce<int>();
    public static WriteOnce<int> WsPort { get { return wsPort; } }

    static readonly WriteOnce<string> zone = new WriteOnce<string>();
    public static WriteOnce<string> Zone { get { return zone; } }
    static readonly WriteOnce<string> host = new WriteOnce<string>();
    public static WriteOnce<string> Host { get { return host; } }


}
