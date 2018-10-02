import {Entity} from '@/models/Entity';
import {GameObjectSubType} from '@/models/TypeAlias';

export class PlayerNameAnonymizer {

    private static playerCountNumber = 0;
    private static readonly cachedPlayerNames: {[playerName: string]: string} = {};

    public static getAnonymizedName(entity: Entity): string {
        const foundName = PlayerNameAnonymizer.cachedPlayerNames[entity.name];
        if (foundName) {
            return foundName;
        }
        return PlayerNameAnonymizer.cachedPlayerNames[entity.name] = PlayerNameAnonymizer.getNewName(entity);
    }

    private static getNewName(entity: Entity): string {
        if (entity.subType === GameObjectSubType.LocalPlayer) {
            return 'You';
        }
        return `Player${++PlayerNameAnonymizer.playerCountNumber}`;
    }
}
