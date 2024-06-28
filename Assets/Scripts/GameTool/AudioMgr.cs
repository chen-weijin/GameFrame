using framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMgr : Singleton<AudioMgr>
{
    public void PlayEffect(string path)
    {
        // ������ƵƬ��
        AudioClip audioClip = (AudioClip)Resources.Load(path);

        // �˷�ʽ������Ƶ, Unity3D����transform.position������һ������Ϸ����, ��������Ƶ���Զ����ٸ���Ϸ����
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
    }
    public void PlayMusic(string path)
    {
        GameObject core = _GetCore();
        if (core == null) return;
        AudioSource audioSource = core.EnsureComponent<AudioSource>();
        audioSource.loop = true;
        //������ƵƬ��
        //AudioClip audioClip = (AudioClip)Resources.Load("Audio/Footstep01");
        AudioClip audioClip = (AudioClip)Resources.Load(path);

        // ����ƵƬ��
        audioSource.clip = audioClip;
        // ������Ƶ��ѡ����һ�ַ�ʽ��
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
