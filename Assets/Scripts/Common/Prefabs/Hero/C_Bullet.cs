using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class C_Bullet : MonoBehaviour
{
    [Header("Tốc độ")]
    [SerializeField]
    private float speed = 100.0f;

    [Header("Parabol")]
    [SerializeField]
    private bool isParabol = false;
    [SerializeField]
    private float height = 5.0f;
    [SerializeField]
    private bool isChangeAngle = false;

    [Header("Auto Rotating")]
    [SerializeField]
    private bool isRotating = false;
    [SerializeField]
    private Vector3 rotating = new Vector3();

    [Header("Hiệu ứng nổ")]
    [SerializeField]
    private bool isExplosion = false;
    [SerializeField]
    private GameObject Explosion = null;
    [SerializeField]
    private bool isInPlace = true;

    [Header("Mảnh vỡ")]
    [SerializeField]
    private bool isBreak = false;
    [SerializeField]
    private GameObject fragment = null;
    [SerializeField]
    private float timeinit = 0.0f;
    [SerializeField]
    private float timefrm = 0.0f;
    [SerializeField]
    private Vector3 offsetfrm = new Vector3();

    [Header("Trúng đòn")]
    [SerializeField]
    private float timed = 0.0f;
    [SerializeField]
    private bool isHit = false;
    [SerializeField]
    private float time = 0.0f;

    [Header("Giới hạn chiều cao")]
    [SerializeField]
    private bool isOneToAll = true;
    [SerializeField]
    private bool islmH = false;
    [SerializeField]
    private float lmH = 10.0f;
    [SerializeField]
    private GameObject bl = null;
    [SerializeField]
    private bool isOffs = false;
    [SerializeField]
    private Vector3 offsetlmH = new Vector3();
    [SerializeField]
    private bool isRotate = false;

    [Header("Scale")]
    [SerializeField]
    private float ratio = 9.423f;
    [SerializeField]
    private float timefx = 0.0f;
    [SerializeField]
    private float timehit = 0.0f;

    [Header("Comback")]
    [SerializeField]
    private bool isComback = false;
    [SerializeField]
    private float timecb = 0.5f;
    [SerializeField]
    private Vector3 offset = new Vector3();

    private float t;
    private float timet;
    private Vector3 startPos;

    private async void Comback(bool isL2R = true)
    {
        if (isL2R)
        {
            Vector3 tmp = offset;
            tmp.x *= -1;
            offset = tmp;
        }
        Vector3 comback = startPos + offset;

        await Task.Delay(TimeSpan.FromSeconds(timecb / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
        while (true)
        {
            if (this == null || this.gameObject == null) break;

            float step = speed * Time.deltaTime * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
            transform.position = Vector3.MoveTowards(transform.position, comback, step);

            if (Vector3.Distance(transform.position, comback) < 0.001f)
            {
                Destroy(gameObject);
                break;
            }
            await Task.Yield();
        }
    }

    public async void Move(Transform parent, Vector3 finish, C_Character target, float timeDlMove = 0.0f, List<C_Character> targets = null)
    {
        // Khởi tạo biến
        t = 0.0f;
        startPos = this.transform.position;
        timet = Vector2.Distance(startPos, finish) / speed;

        prevPoint = this.transform.position;
        if (!(target.nhanvat.team == 1)) rotateOffset = 0;

        // Delay di chuyển bullet
        await Task.Delay(TimeSpan.FromSeconds(timeDlMove / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));

        while (true)
        {
            if (this == null || this.gameObject == null) break;

            if (isParabol)
            {
                t += Time.deltaTime * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
                transform.position = MathParabola.Parabola(startPos, finish, height, t / timet);
            }
            else
            {
                float step = speed * Time.deltaTime * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1);
                transform.position = Vector3.MoveTowards(transform.position, finish, step);
            }

            if (isChangeAngle)
            {
                RotationChangeWhileFlying();
            }

            if (isRotating)
            {
                transform.Rotate(
                    rotating.x * Time.deltaTime * 1000 * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1),
                    rotating.y * Time.deltaTime * 1000 * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1),
                    rotating.z * Time.deltaTime * 1000 * ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)
               );
            }

            if (Vector3.Distance(transform.position, finish) < 0.001f || (islmH && transform.position.y >= lmH) || t > timet)
            {
                t = 0.0f;
                if (isExplosion)
                {
                    GameObject fx = Instantiate(Explosion, target.transform);
                    if (isInPlace)
                    {
                        fx.transform.position = this.gameObject.transform.position;

                        if (isBreak && targets != null && targets.Count != 0)
                        {
                            for (int i = 0; i < targets.Count; i++)
                            {
                                C_LibSkill.CreateBullet(fragment, parent, targets[i], this.gameObject.transform.position, targets[i].transform.position, false, timeinit, timefrm, offsetfrm);
                            }
                        }
                    }
                }

                if (islmH)
                {
                    if (isOneToAll)
                    {
                        if (targets != null && targets.Count != 0)
                        {
                            if (isOffs)
                            {
                                for (int i = 0; i < targets.Count; i++)
                                {
                                    Vector3 offs = offsetlmH;
                                    if (!(targets[i].nhanvat.team == 1))
                                        offs.x *= -1;
                                    Vector3 srt = this.transform.position + offs;

                                    C_LibSkill.CreateBullet(bl, parent, targets[i], srt, targets[i].transform.position, isRotate);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Rơi tự do từ lmH -> target
                        C_LibSkill.CreateBullet(bl, parent, target, new Vector3(finish.x, lmH, this.transform.position.z), target.transform.position);
                    }
                }

                if (isComback)
                {
                    Comback(!(target.nhanvat.team == 1));
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(timed / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
                    Destroy(gameObject);
                }

                if (isHit && target != null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(time / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
                    target.Beaten();
                }
                break;
            }
            await Task.Yield();
        }
    }

    public async void ScaleX(Vector2 A, Vector3 B, C_Character target = null)
    {
        float dis = Vector2.Distance(A, B);
        float scale = dis / ratio;

        this.transform.localScale = new Vector3(this.transform.localScale.x * scale, this.transform.localScale.y, this.transform.localScale.z);

        if (isExplosion && target != null)
        {
            await Task.Delay(TimeSpan.FromSeconds(timefx / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));

            GameObject fx = Instantiate(Explosion, target.transform);
        }

        if (isHit && target != null)
        {
            await Task.Delay(TimeSpan.FromSeconds(timehit / ((FightingGame.instance) ? FightingGame.instance.myTimeScale : 1)));
            target.Beaten();
        }
    }

    private Vector3 currPoint;
    private Vector3 prevPoint;
    private Vector3 currDir;
    private float rotateOffset = 180;
    private void RotationChangeWhileFlying()
    {
        currPoint = this.transform.position;

        //get the direction (from previous pos to current pos)
        currDir = prevPoint - currPoint;

        //normalize the direction
        currDir.Normalize();

        //get angle whose tan = y/x, and convert from rads to degrees
        float rotationZ = Mathf.Atan2(currDir.y, currDir.x) * Mathf.Rad2Deg;
        //Vector3 Vzero = Vector3.zero;

        //rotate z based on angle above + an offset (currently 90)
        transform.rotation = Quaternion.Euler(0, 0, rotationZ + rotateOffset);

        //store the current point as the old point for the next frame
        prevPoint = currPoint;
    }
}
