<template>
    <div class="combat-entry-display">
        <CombatLogEntry v-for="entry in combatEntries" :entry="entry" :key="entry.timestamp" />
    </div>
</template>

<script lang="ts">
import {Component, Vue} from 'vue-property-decorator';
import {CombatEntry} from '../models/CombatEntry';
import CombatLogEntry from './CombatLogEntry.vue';

@Component({
    components: {
        CombatLogEntry,
    },
})
export default class CombatLog extends Vue {

    protected updated() {
        const $self = this.$el;
        $self.scrollTop = $self.scrollHeight;
    }

    private get combatEntries(): CombatEntry[] {
        const section = this.$store.state.visibleSection;
        if (!section) {
            return [];
        }
        return section.combatEntries;
    }
}
</script>

<style scoped>
    .combat-entry-display {
        display: flex;
        flex-direction: column;
        /*flex-direction: column-reverse;*/
        overflow-y: scroll;
        height: 100%;
    }
</style>
