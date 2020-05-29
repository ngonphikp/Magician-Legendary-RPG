using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class C_SoftEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject content = null;
    [SerializeField]
    private Image image = null;

    [SerializeField]
    private string rs_effect;

    public async void DestroySE()
    {
        content.GetComponent<Animator>().SetTrigger("destroy");

        await Task.Delay(600);
        Destroy(this.gameObject);
    }

    public void set(string rs_effect)
    {
        this.rs_effect = rs_effect;
        Sprite sprite = QuickFunction.getAssetImages("Sprites/SoftEffect/" + rs_effect);
        if (sprite != null) image.sprite = sprite;
    }
}
