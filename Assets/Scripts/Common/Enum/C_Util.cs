using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class C_Util
{
    /// <summary>
    /// Log Json của một class/obj nào đó
    /// </summary>
    /// <param name="obj"></param>
    public static void getDumpObject(object obj)
    {
        Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
    }
    public static String formatNumberDot(long pNumber)
    {
        var tempMoney = pNumber;
        string strMoney = tempMoney.ToString();
        bool isAm = pNumber >= 0 ? false : true;
        if (pNumber >= 10000)
        {
            int lengthMoney = strMoney.Length;

            int countDot = (lengthMoney - 1) / 3;

            string newStrMoney = "";
            while (countDot > 0)
            {
                newStrMoney = "." + strMoney.Substring(lengthMoney - 3, 3) + newStrMoney;
                countDot--;
                lengthMoney -= 3;
            }
            newStrMoney = strMoney.Substring(0, lengthMoney) + newStrMoney;
            if (isAm)
            {
                newStrMoney = "-" + newStrMoney;
            }
            return newStrMoney;
        }
        else
        {
            if (isAm)
            {
                strMoney = "-" + strMoney;
            }
            return strMoney;
        }
    }

    public static String formatMoneyByText(long pMoney)
    {
        string newString = "";
        long intOrigin = pMoney;
        long intExcess = 0;
        long level = 0;

        while (intOrigin >= 1000)
        {
            intExcess = intOrigin % 1000;
            intOrigin = intOrigin / 1000;
            level++;
        }
        newString += intOrigin.ToString();
        if (intExcess >= 10)
        {
            long tempExecess = intExcess / 10;
            string stringExecess = tempExecess.ToString();
            // cc.log(stringExecess);
            newString += "." + (stringExecess.Length == 1 ? "0" + stringExecess : stringExecess);
        }
        switch (level)
        {
            case 0:
                newString += "";
                break;
            case 1:
                newString += "K"; // 1000 +
                break;
            case 2:
                newString += "M"; // 1.000.000 +
                break;
            case 3:
                newString += "B"; // 1.000.000.000 +
                break;
            case 4:
                newString += "T"; // 1.000.000.000.000 +
                break;
            case 5:
                newString += "Q"; // 1.000.000.000.000.000 +
                break;
            default:
                newString += "Q+"; // đéo bao giờ =)))))
                break;

        }
        // cc.log(level);
        //cc.log(intExcess);
        return newString;
    }


    /**
        * @param pNumberDotFormat : number has number dot format
        * @param pReg: char used in function formatNumberDotByReg
        */
    public static Int32 reverseNumberDot(string pNumberDotFormat)
    {
        string strMoney = pNumberDotFormat.ToString();
        string[] arrMoney = strMoney.Split('.');

        string newMoney = "";
        for (int i = 0; i < arrMoney.Length; i++)
        {
            newMoney += arrMoney[i];
        }

        return Int32.Parse(newMoney);
    }

    public static bool validateNumber(string pNum)
    {
        Regex regex = new Regex(@"^[0-9\ ]+$");
        if (regex.IsMatch(pNum))
        {
            Debug.Log("regex good");
            return true;
        }
        Debug.Log("regex fail");
        return false;

    }

    public static bool validateNonSpecialChars(string val)
    {

        Regex regex = new Regex("^[0-9a-zA-Z]+$");
        if (regex.IsMatch(val))
        {
            Debug.Log("regex good");
            return true;
        }
        Debug.Log("regex fail");
        return false;

    }

    /*
        * @brief: kiem tra email
        * */
    public static bool validateEmail(string pString)
    {

        Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        if (regex.IsMatch(pString))
        {
            Debug.Log("regex good");
            return true;
        }
        Debug.Log("regex fail");
        return false;
    }

    public static String TimeStampRange(double timestamp, string dateString)
    {
        DateTime origin = new DateTime();
        return origin.AddSeconds(timestamp).ToString(dateString);
    }

    /// <summary>
    /// chuyen doi truc tiep tu timestamp sang datetime
    /// </summary>
    /// <param name="timestamp">int </param>
    /// <param name="dateString">"HH:mm:ss"</param>
    /// <returns></returns>
    public static String ConvertFromTimestamp(double timestamp, string dateString)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        return origin.AddSeconds(timestamp).ToString(dateString);
    }

    public static async void numTextEffect(Text textComponent, float numStart, float numEnd, float duration = 0.5f)
    {
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            textComponent.text = ((int)Mathf.Lerp(numStart, numEnd, progress)).ToString();
            await Task.Yield();
        }
        textComponent.text = numEnd.ToString();
    }
    /// <summary>
    ///maping các text có dạng : [0] .... [1] ..... với các param tương ứng 
    /// </summary>
    /// <param name="strReplace"></param>
    /// <param name="param"> mảng các param</param>
    /// <returns></returns>
    public static String ReplaceTempText(string strReplace, List<string> param)
    {
        string text = "";
        string tempDesc = strReplace;
        string pattern = @"\[+[\.\d\.]]";
        MatchCollection matches = Regex.Matches(tempDesc, pattern);
        string replaceStr = tempDesc;
        // Use foreach-loop.
        int i = 0;
        foreach (Match match in matches)
        {
            replaceStr = replaceStr.Replace(match.ToString(), param[i].ToString());
            i++;
        }
        text = replaceStr;

        return text;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timestamp"></param>
    /// <returns></returns>
    public static string CaculateRelativeTime(double timestamp)
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        var ts = new TimeSpan(DateTime.UtcNow.Ticks - origin.AddSeconds(timestamp).Ticks);
        double delta = Math.Abs(ts.TotalSeconds);

        if (delta < 1 * MINUTE)
            return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

        if (delta < 2 * MINUTE)
            return "a minute ago";

        if (delta < 45 * MINUTE)
            return ts.Minutes + " minutes ago";

        if (delta < 90 * MINUTE)
            return "an hour ago";

        if (delta < 24 * HOUR)
            return ts.Hours + " hours ago";

        if (delta < 48 * HOUR)
            return "yesterday";

        if (delta < 30 * DAY)
            return ts.Days + " days ago";

        if (delta < 12 * MONTH)
        {
            int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            return months <= 1 ? "one month ago" : months + " months ago";
        }
        else
        {
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }


    public static string GetNameMoneyByType(int type)
    {
        string nameMoney = "";
        foreach (string name in Enum.GetNames(typeof(C_Enum.MoneyType)))
        {
            int val = (int)Enum.Parse(typeof(C_Enum.MoneyType), name);
            if (type == val)
            {
                //nameMoney = JsonData_M.Instance.language[name].ToString();
                break;
            }
        }
        return nameMoney;
    }


    public int Equips_Power(int hp = 0, int strength = 0, int intelligence = 0,
        int armor = 0, int magicResistance = 0, int dexterity = 0, int agility = 0,
        int elusiveness = 0, int armorPenetration = 0, int magicPenetration = 0,
        int critBonus = 0, int crit = 0, int tenacity = 0)
    {
        int power = hp / 2 + strength + intelligence +
            (armor + magicResistance + dexterity + agility + elusiveness +
            armorPenetration + magicPenetration + crit + critBonus + tenacity) * 2;
        return power;
    }
}

