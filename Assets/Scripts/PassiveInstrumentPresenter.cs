public class PassiveInstrumentPresenter : InstrumentPresenter
{
    protected override void Initialize()
    {
        Instrument.transform.gameObject.SetActive(true);
    }
}
