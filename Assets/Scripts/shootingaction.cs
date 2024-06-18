using UnityEngine;
using UnityEngine.Events;

public class shootingaction : MonoBehaviour
{
    public UnityEvent action;

    public void Action()
    {
        action?.Invoke();
    }
}
