using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class C_LibSkill : MonoBehaviour
{
    // Math
    public static Vector3 DisX(Vector3 target, float disX, bool isRight = true)
    {
        float dis = (isRight) ? disX : -disX;
        Vector3 finish = target;
        finish += new Vector3(dis, 0, 0);
        return finish;
    }

    public static Vector3 DisABC(Vector3 A, Vector3 B, float radius)
    {
        float angle = Mathf.Atan2(A.y - B.y, A.x - B.x);

        float x = radius * Mathf.Cos(angle * 1.0f) + B.x;
        float y = radius * Mathf.Sin(angle * 1.0f) + B.y;

        //Debug.Log(angle + " & " + radius + " => " + x + ", " + y);
        return new Vector3(x, y, A.z);
    }

    public static Vector3 DisMid(float disX, bool isRight = true)
    {
        float dis = (isRight) ? disX : -disX;
        Vector3 finish = (isRight) ? PlayGame.instance.pointMR.position : PlayGame.instance.pointML.position;
        finish += new Vector3(dis, 0, 0);
        return finish;
    }

    public static Vector3 DisOffset(Vector3 target, Vector3 offset, bool isRight)
    {
        float  dis = (isRight) ? offset.x : -offset.x;
        Vector3 finish = target;
        finish += new Vector3(dis, offset.y, offset.z);
        return finish;
    }

    public static Vector3 GHero(List<C_Hero> targets, Transform thisTran)
    {
        Vector3 G = new Vector3();
        float Sx = 0.0f;
        float Sy = 0.0f;
        for (int i = 0; i < targets.Count; i++)
        {
            Sx += targets[i].transform.position.x;
            Sy += targets[i].transform.position.y;
        }
        G.x = Sx / targets.Count;
        G.y = Sy / targets.Count;
        G.z = thisTran.position.z;

        return G;
    }

    // Support
    public static List<C_Hero> FilterHeros(int[] arr, List<C_Hero> heros)
    {
        List<C_Hero> result = new List<C_Hero>();
        for (int i = 0; i < heros.Count; i++)
            if (Condition(heros[i].character.idx, arr)) 
                result.Add(heros[i]);
        return result;
    }

    private static bool Condition(int idx, int[] arr, int start = 0)
    {
        for (int i = start; i < arr.Length; i++)
        {
            if (idx == arr[i]) return true;
        }
        return false;
    }

    // Skill
    public static async void FxHitOne(List<C_Hero> targets, Transform parent = null, Vector3 pos = new Vector3(), GameObject fx = null, float timefx = 0.0f, bool isHit = true, float timeHit = 0.0f, int knock = 0, float timeKnDs = 0.0f, float timeKnDf = 0.0f, float timeKnDm = 0.0f, Vector3 offsetKn = new Vector3(), float radius = 0.0f)
    {
        // DeLay tạo fx
        await Task.Delay(TimeSpan.FromSeconds(timefx / GameManager.instance.myTimeScale));
        // Tạo fx        
        GameObject fxP = Instantiate(fx, parent);
        fxP.transform.position = pos;

        for (int i = 0; i < targets.Count; i++)
        {
            if (isHit) Hit(targets, i, timeHit);
            switch (knock)
            {
                case 1: Knock(targets, i, timeKnDs, timeKnDf, timeKnDm, offsetKn); break;
                case 2: Knock(targets, i, timeKnDs, timeKnDf, timeKnDm, pos, radius); break;
            }
        }
    }    

    public static async void FxHit(List<C_Hero> targets, GameObject fx = null, float timefx = 0.0f, bool isHit = true, float timehit = 0.0f, int knock = 0, float timeKnDs = 0.0f, float timeKnDf = 0.0f, float timeKnDm = 0.0f, Vector3 offsetKn = new Vector3(), float radius = 0.0f)
    {
        // DeLay tạo fx
        await Task.Delay(TimeSpan.FromSeconds(timefx / GameManager.instance.myTimeScale));
        for (int i = 0; i < targets.Count; i++)
        {
            // Tạo fx
            if(fx != null) Instantiate(fx, targets[i].gameObject.transform);
            if (isHit) Hit(targets, i, timehit);
            switch (knock)
            {
                case 1: Knock(targets, i, timeKnDs, timeKnDf, timeKnDm, offsetKn); break;
                case 2: Knock(targets, i, timeKnDs, timeKnDf, timeKnDm, targets[i].gameObject.transform.position, radius); break;
            }
        }
    }

    private static async void Hit(List<C_Hero> targets, int index = 0, float time = 0.0f)
    {
        // DeLay anim trúng đòn
        await Task.Delay(TimeSpan.FromSeconds(time / GameManager.instance.myTimeScale));
        targets[index].Beaten();
        Debug.Log("Hit: " + targets[index].gameObject.name);
    }

    private static void Knock(List<C_Hero> targets, int index = 0, float timeKnDs = 0.0f, float timeKnDf = 0.0f, float timeKnDm = 0.0f, Vector3 offsetKn = new Vector3())
    {
        Vector3 tmp = offsetKn;
        if (!(targets[index].character.team == 1)) tmp.x *= -1;
        Vector3 finish = tmp + targets[index].gameObject.transform.position;
        MoveTo(targets[index].gameObject.transform, finish, timeKnDs, timeKnDf, timeKnDm);
    }

    private static void Knock(List<C_Hero> targets, int index = 0, float timeKnDs = 0.0f, float timeKnDf = 0.0f, float timeKnDm = 0.0f, Vector3 pos = new Vector3(), float radius = 0.0f)
    {
        Vector3 finish = DisABC(targets[index].gameObject.transform.position, pos, radius);
        MoveTo(targets[index].gameObject.transform, finish, timeKnDs, timeKnDf, timeKnDm);
    }

    public static void Shoot(GameObject bullet, Transform parent, List<C_Hero> targets = null, bool isRotate = false, float timeInit = 0.0f, float timeDlMove = 0.0f, Vector3 offset = new Vector3())
    {
        for (int i = 0; i < targets.Count; i++)
        {
            CreateBullet(bullet, parent, targets[i], parent.position, targets[i].transform.position, isRotate, timeInit, timeDlMove, offset, targets);
        }
    }

    public static void Shoot(GameObject bullet, Transform parent, Vector3 position, List<C_Hero> targets = null, bool isRotate = false, float timeInit = 0.0f, float timeDlMove = 0.0f, Vector3 offset = new Vector3())
    {
        for (int i = 0; i < targets.Count; i++)
        {
            CreateBullet(bullet, parent, targets[i], position, targets[i].transform.position, isRotate, timeInit, timeDlMove, offset, targets);
        }
    }

    public static void ShootOne(GameObject bullet, Transform parent, C_Hero target,  Vector3 finish, float timeInit = 0.0f, float timeDlMove = 0.0f, bool isRotate = false, Vector3 offset = new Vector3(), List<C_Hero> targets = null)
    {
        CreateBullet(bullet, parent, target, parent.position, finish, isRotate, timeInit, timeDlMove, offset, targets);
    }

    public async static void CreateBullet(GameObject bullet, Transform parent, C_Hero target, Vector3 start, Vector3 finish, bool isRotate = false, float timeInit = 0.0f, float timeDlMove = 0.0f, Vector3 offset = new Vector3(), List<C_Hero> targets = null)
    {
        // Delay tạo đạn
        await Task.Delay(TimeSpan.FromSeconds(timeInit / GameManager.instance.myTimeScale));

        // Tạo viên đạn
        GameObject bl = Instantiate(bullet, parent); // Cha parent
        bl.transform.position = start; // Vị trí ban đầu

        // Lấy vị trí kết thúc        
        Vector3 offs = offset;
        if (!(target.character.team == 1))
            offs.x *= -1;
        Vector3 fsh = finish + offs;

        if (isRotate) // Bullet có định hướng ban đầu
        {
            Vector3 A = start;
            Vector3 B = fsh;
            float angle = Mathf.Rad2Deg * Mathf.Atan((B.y - A.y) / (B.x - A.x));
            if (!(target.character.team == 1))
                angle *= -1;
            bl.transform.Rotate(0, 0, angle);
        }

        C_Bullet c_bullet = bl.GetComponent<C_Bullet>();
        c_bullet.Move(parent, fsh, target, timeDlMove, targets);
    }

    public static async void ShootOneScale(GameObject bullet, Transform parent, C_Hero target, Vector3 finish, bool isRotate = false, float timeInit = 0.0f, Vector3 offset = new Vector3())
    {
        // Delay tạo bullet
        await Task.Delay(TimeSpan.FromSeconds(timeInit / GameManager.instance.myTimeScale));
        GameObject bulletObj = Instantiate(bullet, parent);

        Vector2 A = parent.position;
        Vector3 offs = offset;
        if (target.character.team == 0)
            offs.x *= -1;
        Vector2 B = finish + offs;

        if (isRotate) // Bullet có định hướng ban đầu
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan((B.y - A.y) / (B.x - A.x));

            if (target.character.team == 0)// Trái --> Phải
                angle *= -1;

            bulletObj.transform.Rotate(0, 0, angle);
        }

        C_Bullet c_bullet = bulletObj.GetComponent<C_Bullet>();
        c_bullet.ScaleX(A, B);
    }

    public static async void MoveTo(Transform thisTran, Vector3 finish, float timeds, float timedf, float timedm, bool isParabol = false, float height = 5.0f)
    {
        float t = 0.0f;

        // Delay start
        await Task.Delay(TimeSpan.FromSeconds(timeds / GameManager.instance.myTimeScale));

        Vector3 old = thisTran.position;
        Vector3 start;

        Vector3 dis = finish - old;
        float speed = dis.magnitude / timedm;
        while (true)
        {
            start = thisTran.position;

            if (isParabol)
            {
                t += Time.deltaTime * GameManager.instance.myTimeScale;
                thisTran.position = MathParabola.Parabola(start, finish, height, t / timedm);
            }
            else
            {
                thisTran.position = Vector3.MoveTowards(start, finish, Time.deltaTime * speed * GameManager.instance.myTimeScale);
            }

            if (Vector3.Distance(thisTran.position, finish) < 0.001f || t >= timedm)
                break; 
            await Task.Yield();
        }

        // Delay finish
        await Task.Delay(TimeSpan.FromSeconds(timedf / GameManager.instance.myTimeScale));
        // Reset RectTransform
        thisTran.GetComponent<RectTransform>().localPosition = new Vector3();
    }

    // FX UI

    public static async void DarkScreen(GameObject thisGameObj, float timed = 0.0f, float timea = 0.0f, List<C_Hero> targets = null)
    {
        await Task.Delay(TimeSpan.FromSeconds(timed / GameManager.instance.myTimeScale));
        //CanvasFx.instance.DarkScreen(timea);
        ShowObj(thisGameObj, timea);
        for (int i = 0; i < targets.Count; i++)
        {
            ShowObj(targets[i].gameObject, timea);
        }
    }

    private static async void ShowObj(GameObject gameObject, float time = 0.0f)
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        RectTransform rect = parent.GetComponent<RectTransform>();
        Vector3 pos = parent.GetComponent<RectTransform>().anchoredPosition3D;
        pos.z -= 11;
        rect.anchoredPosition3D = pos;
        await Task.Delay(TimeSpan.FromSeconds(time / GameManager.instance.myTimeScale));
        pos.z += 11;
        rect.anchoredPosition3D = pos;
    }

    public static void Vibrate(float time)
    {
        //CanvasFx.instance.VibrateScreen(time);
    }

    public static void Vibrate2(float time)
    {
        //CanvasFx.instance.VibrateScreen2(time);
    }
}
