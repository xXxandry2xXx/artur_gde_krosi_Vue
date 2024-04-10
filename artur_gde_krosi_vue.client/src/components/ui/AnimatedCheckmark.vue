<template>
    <div class="checkmark-wrapper">
        <span class="checkmark"></span>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

    export default defineComponent({
        name: 'AnimatedCheckmark'
    })

</script>

<style scoped>
    .checkmark-wrapper {
        display: flex;
        justify-content: end;
        width: 200px;
        height: 190px;
        position: relative;
    }

        .checkmark-wrapper::after {
            content: "";
            display: block;
            border: 6px solid #c3c3c3;
            border-radius: 50%;
            width: 100%;
            height: 100%;
        }

        .checkmark-wrapper::before {
            content: "";
            display: block;
            border: 6px solid transparent;
            border-left-color: #D2A805;
            position: absolute;
            border-radius: 50%;
            width: 94%;
            height: 100%;
            transition: 0.2s;
            animation: circle-d2a42502 linear forwards 1s;
        }

    .checkmark {
        width: 100px;
        height: 50px;
        opacity: 0;
        border-left: 6px solid #D2A805;
        border-bottom: 6px solid #D2A805;
        transform-origin: left top;
        transform: rotate(-50deg);
        position: absolute;
        left: 22%;
        top: 57%;
        animation: checkmark-d2a42502 linear forwards 1s;
    }

    @keyframes circle {
        0% {
            border-color: transparent;
            border-left-color: #D2A805;
        }

        90% {
            transform: rotate(-360deg);
            border-color: transparent;
            border-left-color: #D2A805;
        }

        100% {
            transform: rotate(-360deg);
            border-color: #D2A805;
        }
    }

    @keyframes checkmark {
        0% {
            width: 0;
            height: 0;
            opacity: 0;
        }

        80% {
            width: 0;
            height: 0;
            opacity: 0;
        }

        90% {
            width: 0;
            height: 50px;
            opacity: 1;
        }

        100% {
            width: 100px;
            height: 50px;
            opacity: 1;
        }
    }
</style>