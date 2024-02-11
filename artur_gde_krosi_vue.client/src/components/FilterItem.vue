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

<style>
    .filter-item {
        display: flex;
        align-items: center;
    }

    .filter-item:hover {
        cursor: pointer;
    }

    .filter-item-container {
        width: 100%;
        margin: 10px 0;
    }

    .filter-item-name {
        font-size: 20px;
    }

    .filter-item-checkbox {
        display: none;
    }

    .filter-item-checkbox-fake {
        position: relative;
        display: inline-block;
        width: 17px;
        height: 17px;
        border-radius: 3px;
        border: 1px solid rgb(185 147 0);
    }

        .filter-item-checkbox-fake::after {
            content: '';
            position: absolute;
            display: none;
            border-right: 3px solid rgb(185 147 0);
            border-bottom: 3px solid rgb(185 147 0);
            right: 4px;
            bottom: 3px;
            width: 6px;
            height: 11px;
            transform: rotate(35deg);
        }

    .filter-item-checkbox:checked ~ .filter-item-checkbox-fake {
        background: rgb(241 221 83);
    }

    .filter-item-checkbox:checked ~ .filter-item-checkbox-fake::after {
        display: block;
    }

    .filter-item-checkbox-fake {
        margin-right: 10px;
    }
</style>