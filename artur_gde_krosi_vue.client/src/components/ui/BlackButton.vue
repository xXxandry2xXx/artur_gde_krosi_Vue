<template>
    <button class="black-button-default">
        <slot></slot>
    </button>
</template>

<script lang="ts">
import { defineComponent } from "vue";

    export default defineComponent({
        name: 'BlackButton'
    })
</script>

<style>
    .black-button-default {
        background: #000;
        color: #fff;
        border: none;
        padding: 10px 25px;
        font-size: 17px;
        font-weight: 600;
        border-radius: 7px;
    }
</style>