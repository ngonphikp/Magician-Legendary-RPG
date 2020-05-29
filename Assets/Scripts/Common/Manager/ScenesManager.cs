using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Back - Chuyển sang scene cha của scene hiện tại đã lưu trong scenes
    public void LoadPreviousScene()
    {
        // Lấy tên scene hiện tại
        string present = SceneManager.GetActiveScene().name;
        Debug.Log("Back: " + present);
        // Kiểm tra trong scenes tồn tại chưa
        // Nếu tồn tại -> chuyển scene
        if (GameManager.instance.scenes.ContainsKey(present))
        {
            SceneManager.LoadScene(GameManager.instance.scenes[present]);
        }
        // Nếu không -> chuyển về HomeGame
        else
        {
            Debug.Log(present + ": Parent - NULL");
            SceneManager.LoadScene("HomeGame");
        }
    }

    // Chuyển scence và lưu lại cha - next->parent = present
    public void ChangeScene(string next)
    {
        // Lấy tên scene hiện tại
        string present = SceneManager.GetActiveScene().name;

        // Nếu scence hiện tại = scence next
        if (next == present)
        {
            Debug.Log("Trùng scenes");
            return;
        }

        // Kiểm tra trong scenes tồn tại chưa
        // Nếu tồn tại -> cập nhật lại cha
        if (GameManager.instance.scenes.ContainsKey(next))
        {
            GameManager.instance.scenes[next] = present;
        }
        // Nếu không -> Thêm vào scenes
        else
        {
            GameManager.instance.scenes.Add(next, present);
            //foreach(var i in GameManager.instance.scenes)
            //{
            //    Debug.Log(i.Key + " / " + i.Value);
            //}
        }
        Debug.Log(next + " / " + GameManager.instance.scenes[next]);
        // Chuyển scene
        SceneManager.LoadScene(next);
    }

    public void ReLoadScence()
    {
        // Lấy tên scene hiện tại
        string present = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(present);
    }
}
