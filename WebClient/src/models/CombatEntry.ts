import {EffectOrigin, EffectType, GameTimeStamp} from '@/models/TypeAlias';
import {Entity} from '@/models/Entity';

export interface CombatEntry {
    target: Entity;
    timeStamp: GameTimeStamp;
    healthChange: number;
    newHealthValue: number;
    effectType: EffectType;
    effectOrigin: EffectOrigin;
    causer: Entity;
    causingSpell: string;
}
