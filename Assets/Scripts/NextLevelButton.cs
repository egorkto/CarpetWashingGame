using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private int _nextLevelSceneIndex;

    public void RunNextLevel()
    {
        SceneManager.LoadScene(_nextLevelSceneIndex);
    }
}
