<template>

    <div>
        <h1 class="title">
            <a :name="commandModule.name" :href="'#' + commandModule.name"
                v-on:click.prevent="emitClick(commandModule)">
                {{commandModule.name}}
            </a>
        </h1>
        <h2 class="subtitle">
            {{commandModule.summary}}
        </h2>
        <ul>
            <CommandView v-for="(commandGroup, key) in overloads(commandModule.commands)" :key="key" :commandGroup="commandGroup" />
        </ul>
        <br />
    </div>

</template>

<style scoped>
.title
{
    text-transform: capitalize;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { ModuleHelpData, CommandHelpData } from '@/models/ModuleHelpData';
import CommandView from '@/components/Commands/CommandView.vue';
import * as _ from 'lodash';

@Component({
    components:
    {
        CommandView
    },
})
export default class ModuleView extends Vue
{
    @Prop() private commandModule!: ModuleHelpData;

    emitClick(module: ModuleHelpData)
    {
        this.$emit('moduleClicked', module);
    }

    overloads(commands: CommandHelpData[])
    {
        return _.groupBy(commands, command => command.name);
    }
}
</script>
