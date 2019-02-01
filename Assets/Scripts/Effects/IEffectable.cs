using System.Collections.Generic;

public interface IEffectable {

    List<Effect> Effects
    {
        get;
    }

    void ApplyEffects(Effect[] effects);
    void ApplyEffect(Effect effect);
    void RemoveEffect(Effect effect);
    void RegisterEffect(Effect effect); // used when want to just manage, and not apply

}
