using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crossdata;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

public class QuickFunction
{
    static Action action;
    static Dictionary<Action, double> actionCall = new Dictionary<Action, double>();
    // Start is called before the first frame update
    public static void removeAllChildren(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject.Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
    public static Sprite getAssetImages(string filePath)
    {
        try
        {
            Sprite image = Resources.Load<Sprite>(filePath);
            if (image == null)
            {
                image = StorageVarible.Sprites_Resoucres.LoadAsset<Sprite>("assets/resources/" + filePath + TypeFile.PNG);
            }
            return image;
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    public static TextAsset getAssetJsons(string filePath)
    {
        try
        {
            TextAsset file = Resources.Load<TextAsset>($"Text/{filePath}");
            if (file == null)
            {
                file = StorageVarible.Text_Resources.LoadAsset<TextAsset>("assets/resources/text/" + filePath + TypeFile.JSON);
            }
            return file;
        }
        catch (System.Exception)
        {
            return null;
        }

    }

    public static GameObject getAssetPref(string filePath)
    {
        try
        {
            GameObject file = Resources.Load<GameObject>(filePath);
            if (file == null)
            {
                file = StorageVarible.GameObject_Resources.LoadAsset<GameObject>("assets/resources/" + filePath + TypeFile.PREFAB);
            }
            return file;
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    public static void checkCall(Action call)
    {

        if (actionCall.ContainsKey(call))
        {
            if (actionCall[call] >= 9)
            {
                actionCall[call] = 0;
                Task.Factory.StartNew(() =>
                {
                    System.Timers.Timer timer = new System.Timers.Timer(50);
                    timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, call, timer);
                    timer.AutoReset = true;
                    timer.Enabled = true;
                });
                call();
            }else{                
                Debug.Log("time to call " + actionCall[call]);  
            }
        }
        else
        {
            double timeNum = new double();
            actionCall.Add(call, timeNum);
            Task.Factory.StartNew(() =>
            {
                System.Timers.Timer timer = new System.Timers.Timer(50);
                timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, call, timer);
                timer.AutoReset = true;
                timer.Enabled = true;
            });
            call();
        }
    }
    static void OnTimedEvent(System.Object source, System.Timers.ElapsedEventArgs e, Action call, System.Timers.Timer timer)
    {
        actionCall[call]++;
        if (actionCall[call] >= 10)
        {
            timer.Stop();
        }
    }
    public static AudioClip getSound(string filePath)
    {
        try
        {
            AudioClip file = Resources.Load<AudioClip>(filePath);
            if (file == null)
            {
                file = StorageVarible.Sound_Resources.LoadAsset<AudioClip>("assets/resources/" + filePath + TypeFile.Sound);
            }
            return file;
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    public static int GetIdEffectTeam(List<string> listElement)
    {
        int idEffect = -1;
        string[] arrElementID = listElement.ToArray();
        var query = arrElementID.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();
        //  query.getDumpObject();
        if (query.Count > 0)
        {
            int counter = query[0].Counter;
            if (counter == 5)
            {
                idEffect = 3;
            }
            else if (counter == 4)
            {
                idEffect = 2;
            }
            else if (counter == 3)
            {
                if (arrElementID.Length == 3)
                {
                    idEffect = 0;
                }
                else
                {
                    if (query.Count > 1)//! co phan tu duoc lap tiep theo
                    {
                        int countter2 = query[1].Counter;
                        idEffect = 1;
                    }
                    else
                    {
                        idEffect = 0;
                    }
                }

            }
            else
            {
                idEffect = -1;
            }


        }
        return idEffect;
    }
}
