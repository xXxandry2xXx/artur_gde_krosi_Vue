<template>
    <select @change="changeSortOrder" :value="currentOrder">
        <option disabled value="5">Сортировать по</option>
        <option v-for="option in options" :value="option.value">{{ option.name }}</option>
    </select>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import store from '@/store';

    export default defineComponent({
        name: 'DropdownMenu',

        data() {
            return {
                currentOrder: '5',
            }
        },

        props: {
            options: {
                type: Array,
                required: true
            }
        },

        methods: {
            changeSortOrder(this: { currentOrder: string }, event: Event) {
                let target = event.target as HTMLInputElement;
                let value = target.value;

                this.currentOrder = value;
                store.state.selectedFilters.sortOrder = Number(value);
                store.dispatch('applyFilters');
            }
        },

        mounted(this: { currentOrder: number }) {
            this.currentOrder = store.getters.currentSelectedFilters.sortOrder;
        }
    })
</script>