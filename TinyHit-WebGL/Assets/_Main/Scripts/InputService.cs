using UnityEngine;

public class InputService : MonoBehaviour
{
    public InputArea ThrowArea => _throwArea;
    public InputArea RestartArea => _restartArea;
    public InputArea MenuArea => _menuArea;
    public InputArea StartArea => _startArea;
    public InputArea ShopArea => _shopArea;
    public InputArea ShopToMenuArea => _shopToMenuArea;
    public InputArea BuyArea => _buyArea;
    public InputArea SwitchShopPrevArea => _switchShopPrevArea; 
    public InputArea SwitchShopNextArea => _switchShopNextArea;

    [SerializeField] private InputArea _throwArea;
    [SerializeField] private InputArea _restartArea;
    [SerializeField] private InputArea _menuArea;
    [SerializeField] private InputArea _startArea;
    [SerializeField] private InputArea _shopArea;
    [SerializeField] private InputArea _shopToMenuArea;
    [SerializeField] private InputArea _buyArea;
    [SerializeField] private InputArea _switchShopPrevArea;
    [SerializeField] private InputArea _switchShopNextArea;

    public void SetActiveThrowInputArea(bool isActive)
    {
        if (_throwArea == null) return;

        _throwArea.gameObject.SetActive(isActive);
    }

    public void SetActiveGameOverInputAreas(bool isActive)
    {
        if (_restartArea == null || _menuArea == null) return;

        _restartArea.gameObject.SetActive(isActive);
        _menuArea.gameObject.SetActive(isActive);
    }

    public void SetActiveMenuInputAreas(bool isActive)
    {
        if (_startArea == null || _shopArea == null) return;

        _startArea.gameObject.SetActive(isActive);
        _shopArea.gameObject.SetActive(isActive);
    }

    public void SetActiveShopInputAreas(bool isActive)
    {
        if (_shopToMenuArea == null || _buyArea == null || _switchShopPrevArea == null || _switchShopNextArea == null) return;

        _shopToMenuArea.gameObject.SetActive(isActive);
        _buyArea.gameObject.SetActive(isActive);
        _switchShopPrevArea.gameObject.SetActive(isActive);
        _switchShopNextArea.gameObject.SetActive(isActive);
    }
}