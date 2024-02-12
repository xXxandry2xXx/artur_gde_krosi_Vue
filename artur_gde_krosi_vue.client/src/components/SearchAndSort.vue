<template>
    <div class="search-and-sort">
        <div class="search-input-wrapper">
            <DefaultInput class="search-input" placeholder="Введите название или бренд..." />
            <button class="search-button"><i class="fas fa-search"></i></button>
        </div>
        <DropdownMenu class="sort-dropdown" :options="sortingOptions"/>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        props: {
            sortingOptions: {
                type: Array,
                required: true
            }
        }
    })
</script>