using framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMgr : Singleton<AudioMgr>
{
    public void PlayEffect(string path)
    {
        // 加载音频片段
        AudioClip audioClip = (AudioClip)Resources.Load(path);

        // 此方式播放音频, Unity3D会在transform.position处创建一个空游戏对象, 播放完音频后自动销毁该游戏对象
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
    }
    public void PlayMusic(string path)
    {
        GameObject core = _GetCore();
        if (core == null) return;
        AudioSource audioSource = core.EnsureComponent<AudioSource>();
        audioSource.loop = true;
        //加载音频片段
        //AudioClip audioClip = (AudioClip)Resources.Load("Audio/Footstep01");
        AudioClip audioClip = (AudioClip)Resources.Load(path);

        // 绑定音频片段
        audioSource.clip = audioClip;
        // 播放音频（选其中一种方式）
        audioSource.Play();

    }

    private GameObject _GetCore()
    {
        var s = SceneManager.GetActiveScene();
        var gArr = s.GetRootGameObjects();
        for (var i = 0; i < gArr.Length; i++)
        {
            if (gArr[i].name == "core")
            {
                return gArr[i].transform.gameObject;
            }
        }
        return null;
    }
}
