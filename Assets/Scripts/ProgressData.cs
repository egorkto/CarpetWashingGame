using System;
using System.Collections.Generic;

[Serializable]
public struct ProgressData
{
    public int Level;
    public List<InstrumentType> OpenedInstruments;
}
