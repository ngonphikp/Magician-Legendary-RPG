using UnityEngine;

public static class C_Util
{
    public static T Clone<T>(this T source)
    {
        var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(source);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serialized);
    }
    public static void getDumpObject(object obj)
    {
        Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
    }
}

