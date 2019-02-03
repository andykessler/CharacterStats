[System.Flags]
public enum TargetType : int
{
    Self    = (1 << 1),
    Ally    = (1 << 2),
    Enemy   = (1 << 3),
    Neutral = (1 << 4),
    Air     = (1 << 5),
    Ground  = (1 << 6),
    Land    = (1 << 7),
}