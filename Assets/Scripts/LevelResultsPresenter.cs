using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelResultsPresenter : MonoBehaviour
{
    [SerializeField] private Carpet _carpet;
    [SerializeField] private Vector3 _carpetPosition;
    [SerializeField] private LayerMask _dirtyCarpetMask;
    [SerializeField] private LayerMask _cleanCarpetMask;
    [SerializeField] private int _levelResultsSceneIndex;
    [SerializeField] private Texture2D _empty;

    private Carpet _dirty;
    private Carpet _clean;
    private CarpetData _dirtyData;
    private CarpetData _cleanData;

    public void SetDirty(CarpetData data)
    {
        var copy = data;
        copy.DirtyAlfa = new Material(data.DirtyAlfa);
        copy.Dirty1 = new Material(data.Dirty1);
        copy.Dirty2 = new Material(data.Dirty2);
        copy.Foam1 = new Material(data.Foam1);
        copy.Foam2 = new Material(data.Foam2);
        copy.Foam1.mainTexture = _empty;
        copy.Foam2.mainTexture = _empty;
        var originalDirty1 = copy.Dirty1.mainTexture as Texture2D;
        var originalDirty2 = copy.Dirty2.mainTexture as Texture2D;
        var copyDirty1 = new Texture2D(originalDirty1.width, originalDirty1.height);
        var copyDirty2 = new Texture2D(originalDirty2.width, originalDirty2.height);
        copyDirty1.SetPixels(originalDirty1.GetPixels());
        copyDirty2.SetPixels(originalDirty2.GetPixels());
        copyDirty1.Apply();
        copyDirty2.Apply();
        copy.Dirty1.mainTexture = copyDirty1;
        copy.Dirty2.mainTexture = copyDirty2;
        _dirtyData = copy;
    }

    public void SetCleaned(CarpetData data)
    {
        var copy = data;
        copy.Foam1 = new Material(data.Foam1);
        copy.Foam2 = new Material(data.Foam2);
        copy.Foam1.mainTexture = _empty;
        copy.Foam2.mainTexture = _empty;
        _cleanData = copy;
    }
    
    public void PresentLevelResults()
    {
        SceneManager.LoadScene(_levelResultsSceneIndex);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == _levelResultsSceneIndex)
        {
            _dirty = Instantiate(_carpet, _carpetPosition, Quaternion.Euler(0, -90, 90));
            _clean = Instantiate(_carpet, _carpetPosition, Quaternion.Euler(0, -90, 90));
            _dirty.SetData(_dirtyData);
            _clean.SetData(_cleanData);
            SetLayer(_dirty.gameObject, (int)Mathf.Log(_dirtyCarpetMask, 2));
            SetLayer(_clean.gameObject, (int)Mathf.Log(_cleanCarpetMask, 2));
            Destroy(gameObject);
        }
    }

    private void SetLayer(GameObject gameObj, int layer)
    {
        gameObj.layer = layer;
        foreach (var child in gameObj.GetComponentsInChildren<Transform>())
            child.gameObject.layer = layer;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
