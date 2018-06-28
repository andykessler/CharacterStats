using System.Collections;
using System.Collections.Generic;

public enum Quality
{
    Poor,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
}

static class QualityMethods
{
    public static int GetColor(Quality q)
    {
        switch (q)
        {
            case Quality.Poor:
                return 0x9D9D9D;
            case Quality.Common:
                return 0xFFFFFF;
            case Quality.Uncommon:
                return 0x1EFF00;
            case Quality.Rare:
                return 0x0070DD;
            case Quality.Epic:
                return 0xA335EE;
            case Quality.Legendary:
                return 0xFF8000;
            default:
                return 0xFF00FF;
        }
    }
}
