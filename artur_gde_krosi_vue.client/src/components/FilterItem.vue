<template>
    <div class="filter-item-container">
        <label class="filter-item">
            <input class="filter-item-checkbox" type="checkbox" />
            <span class="filter-item-checkbox-fake"></span>
            <span v-if="item.name" class="filter-item-name">{{ item.name }}</span>
            <span v-else class="filter-item-name">{{ item }}</span>
        </label>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        props: {
            item: {
                type: [Object, Number],
                required: true
            }
        }
    })
</script>