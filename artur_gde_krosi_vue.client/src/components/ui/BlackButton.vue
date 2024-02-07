<template>
    <button class="black-button-default">{{ buttonValue }}</button>
</template>

<script lang="ts">
import { defineComponent } from "vue";

    export default defineComponent({
        data() {
            return {
                name: 'black-button'
            }
        },

        props: {
            buttonValue: {
                type: String,
                required: true
            }
        }
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