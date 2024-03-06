using UnityEngine;

public class InstrumentEffector : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _workingClip;
    [SerializeField] private Instrument _instrument;

    private void OnEnable()
    {
        _instrument.StartMotion += StartEffects;
        _instrument.Motion += HoldEffects;
        _instrument.EndMotion += FinishEffects;
    }

    private void OnDisable()
    {
        _instrument.StartMotion += StartEffects;
        _instrument.Motion += HoldEffects;
        _instrument.EndMotion += FinishEffects;
    }

    private void StartEffects()
    {
        _particles.Play();
        _source.clip = _workingClip;
        _source.Play();
    }

    private void HoldEffects(Vector3 point)
    {
        _particles.transform.position = new Vector3(point.x, _particles.transform.position.y, point.z);

    }

    private void FinishEffects()
    {
        _particles.Stop();
        _source.Stop();
    }
}
