using TMPro;
using UnityEngine;

public class LevelProgressPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;

    public void Present(int level)
    {
        _levelText.text = level.ToString();
    }
}