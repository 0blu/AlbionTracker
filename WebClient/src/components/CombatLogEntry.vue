<template>
    <div class="entry">
        <span>{{entry.causer | getEntityName}} dealt {{entry.healthChange}} to {{entry.target | getEntityName}}</span>
    </div>
</template>

<script lang="ts">
import {Component, Prop, Vue} from 'vue-property-decorator';
import {Entity} from '../models/Entity';

@Component({
    components: {
    },
})
export default class CombatLogEntry extends Vue {
    @Prop()
    private entry!: Entity;
}
</script>

<style scoped>
    .entry {
        height: 3em;
        border: 1px solid black;
        border-radius: 4%;
        animation-name: height;
        animation-duration: 4s;
    }
</style>
