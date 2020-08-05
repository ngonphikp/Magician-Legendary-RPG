using System.Threading.Tasks;
using UnityEngine;

public class C_Ctl_T1007 : MonoBehaviour, I_Control
{
    //[Header("Anim2")]

    [Header("Anim3")]
    [SerializeField]
    private float time3d0 = 0.8f;
    [SerializeField]
    private float time3ds = 0f;
    [SerializeField]
    private float time3df = 1f;
    [SerializeField]
    private float time3dm = 0.3f;
    [SerializeField]
    private GameObject fx3d0 = null;
    [SerializeField]
    private float dis3d0 = -17f;
    [SerializeField]
    private float height3 = 7f;


    //[Header("Anim4")]

    [Header("Anim5")]
    [SerializeField]
    private float time5d0 = 0.6f;
    [SerializeField]
    private GameObject fx5d0 = null;
    [SerializeField]
    private float time5ds = 0.3f;
    [SerializeField]
    private float time5df = 0.8f;
    [SerializeField]
    private float time5dm = 0.3f;
    [SerializeField]
    private float dis5d0 = 17f;

    public void Play(int anim)
    {

        switch (anim)
        {
            case 2:
                Anim2();
                break;
            case 3:
                Anim3();
                break;
            case 4:
                Anim4();
                break;
            case 5:
                Anim5();
                break;
            case 6:
                Anim6();
                break;
            case 7:
                Anim7();
                break;
        }
    }

    private void Anim2()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 2");
    }

    private void Anim3()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 3");

        Vector3 finish = C_LibSkill.DisX(FightingGame.instance.targets[0].transform.position, dis3d0, FightingGame.instance.targets[0].nhanvat.team == 1);
        C_LibSkill.MoveTo(this.transform, finish, time3ds, time3df, time3dm, true, true, height3);

        C_LibSkill.FxHit(FightingGame.instance.targets, fx3d0, time3d0);
    }

    private void Anim4()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 4");        
    }

    private void Anim5()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 5");

        Vector3 G = C_LibSkill.GHero(FightingGame.instance.targets);
        Vector3 C = C_LibSkill.DisABC(G, this.transform.position, dis5d0 + Vector3.Distance(this.transform.position, G));
        C_LibSkill.MoveTo(this.transform, C, time5ds, time5df, time5dm, false);

        C_LibSkill.FxHit(FightingGame.instance.targets, fx5d0, time5d0);
    }

    private void Anim6()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 6");
    }

    private void Anim7()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 7");
    }
}
