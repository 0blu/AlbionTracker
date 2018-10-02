<template>
    <div id="app">
        <div class="top-content">
            <SectionSelection />
            <OptionMenu />
            {{clusterName}}
        </div>
        <div class="main-content">
            <BarDiagram />
        </div>
        <div class="side-content">
            <CombatLog />
        </div>
        <div class="bottom-content">
            <!--<FarmStats />-->
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import SectionSelection from './components/SectionSelection.vue';
import BarDiagram from './components/BarDiagram.vue';
import FarmStats from './components/FarmStats.vue';
import OptionMenu from './components/OptionMenu.vue';
import CombatLog from './components/CombatLog.vue';

@Component({
    components: {
        CombatLog,
        OptionMenu,
        FarmStats,
        BarDiagram,
        SectionSelection,
    },
})
export default class App extends Vue {

    private get clusterName(): string {
        const currentCluster = this.$store.state.currentCluster;
        if (!currentCluster) {
            return '';
        }
        return currentCluster.clusterName;
    }

}
</script>

<style>
    #app {
        font-family: 'Roboto', sans-serif;

        display: grid;
        height: 100%;
        grid-template-columns: 3fr 1fr;
        grid-template-rows: auto 1fr 1fr;
        grid-gap: 1px 1px;

        grid-template-areas:
                "top    top"
                "main   side"
                "bottom side";
    }

    .top-content {
        grid-area: top;
    }
    .main-content {
        grid-area: main;
    }
    .side-content {
        grid-area: side;
    }
    .bottom-content {
        grid-area: bottom;
    }
</style>
