<template>
    <div class="preloader_wrapper">
        <div class="preloader">
            <div class="lds-ring">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

    export default defineComponent({
        name: 'Preloader'
    })
</script>

<style>

    .preloader_wrapper {
        width: 100%;
        height: 100%;
        position: absolute;
        background: rgb(255,255,255, 1);
        z-index: 95;
    }

    .preloader {
        display: flex;
        justify-content: center;
        align-items: center;
        position: fixed;
        top: 40%;
        right: 33%;
        left: 25%;
    }

    .lds-ring {
        display: inline-block;
        top: 10%;
        left: 45%;
    }

        .lds-ring div {
            box-sizing: border-box;
            display: block;
            position: absolute;
            width: 90px;
            height: 90px;
            margin: 8px;
            border: 10px solid #ffe742;
            border-radius: 50%;
            animation: lds-ring 1.2s cubic-bezier(0.5, 0, 0.5, 1) infinite;
            border-color: #ffe742 transparent transparent transparent;
        }

            .lds-ring div:nth-child(1) {
                animation-delay: -0.45s;
            }

            .lds-ring div:nth-child(2) {
                animation-delay: -0.3s;
            }

            .lds-ring div:nth-child(3) {
                animation-delay: -0.15s;
            }

    @keyframes lds-ring {
        0% {
            transform: rotate(0deg);
        }

        100% {
            transform: rotate(360deg);
        }
    }

    @media (max-width: 425px) {
        .preloader {
            right: 55%;
        }
    }
</style>