using UnityEngine;
using TMPro;

public class FortuneUI : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private GameObject _panel;

    [SerializeField]
    private TextMeshProUGUI _tmp;

    [SerializeField]
    private string[] _fortunes;

    private void Start()
    {
        _player.Input.CancelEvent += OnCancel;
    }

    private void Update()
    {
        if (_player.Input.IsAttacking)
        {
            OnCancel();
        }
    }

    private void OnCancel()
    {
        if (_panel.activeSelf)
        {
            _panel.SetActive(false);
            UnpauseGame();
        }
    }

    public void ShowFortune()
    {
        _tmp.text = _fortunes[GetRandomFortuneIndex()];
        _panel.SetActive(true);
        PauseGame();
    }

    private int GetRandomFortuneIndex() => Random.Range(0, _fortunes.Length);

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
