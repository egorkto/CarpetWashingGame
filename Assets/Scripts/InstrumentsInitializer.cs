using UnityEngine;

public class InstrumentsInitializer : MonoBehaviour
{
    [SerializeField] private WaterSprinkler _waterSprinkler;
    [SerializeField] private FoamSprinkler _foamSprinkler;
    [SerializeField] private WateringCan _wateringCan;
    [SerializeField] private TextureCleaner _mop;
    [SerializeField] private TextureCleaner _brush;
    [SerializeField] private RepairTool _repairTool;

    public void Initialize(Carpet carpet)
    {
        var data = carpet.GetData();
        _waterSprinkler.Initialize(data.DirtyAlfa);
        _foamSprinkler.Initialize(data.Foam1.mainTexture as Texture2D);
        _wateringCan.Initialize(data.Foam2.mainTexture as Texture2D);
        _mop.Initialize(data.Foam1.mainTexture as Texture2D, data.Dirty1.mainTexture as Texture2D);
        _brush.Initialize(data.Foam2.mainTexture as Texture2D, data.Dirty2.mainTexture as Texture2D);
        _repairTool.Initialize(carpet, carpet.GetBounds());
    }
}
