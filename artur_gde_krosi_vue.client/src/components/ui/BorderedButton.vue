<template>
    <button class="bordered-button-default">
        <slot></slot>
    </button>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        name: 'BorderedButton'
    })
</script>

<style>
    .bordered-button-default {
        background: none;
        border: 1px solid var(--border-font-color);
        color: var(--border-font-color);
        transition: 0.2s;
    }

        .bordered-button-default:hover {
            cursor: pointer;
            background: var(--featured-hover-color);
        }

    .bordered-button-default-unavailable {
        color: var(--additional-font-color);
        border: 1px solid var(--additional-font-color);
        background: var(--additional-font-color-lighter);
    }

        .bordered-button-default-unavailable:hover {
            cursor: not-allowed;
            background: var(--additional-font-color-lighter);
        }
</style>