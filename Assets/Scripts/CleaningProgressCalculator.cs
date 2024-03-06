using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class CleaningProgressCalculator : MonoBehaviour
{
    public event Action<float> ProgressChanged;

    [SerializeField] private Vacuum _vacuum;
    [SerializeField] private TextureCleaner _mop;
    [SerializeField] private TextureCleaner _brush;
    [SerializeField] private WaterSprinkler _waterSprinkler;
    [SerializeField] private RepairTool _repairTool;
    [SerializeField] private Texture2D _empty;
    [SerializeField] private float _pixelDirty1Influence;
    [SerializeField] private float _pixelDirty2Influence;
    [SerializeField] private float _alfaDirtyInfluence;
    [SerializeField] private float _trashInfluence;
    [SerializeField] private float _repairInfluence;

    private int _startDirty1PixelsCount;
    private int _startDirty2PixelsCount;
    private float _startAlfa;
    private int _startTrashCount;
    private int _dirty1Pixels;
    private int _dirty2Pixels;
    private int _trashCount;
    private float _alfa;
    private float _pixels1Progress;
    private float _pixels2Progress;
    private float _alfaProgress;
    private float _trashProgress;
    private float _repairProgress;

    private float _sumInfluence => _pixelDirty1Influence + _pixelDirty2Influence + _alfaDirtyInfluence + _trashInfluence + _repairInfluence;
    private float GeneralProgress => _pixels1Progress + _pixels2Progress + _alfaProgress + _trashProgress + _repairProgress;

    public void Initialize(Carpet carpet)
    {
        var data = carpet.GetData();
        if (data.Dirty1.mainTexture == _empty)
            _pixelDirty1Influence = 0;
        if(data.Dirty2.mainTexture == _empty)
            _pixelDirty2Influence = 0;
        if (data.DirtyAlfa.color.a == 0)
            _alfaDirtyInfluence = 0;
        if (carpet.Trash.Length == 0)
            _trashInfluence = 0;
        if (carpet.Whole)
            _repairInfluence = 0;
        var pixelDirtyTextures = new Texture2D[] { data.Dirty1.mainTexture as Texture2D, data.Dirty2.mainTexture as Texture2D };
        _startAlfa = _alfa = data.DirtyAlfa.color.a;
        _startTrashCount = _trashCount = carpet.Trash.Length;
        foreach (var pixel in (data.Dirty1.mainTexture as Texture2D).GetPixels())
            if (pixel != Color.clear)
                _startDirty1PixelsCount++;
        foreach (var pixel in (data.Dirty2.mainTexture as Texture2D).GetPixels())
            if (pixel != Color.clear)
                _startDirty2PixelsCount++;
        _dirty1Pixels = _startDirty1PixelsCount;
        _dirty2Pixels = _startDirty2PixelsCount;
    }

    private void OnEnable()
    {
        _vacuum.Clean += CalculateTrashProgress;
        _mop.Clean += CalculatePixels1Progress;
        _brush.Clean += CalculatePixels2Progress;
        _waterSprinkler.Clean += CalculateAlfaProgress;
        _repairTool.FinishRepair += CalculateRepairProgress;
    }

    private void OnDisable()
    {
        _vacuum.Clean -= CalculateTrashProgress;
        _mop.Clean -= CalculatePixels1Progress;
        _brush.Clean -= CalculatePixels2Progress;
        _waterSprinkler.Clean -= CalculateAlfaProgress;
        _repairTool.FinishRepair -= CalculateRepairProgress;
    }

    private void CalculateTrashProgress()
    {
        _trashCount--;
        _trashProgress = (1 - ((float)_trashCount / _startTrashCount)) * (_trashInfluence / _sumInfluence);
        ProgressChanged?.Invoke(GeneralProgress);
    }

    private void CalculatePixels1Progress(int cleanedPixels)
    {
        _dirty1Pixels -= cleanedPixels;
        _pixels1Progress = (1 - ((float)_dirty1Pixels / _startDirty1PixelsCount)) * (_pixelDirty1Influence / _sumInfluence);
        ProgressChanged?.Invoke(GeneralProgress);
    }

    private void CalculatePixels2Progress(int cleanedPixels)
    {
        _dirty2Pixels -= cleanedPixels;
        _pixels2Progress = (1 - ((float)_dirty2Pixels / _startDirty2PixelsCount)) * (_pixelDirty2Influence / _sumInfluence);
        ProgressChanged?.Invoke(GeneralProgress);
    }

    private void CalculateAlfaProgress(float alfa)
    {
        _alfa = alfa;
        _alfaProgress = (1 - (_alfa / _startAlfa)) * (_alfaDirtyInfluence / _sumInfluence);
        ProgressChanged?.Invoke(GeneralProgress);

    }

    private void CalculateRepairProgress()
    {
        _repairProgress = _repairInfluence / _sumInfluence;
        ProgressChanged?.Invoke(GeneralProgress);
    }
}
