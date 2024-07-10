using framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginView : BaseView
{
    void Start()
    {
        UIManager.GetRootCanvas().gameObject.FindInChildren("btn_start").GetComponent<Button>().onClick.AddListener(() =>
        {
            //WXPlatform.Login((code) =>
            //{
            //    SceneManager.LoadScene("Lobby");
            //});
            SceneManager.LoadScene("Lobby");
        });
    }

}