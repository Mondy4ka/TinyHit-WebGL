using UnityEngine;

public class InputService : MonoBehaviour
{
    public InputArea ThrowArea => _throwArea;
    public InputArea RestartArea => _restartArea;
    public InputArea MenuArea => _menuArea;
    public InputArea StartArea => _startArea;

    [SerializeField] private InputArea _throwArea;
    [SerializeField] private InputArea _restartArea;
    [SerializeField] private InputArea _menuArea;
    [SerializeField] private InputArea _startArea;

    public void SetActiveThrowInputArea(bool isActive)
    {
        if (ThrowArea == null) return;

        ThrowArea.gameObject.SetActive(isActive);
    }

    public void SetActiveReastartInputArea(bool isActive)
    {
        if (RestartArea == null) return;

        RestartArea.gameObject.SetActive(isActive);
    }

    public void SetActiveMenuInputArea(bool isActive)
    {
        if (MenuArea == null) return;

        MenuArea.gameObject.SetActive(isActive);
    }

    public void SetActiveStartInputArea(bool isActive)
    {
        if (StartArea == null) return;

        StartArea.gameObject.SetActive(isActive);
    }
}