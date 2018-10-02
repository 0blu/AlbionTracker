import {GameObjectSubType, GameObjectType, ObjectReference} from '@/models/TypeAlias';

export interface NewEntity {
    objectId: ObjectReference;
    type: GameObjectType;
    subType: GameObjectSubType;
    name: string;
}
