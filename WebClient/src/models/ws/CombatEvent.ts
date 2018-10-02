import {EffectOrigin, EffectType, GameTimeStamp, ObjectReference} from '@/models/TypeAlias';

export interface CombatEvent {
    objectId: ObjectReference;
    timeStamp: GameTimeStamp;
    healthChange: number;
    newHealthValue: number;
    effectType: EffectType;
    effectOrigin: EffectOrigin;
    causerId: ObjectReference;
    causingSpell: string;
}
