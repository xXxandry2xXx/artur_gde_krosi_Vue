<template>
    <select @change="changeSortOrder" :value="currentOrder">
        <option disabled>Сортировать по</option>
        <option v-for="option in options" :value="option.value">{{ option.name }}</option>
    </select>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { getSelectedFiltersFromLocalStorage } from '@/helper';
    import store from '@/store';

    export default defineComponent({
        name: 'DropdownMenu',

        data() {
            return {
                currentOrder: '0',
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

                let selectedFilters = getSelectedFiltersFromLocalStorage();

                this.currentOrder = value;
                selectedFilters.sortOrder = value;
                localStorage.setItem('selectedFilters', JSON.stringify(selectedFilters));
                store.dispatch('applyFilters');
            }
        },

        mounted(this: { currentOrder: number }) {
            let selectedFilters = getSelectedFiltersFromLocalStorage();
            this.currentOrder = selectedFilters.sortOrder;
        }
    })
</script>