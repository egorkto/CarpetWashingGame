using UnityEngine;

[CreateAssetMenu(fileName = "New CarpetSetup", menuName = "Carpet Setup", order = 52)]
public class CarpetSetup : ScriptableObject
{
    public Mesh WholeMesh => _wholeMesh;
    public Mesh[] TornMeshs => _tornMeshs;
    public Texture2D[] CarpetTextures => _carpetTextures;
    public Texture2D[] DirtyTextures1 => _dirtyTextures1;
    public Texture2D[] DirtyTextures2 => _dirtyTextures2;

    [SerializeField] private Mesh _wholeMesh;
    [SerializeField] private Mesh[] _tornMeshs;
    [SerializeField] private Texture2D[] _carpetTextures;
    [SerializeField] private Texture2D[] _dirtyTextures1;
    [SerializeField] private Texture2D[] _dirtyTextures2;
}
