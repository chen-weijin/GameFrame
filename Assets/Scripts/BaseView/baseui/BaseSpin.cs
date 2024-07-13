using UnityEngine;
using UnityEngine.UI;

public class BaseSpin : MonoBehaviour
{
    public void Show()
    {
        gameObject.GetRectTransform().BeginAction()
            .DelayTime(4)
            .CallFunc(() => {
                Destroy(gameObject);
            })
            .Run();
    }
}
