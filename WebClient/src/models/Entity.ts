import {GameObjectSubType, GameObjectType} from '@/models/TypeAlias';

export interface Entity {
    objectId: number;
    name: string;

    type: GameObjectType;
    subType: GameObjectSubType;
}
