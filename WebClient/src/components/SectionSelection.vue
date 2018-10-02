<template>
    <div>
        <label for="sectionSelection">
            Section Selection
        </label>
        <select id="sectionSelection" v-model="selectedSectionTimestamp" @input="selectSection(Number($event.target.value))">
            <option v-for="section in sections" :value="section.startTime" :key="section.startTime">{{section.startTime}} {{Math.round(section.duration/1000)}} sec</option>
        </select>
    </div>
</template>

<script lang="ts">
    import {Component, Vue, Watch} from 'vue-property-decorator';
    import {TimeSection} from '../models/TimeSection';

    @Component({
        components: {
        },
    })
    export default class SectionSelection extends Vue {

        private selectedSectionTimestamp = 0;

        private get sections(): TimeSection[] {
            return this.$store.state.allSections;
        }

        private selectSection(timestamp: number) {
            const section = this.sections.find((s) => s.startTime === timestamp);

            if (!section) {
                return;
            }

            this.$store.commit('setVisibleSection', section);
        }

        @Watch('$store.state.visibleSection')
        private onVisibleSectionChanges(newVisibleSection: TimeSection) {
            this.selectedSectionTimestamp = newVisibleSection.startTime;
        }
    }
</script>

<style scoped>

</style>
