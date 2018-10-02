namespace Albion.Common
{
    public enum EffectType : byte
    {
        None,
        Physical,
        Magic
    }

    public enum EffectOrigin : byte
    {
        MeleeAttack,
        RangedAttack,
        SpellDirect,
        SpellArea,
        SpellPassive,
        OverTimeEffect,
        Reflected,
        SpellCost,
        ServerAuthority
    }

    public enum ReflectDamageBehavior : byte
    {
        BasedOnOrigin,
        Always,
        Never
    }
}
