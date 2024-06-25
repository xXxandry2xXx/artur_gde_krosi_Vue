<template>
    <label class="default-checkbox-wrapper">
        <input type="checkbox" class="default-checkbox" />
        <span class="default-checkbox-fake"></span>
        <span class="default-checkbox-text"><slot></slot></span>
    </label>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        name: 'CheckboxItem'
    })
</script>

<style>
    .default-checkbox-wrapper {
        display: flex;
        align-items: center;
        justify-content: center;
    }

        .default-checkbox-wrapper:hover {
            cursor: pointer;
        }

    .default-checkbox {
        display: none;
    }

    .default-checkbox-fake {
        position: relative;
        display: inline-block;
        min-width: 20px;
        min-height: 20px;
        max-width: 20px;
        max-height: 20px;
        border: 1px solid var(--border-font-color);
    }

        .default-checkbox-fake::after {
            content: '';
            position: absolute;
            display: none;
            border-right: 3px solid rgb(185 147 0);
            border-bottom: 3px solid rgb(185 147 0);
            right: 5px;
            bottom: 4px;
            width: 7px;
            height: 13px;
            transform: rotate(40deg);
        }

    .default-checkbox:checked ~ .default-checkbox-fake::after {
        display: block;
    }

    .default-checkbox-fake {
        margin-right: 10px;
    }
    .default-checkbox-wrapper span {
        user-select: none;
    }

    @media (width: 2560px) {
        .default-checkbox-fake {
            min-width: 60px;
            min-height: 60px;
            border: 3px solid var(--border-font-color);
        }

            .default-checkbox-fake::after {
                border-right: 10px solid rgb(185 147 0);
                border-bottom: 10px solid rgb(185 147 0);
                right: 14px;
                bottom: 11px;
                width: 18px;
                height: 36px;
            }
    }
</style>