import {EntityMap} from '@/models/TypeAlias';

export interface ClusterDetails {
    clusterName: string;

    knownEntities: EntityMap;
}
