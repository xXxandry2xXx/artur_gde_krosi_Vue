<template>
    <div class="bordered-button-default">
        <slot></slot>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        name: 'BorderedButton'
    })
</script>

<style>
    .bordered-button-default {
        display: flex;
        justify-content: center;
        align-items: center;
        box-sizing: border-box;
        background: none;
        border-radius: 5px;
        border: 1px solid var(--border-font-color);
        color: var(--border-font-color);
        transition: 0.2s;
    }



    .bordered-button-default-unavailable {
        color: var(--additional-font-color);
        border: 1px solid var(--additional-font-color);
        background: var(--additional-font-color-lighter);
    }



    @media (hover: hover) {

        .bordered-button-default:hover {
            cursor: pointer;
            color: #fff;
            background: var(--border-font-color);
        }

        .bordered-button-default-unavailable:hover {
            cursor: not-allowed;
            color: var(--additional-font-color);
            background: var(--additional-font-color-lighter);
        }
    }

    @media (hover: none) {

        .bordered-button-default:active {
            cursor: pointer;
            color: #fff;
            background: var(--border-font-color);
        }

        .bordered-button-default-unavailable:active {
            cursor: not-allowed;
            color: var(--additional-font-color);
            background: var(--additional-font-color-lighter);
        }
    }

    @media (max-width: 2560px) {

        .bordered-button-default {
            border: 3px solid var(--border-font-color);
        }

        .bordered-button-default-unavailable {
            color: var(--additional-font-color);
            border: 3px solid var(--additional-font-color);
            background: var(--additional-font-color-lighter);
        }
    }

    @media (max-width: 768px) {

        .bordered-button-default {
            border: 2px solid var(--border-font-color);
        }

        .bordered-button-default-unavailable {
            color: var(--additional-font-color);
            border: 2px solid var(--additional-font-color);
            background: var(--additional-font-color-lighter);
        }
    }
</style>