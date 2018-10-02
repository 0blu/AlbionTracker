import Vue from 'vue';
import Vuex from 'vuex';
import {TimeSection} from '@/models/TimeSection';
import {ClusterDetails} from '@/models/ClusterDetails';
import {NewEntity} from '@/models/ws/NewEntity';
import {ChangeCluster} from '@/models/ws/ChangeCluster';
import {Entity} from '@/models/Entity';
import {CombatEvent} from '@/models/ws/CombatEvent';
import {InCombatStateUpdate} from '@/models/ws/InCombatStateUpdate';
import {ConfigModel} from '@/models/ConfigModel';

Vue.use(Vuex);

export interface AlbionDamageTrackerStoreState {
    timestamp: number;

    currentCluster?: ClusterDetails;
    activeSection?: TimeSection;
    visibleSection?: TimeSection;
    allSections: TimeSection[];
    currentInCombatStatus: boolean;

    config: ConfigModel;
}

const store = new Vuex.Store<AlbionDamageTrackerStoreState>({
    state: {
        timestamp: Date.now(),

        currentCluster: undefined,
        activeSection: undefined,
        visibleSection: undefined,
        allSections: [],
        currentInCombatStatus: false,

        config: {
            anonymizeNames: false,
        },
    },
    mutations: {
        setTimestamp(state, newTimestamp: number) {
            state.timestamp = newTimestamp;
        },
        startNewSection(state) {
            const startTime = Date.now();
            const currentActiveSection = state.activeSection;
            if (currentActiveSection) {
                currentActiveSection.endTime = startTime;
            }

            const currentCluster = state.currentCluster;
            if (!currentCluster) {
                throw new Error('Try to startNewSection but no cluster is set... this should not happen');
            }

            const section: TimeSection = {
                sectionName: 'someRandomFight',
                cluster: currentCluster,

                startTime,
                get duration() {
                    if (!this.endTime) {
                        return store.state.timestamp - this.startTime;
                    }
                    return this.endTime - this.startTime;
                },
                endTime: undefined,

                involvedEntities: {},
                quickFightSummary: {},
                combatEntries: [],
            };

            state.allSections.push(section);

            state.visibleSection = section;
            state.activeSection = section;
        },
        endCurrentSection(state) {
            const currentActiveSection = state.activeSection;

            if (!currentActiveSection) {
                return;
            }

            currentActiveSection.endTime = Date.now();
            state.activeSection = undefined;
        },
        changeCluster(state, clusterName: string) {
            state.currentCluster = {
                clusterName,
                knownEntities: {},
            };
        },
        addEntity(state, entity: Entity) {
            const currentCluster = state.currentCluster;
            if (!currentCluster) {
                throw new Error('Try to addEntity but no cluster is set... this should not happen');
            }
            currentCluster.knownEntities[entity.objectId] = entity;
        },
        addCombatEvent(state, event: CombatEvent) {
            const activeSection = state.activeSection;
            if (!activeSection) {
                throw new Error('Try to addCombatEvent but no activeSection is set... this should not happen');
            }
            activeSection.combatEntries.push({
                target: activeSection.cluster.knownEntities[event.objectId],
                timeStamp: event.timeStamp,
                healthChange: event.healthChange,
                newHealthValue: event.newHealthValue,
                effectType: event.effectType,
                effectOrigin: event.effectOrigin,
                causer: activeSection.cluster.knownEntities[event.causerId],
                causingSpell: event.causingSpell,
            });
            const involvedEntities = activeSection.involvedEntities;
            if (!involvedEntities[event.objectId]) {
                Vue.set(involvedEntities as any, event.objectId, activeSection.cluster.knownEntities[event.objectId]);
                activeSection.quickFightSummary[event.objectId] = {
                    heal: 0,
                    damage: 0,
                    damageTaken: 0,
                    healTaken: 0,
                };
                if (event.healthChange > 0) {
                    activeSection.quickFightSummary[event.objectId].healTaken += event.healthChange;
                } else {
                    activeSection.quickFightSummary[event.objectId].damageTaken += -event.healthChange;
                }
            }
            if (event.causerId) {
                if (!involvedEntities[event.causerId]) {
                    Vue.set(involvedEntities as any, event.causerId, activeSection.cluster.knownEntities[event.causerId]);
                    activeSection.quickFightSummary[event.causerId] = {
                        heal: 0,
                        damage: 0,
                        damageTaken: 0,
                        healTaken: 0,
                    };
                }
                if (event.healthChange > 0) {
                    activeSection.quickFightSummary[event.causerId].heal += event.healthChange;
                } else {
                    activeSection.quickFightSummary[event.causerId].damage += -event.healthChange;
                }
            }
        },
        changeInCombatStatus(state, newInCombatStatus: boolean) {
            state.currentInCombatStatus = newInCombatStatus;
        },
        setVisibleSection(state, section: TimeSection) {
            state.visibleSection = section;
        },
    },
    actions: {
        async wsInCombatStateUpdate(context, update: InCombatStateUpdate) {
            const newState = update.inActiveCombat || update.inPassiveCombat;
            if (newState === context.state.currentInCombatStatus) {
                return;
            }
            context.commit('changeInCombatStatus', newState);
            // TODO: if auto section switch is enabled
            if (newState) {
                context.commit('startNewSection');
            } else {
                context.commit('endCurrentSection');
            }
        },
        async wsChangeCluster(context, changeCluster: ChangeCluster) {
            context.commit('changeCluster', changeCluster.clusterName);
            context.commit('startNewSection');
        },
        async wsNewEntity(context, newEntity: NewEntity) {
            context.commit('addEntity', {
                objectId: newEntity.objectId,
                name: newEntity.name,
                type: newEntity.type,
                subType: newEntity.subType,
            } as Entity);
        },
        async wsCombatEvent(context, event: CombatEvent) {
            context.commit('addCombatEvent', event);
        },
    },
});

setInterval(() => {
    store.commit('setTimestamp', Date.now());
}, 250);

export default store;
