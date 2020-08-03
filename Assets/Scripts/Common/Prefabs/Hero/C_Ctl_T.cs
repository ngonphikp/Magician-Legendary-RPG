using System.Threading.Tasks;
using UnityEngine;

public class C_Ctl_T : MonoBehaviour, I_Control
{
    //[Header("Anim2")]

    //[Header("Anim3")]

    //[Header("Anim4")]
    
    //[Header("Anim5")]

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
    }

    private void Anim4()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 4");        
    }

    private void Anim5()
    {
        Debug.Log(this.gameObject.GetComponent<C_Character>().nhanvat.id_nv + " Anim 5");
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
