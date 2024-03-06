using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPresenter : MonoBehaviour
{
    [SerializeField] private CleaningProgressCalculator _progressCalculator;
    [SerializeField] private AudioSource _source;
    [SerializeField] private Image _progressBar;
    [SerializeField] private Image _canFinishLevelBorder;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _finishLevelButton;
    [SerializeField] private float _canFinishLevelProgress;
    [SerializeField] private Image[] _smiles;

    private int _nextSmileIndex = 0;

    private void OnEnable()
    {
        _finishLevelButton.gameObject.SetActive(false);
        _canFinishLevelBorder.rectTransform.anchoredPosition = new Vector2(_canFinishLevelBorder.rectTransform.anchoredPosition.x, _progressBar.rectTransform.rect.size.y * _canFinishLevelProgress);           
        _progressCalculator.ProgressChanged += Present;
    }

    private void OnDisable()
    {
        _progressCalculator.ProgressChanged -= Present;
    }

    private void Present(float progress)
    {
        var index = (int)(progress * _smiles.Length) - 1;
        if (index >= _nextSmileIndex)
        {
            _smiles[index].gameObject.SetActive(true);
            _nextSmileIndex = index + 1;
            _source.Play();
        }
        if (progress >= _canFinishLevelProgress)
        {
            _finishLevelButton.gameObject.SetActive(true);
        }
        _progressBar.fillAmount = progress;
        _text.text = ((int)(progress * 100)).ToString() + "%";
    }
}
