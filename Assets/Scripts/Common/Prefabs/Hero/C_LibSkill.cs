using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class C_LibSkill : MonoBehaviour
{
    // Math

    // Khoảng cách so với trục x
    public static Vector3 DisX(Vector3 target, float disX, bool isRight = true)
    {
        float dis = (isRight) ? disX : -disX;
        Vector3 finish = target;
        finish += new Vector3(dis, 0, 0);
        return finish;
    }

    // Khoảng cách đường thẳng
    public static Vector3 DisABC(Vector3 A, Vector3 B, float radius)
    {
        float a = Mathf.Atan2(A.y - B.y, A.x - B.x);
        float b = Mathf.Atan2(A.x - B.x, A.y - B.y);
        float c = Mathf.Atan2(A.x - B.x, A.z - B.z);

        float x = radius * Mathf.Cos(a * 1.0f) + B.x;
        float y = radius * Mathf.Cos(b * 1.0f) + B.y;
        float z = radius * Mathf.Cos(c * 1.0f) + B.z;

        return new Vector3(x, y, z);
    }

    // Khoảng cách 3 trục
    public static Vector3 DisOffset(Vector3 target, Vector3 offset, bool isRight)
    {
        float dis = (isRight) ? offset.x : -offset.x;
        Vector3 finish = target;
        finish += new Vector3(dis, offset.y, offset.z);
        return finish;
    }

    // Tính tâm
    public static Vector3 GHero(List<C_Character> targets)
    {
        Vector3 G = new Vector3();
        float Sx = 0.0f;
        float Sy = 0.0f;
        float Sz = 0.0f;
        for (int i = 0; i < targets.Count; i++)
        {
            Sx += targets[i].transform.position.x;
            Sy += targets[i].transform.position.y;
            Sz += targets[i].transform.position.z;
        }
        G.x = Sx / targets.Count;
        G.y = Sy / targets.Count;
        G.z = Sz / targets.Count;

        return G;
    }

    // Skill
    public static async void FxHit(List<C_Character> targets, GameObject fx = null, float timefx = 0.0f, bool isHit = true, float timehit = 0.0f)
    {
        // DeLay tạo fx
        await Task.Delay(TimeSpan.FromSeconds(timefx / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
        for (int i = 0; i < targets.Count; i++)
        {
            CreateFx(targets[i], fx);
            Hit(targets, i, timehit, isHit);
        }
    }

    public static void CreateFx(C_Character target, GameObject fx = null, bool isChangePos = false, Vector3 pos = new Vector3())
    {
        if (fx != null)
        {
            GameObject Obj = Instantiate(fx, target.gameObject.transform);
            if (isChangePos) Obj.transform.position = pos;
        }
    }

    private static async void Hit(List<C_Character> targets, int index = 0, float time = 0.0f, bool isHit = true)
    {
        // DeLay anim trúng đòn
        await Task.Delay(TimeSpan.FromSeconds(time / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
        targets[index].Beaten();
        //Debug.Log("===========================Hit: " + targets[index].nhanvat.id_nv);
    }

    public static void Shoot(GameObject bullet, Transform parent, List<C_Character> targets = null, bool isRotate = false, float timeInit = 0.0f, float timeDlMove = 0.0f, Vector3 offset = new Vector3())
    {
        for (int i = 0; i < targets.Count; i++)
        {
            CreateBullet(bullet, parent, targets[i], parent.position, targets[i].transform.position, isRotate, timeInit, timeDlMove, offset, targets);
        }
    }

    public async static void CreateBullet(GameObject bullet, Transform parent, C_Character target, Vector3 start, Vector3 finish, bool isRotate = false, float timeInit = 0.0f, float timeDlMove = 0.0f, Vector3 offset = new Vector3(), List<C_Character> targets = null)
    {
        // Delay tạo đạn
        await Task.Delay(TimeSpan.FromSeconds(timeInit / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));

        // Tạo viên đạn
        GameObject bl = Instantiate(bullet, parent); // Cha parent
        bl.transform.position = start; // Vị trí ban đầu

        // Lấy vị trí kết thúc        
        Vector3 offs = offset;
        if (!(target.nhanvat.team == 1))
            offs.x *= -1;
        Vector3 fsh = finish + offs;

        if (isRotate) // Bullet có định hướng ban đầu
        {
            Vector3 A = start;
            Vector3 B = fsh;
            float angle = Mathf.Rad2Deg * Mathf.Atan((B.y - A.y) / (B.x - A.x));
            if (!(target.nhanvat.team == 1))
                angle *= -1;
            bl.transform.Rotate(0, 0, angle);
        }

        C_Bullet c_bullet = bl.GetComponent<C_Bullet>();
        c_bullet.Move(parent, fsh, target, timeDlMove, targets);
    }

    public static async void MoveTo(Transform thisTran, Vector3 finish, float timeds, float timedf, float timedm, bool isComeback = true, bool isParabol = false, float height = 5.0f)
    {
        float t = 0.0f;

        // Delay start
        await Task.Delay(TimeSpan.FromSeconds(timeds / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));

        Vector3 old = thisTran.position;
        Vector3 start;

        Vector3 dis = finish - old;
        float speed = dis.magnitude / timedm;
        while (true)
        {
            start = thisTran.position;

            t += Time.deltaTime * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);

            if (isParabol)
            {
                thisTran.position = MathParabola.Parabola(start, finish, height, t / timedm);
            }
            else
            {
                thisTran.position = Vector3.MoveTowards(start, finish, Time.deltaTime * speed * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1));
            }

            if (Vector3.Distance(thisTran.position, finish) < 0.001f || t >= timedm)
                break;
            await Task.Yield();
        }

        // Delay finish
        await Task.Delay(TimeSpan.FromSeconds(timedf / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));

        // Comeback
        if (isComeback)
        {
            t = 0.0f;

            Vector3 startCb = start;
            Vector3 disCb = old - startCb;
            float speedCb = disCb.magnitude / 0.2f;
            while (true)
            {
                t += Time.deltaTime * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);

                startCb = thisTran.position;

                thisTran.position = Vector3.MoveTowards(startCb, old, Time.deltaTime * speedCb * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1));

                if (Vector3.Distance(thisTran.position, old) < 0.001f || t >= 0.2f)
                    break;
                await Task.Yield();
            }
        }

        // Reset RectTransform
        thisTran.GetComponent<RectTransform>().localPosition = new Vector3();
    }
}
