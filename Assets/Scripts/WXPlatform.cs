using PlasticGui.Configuration.CloudEdition.Welcome;
using System.Collections;
using UnityEngine;
using WeChatWASM;

public class WXPlatform
{
    public static void Login()
    {
        LoginOption info = new LoginOption();
        info.complete = (aa) => { /*登录完成处理,成功失败都会调*/ };
        info.fail = (aa) => { /*登录失败处理*/ };
        info.success = (aa) =>
        {
            //登录成功处理
            Debug.Log("__OnLogin success登陆成功!查看Code：" + aa.code);
            //登录成功...这完成后，跳到下一步，《二、查看授权》
        };

        WXSDKManagerHandler.Instance.Login(info);
    }

    public static void GetSetting()
    {
        GetSettingOption info = new GetSettingOption();
        info.complete = (aa) => { /*获取完成*/ };
        info.fail = (aa) => { /*获取失败*/};
        info.success = (aa) =>
        {
            if (!aa.authSetting.ContainsKey("scope.userInfo") || !aa.authSetting["scope.userInfo"])
            {
                //《三、调起授权》
                Debug.Log("《三、调起授权》:");
            }
            else
            {
                //《四、获取用户信息》
                Debug.Log("《四、获取用户信息》:");
            }
        };
        WXSDKManagerHandler.Instance.GetSetting(info);
    }
    public static void GetUserInfo()
    {

        //直接获取用户信息
        GetUserInfoOption userInfo = new GetUserInfoOption()
        {
            withCredentials = true,
            lang = "zh_CN",
            success = (data) =>
            {
                Debug.Log(data.userInfo.nickName);
            },
            fail = (data) =>
            {

            }
        };
        WXSDKManagerHandler.Instance.GetUserInfo(userInfo);
    }

    public static void GetUserInfoBtn()
    {

        //调用请求获取用户信息
        WXUserInfoButton btn = WXSDKManagerHandler.Instance.CreateUserInfoButton(0, 0, Screen.width, Screen.height, "zh_CN", true);
        btn.OnTap((res) =>
        {
            if (res.errCode == 0)
            {
                //用户已允许获取个人信息，返回的data即为用户信息
                Debug.Log(res.userInfo.nickName);
            }
            else
            {
                Debug.Log("用户未允许获取个人信息");
            }
            btn.Hide();
        });
    }
}