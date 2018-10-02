<template>
    <div>
        <div class="bars">
            <div class="bar" v-for="(barInfo, index) in getAllInfos" :key="barInfo.entity.objectId">
                <div class="bar-out" :style="{'width': `${barInfo.percent}%`, 'background-color': getEntityColor(barInfo.entity)}">

                </div>
                <div class="bar-details">
                    <span class="details-left">
                        <span>{{index + 1}}. </span>
                        <span>{{barInfo.entity | getEntityName}}</span>
                    </span>
                    <span class="details-right">{{barInfo.value}} ({{barInfo.perSec}}, {{Math.round(barInfo.percent)}}%)</span>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import {Component, Vue, Watch} from 'vue-property-decorator';
import {TimeSection} from '../models/TimeSection';
import {Entity} from '../models/Entity';
import {GameObjectType} from '../models/TypeAlias';
import {CombatEntry} from '../models/CombatEntry';
import {NameColorProvider} from '../libs/NameColorProvider';
import {FightEvaluator, RankingSortMode} from '../libs/FightEvaluator';

interface BarDiagramInfo {
    entity: Entity;
    value: number;
    perSec: number;
    percent: number;
}

@Component({
    components: {
    },
})
export default class BarDiagram extends Vue {

    private infos: BarDiagramInfo[] = [];

    private get visibleSection(): TimeSection | undefined {
        return this.$store.state.visibleSection;
    }

    private get selectedEntities(): Entity[] {
        if (!this.visibleSection) {
            return [];
        }

        return Object.values(this.visibleSection.involvedEntities)
            .filter((e) => e.type === GameObjectType.Player);
    }

    private get combatEntries(): CombatEntry[] {
        const visibleSection = this.$store.state.visibleSection;
        if (!visibleSection) {
            return [];
        }
        return visibleSection.combatEntries;
    }

    private get sectionDurationInSec(): number {
        if (!this.visibleSection) {
            return 0;
        }
        return this.visibleSection.duration / 1000;
    }

    private get getAllInfos(): BarDiagramInfo[] {
        const entities = this.selectedEntities;

        if (!this.visibleSection) {
            return [];
        }
        const quickFightSummaries = this.visibleSection.quickFightSummary;

        const results: BarDiagramInfo[] = [];

        let sum = 0;

        for (const entity of entities) {
            const value = FightEvaluator.giveValue(RankingSortMode.ByDamage, quickFightSummaries[entity.objectId]);
            sum += value;
            results.push({
                entity,
                value,
                perSec: Math.ceil(value / this.sectionDurationInSec),
                percent: 0, // Will be set after I know the sum
            });
        }

        for (const result of results) {
            result.percent = (result.value / sum) * 100;
        }

        return results.sort((a, b) => b.value - a.value);
    }

    private getEntityColor(entity: Entity): string {
        return NameColorProvider.getColor(entity.name);
    }
}
</script>

<style scoped>
    .bar {
        position: relative;
        height: 3.5rem;
        margin-bottom: 0.25rem;
    }

    .bar > * {
        height: 100%;
    }

    .bar-out {
        position: absolute;
        transition: width 0.2s;
    }
    .bar-details {
        position: relative;
        z-index: 1;
        font-size: 2rem;
    }

    .details-right {
        float: right;
    }
</style>
