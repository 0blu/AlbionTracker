import {Entity} from '@/models/Entity';

export type ObjectReference = number;
export type GameTimeStamp = number;

export interface EntityMap {
    [objectId: number]: Entity;
}

export enum EffectType {
    None,
    Physical,
    Magic,
}

export enum EffectOrigin {
    MeleeAttack,
    RangedAttack,
    SpellDirect,
    SpellArea,
    SpellPassive,
    OverTimeEffect,
    Reflected,
    SpellCost,
    ServerAuthority,
}

export enum GameObjectSubType {
    Unknown,
    LocalPlayer,
    Player,
    PvpPlayer,
    Mob,
    Boss,
}

export enum GameObjectType {
    Unknown,
    Player,
    Mob,
}

export enum MessageType {
    NewEntity,
    CombatEvent,
    InCombatStateUpdate,
    ChangeCluster,
}
