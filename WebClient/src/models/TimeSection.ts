import {EntityMap} from '@/models/TypeAlias';
import {EntityFightSummary} from '@/models/EntityFightSummary';
import {ClusterDetails} from '@/models/ClusterDetails';
import {CombatEntry} from '@/models/CombatEntry';

export interface TimeSection {
    sectionName: string;
    cluster: ClusterDetails;

    startTime: number;
    duration: number;
    endTime?: number;

    // featuresEntity: Entity -- An entity like a boss that can be displayed as a name

    involvedEntities: EntityMap;
    quickFightSummary: {[objectId: number]: EntityFightSummary};
    combatEntries: CombatEntry[];
}
