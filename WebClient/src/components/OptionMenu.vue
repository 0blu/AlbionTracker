<template>
    <div>
        <label for="anonymousMode">Anonymize player names</label>
        <input id="anonymousMode" type="checkbox" v-model="config.anonymizeNames" />
    </div>
</template>

<script lang="ts">
    import {Component, Vue, Watch} from 'vue-property-decorator';
    import {ConfigModel} from '../models/ConfigModel';

    @Component({
        components: {
        },
    })
    export default class OptionMenu extends Vue {

        get config(): ConfigModel {
            return this.$store.state.config;
        }

    }
</script>

<style scoped>

</style>
