<template>
    <select @change="changeSortOrder" :value="currentOrder">
        <option v-for="option in options" :value="option.value">{{ option.name }}</option>
    </select>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapMutations, mapActions, mapGetters } from 'vuex';

    export default defineComponent({

        data() {
            return {
                currentOrder: this.$store.getters.currentSelectedFilters.sortOrder,
            }
        },

        props: {
            options: {
                type: Array,
                required: true
            }
        },

        methods: {
            ...mapMutations(['setSelectedSortingOrder']),
            ...mapActions(['applyFilters']),
            ...mapGetters(['currentSelectedFilters']),
            
            changeSortOrder(this: any, event: Event) {
                let target = event.target as HTMLInputElement;
                let value = target.value;

                this.setSelectedSortingOrder(value);
                this.applyFilters();
            }
        },
    })
</script>