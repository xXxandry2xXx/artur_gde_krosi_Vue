<template>
    <button class="cart-button-default">
        <div class="cart-button-icon">
            <font-awesome-icon :icon="['fas', 'cart-shopping']" />
            <span><font-awesome-icon :icon="['fas', 'plus']" /></span>
        </div>
        <span class="cart-button-price">
            <slot></slot>
            <span>руб.</span>
        </span>
    </button>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        name: 'CartButton'
    })
</script>

<style>
    .cart-button-default {
        display: flex;
        align-items: center;
        background: none;
        box-sizing: border-box;
        border-radius: 10px;
        border: 1px solid var(--border-font-color);
        color: var(--border-font-color);
        transition: 0.2s;
    }

    .cart-button-icon {
        position: relative;
        height: 100%;
        border-top-left-radius: 6px;
        border-bottom-left-radius: 6px;
        box-sizing: border-box;
        padding: 15px;
        padding-top: 20px;
        padding-right: 20px;
        position: relative;
        font-size: 45px;
        color: #fff;
        background: var(--border-font-color);
        transition: 0.2s;
    }

        .cart-button-icon span {
            position: absolute;
            transition: 0.2s;
            top: 26px;
            left: 35px;
            color: var(--border-font-color);
            font-size: 21px;
        }

    .cart-button-price {
        padding: 15px;
        font-size: 50px;
        font-weight: 800;
        color: var(--border-font-color);
        transition: 0.2s;
    }

        .cart-button-price span {
            font-size: 45px;
            font-weight: 500;
            margin-left: 5px;
        }

    .cart-button-default-unavailable {
        color: var(--additional-font-color);
        border: 1px solid var(--additional-font-color);
        background: var(--additional-font-color-lighter);
    }

    @media (hover: hover) {
        .cart-button-default:hover {
            cursor: pointer;
        }

            .cart-button-default:hover .cart-button-price {
                color: #fff;
                background: var(--border-font-color);
            }

        .cart-button-default-unavailable:hover {
            cursor: not-allowed;
            background: var(--additional-font-color-lighter);
        }
    }

    @media (hover: none) {
        .cart-button-default:active {
            cursor: pointer;
        }

            .cart-button-default:active .cart-button-price {
                color: #fff;
                background: var(--border-font-color);
            }

        .cart-button-default-unavailable:active {
            cursor: not-allowed;
            background: var(--additional-font-color-lighter);
        }
    }

    @media (max-width: 2560px) {

        .cart-button-default,
        .cart-button-default-unavailable {
            border: 3px solid var(--border-font-color);
        }

        .cart-button-price {
            border-left: 3px solid var(--border-font-color);
        }
    }

    @media (max-width: 768px) {

        .cart-button-default,
        .cart-button-default-unavailable {
            border: 3px solid var(--border-font-color);
        }

        .cart-button-price {
            border-left: 3px solid var(--border-font-color);
        }
    }

    @media (max-width: 425px) {

        .cart-button-icon {
            padding: 10px;
            padding-top: 20px;
            padding-right: 15px;
            font-size: 36px;
        }

            .cart-button-icon span {
                font-size: 15px;
                top: 27px;
                left: 27px;
            }

        .cart-button-price {
            padding: 10px 20px;
            font-size: 45px;
        }

            .cart-button-price span {
                font-size: 38px;
            }
    }

    @media (max-width: 375px) {
        .cart-button-icon {
            padding: 9px;
            padding-top: 11px;
            padding-right: 8px;
            font-size: 31px;
        }

        .cart-button-price {
            padding: 5px 15px;
            font-size: 40px;
        }

        .cart-button-icon span {
            font-size: 13px;
            top: 16px;
            left: 23px;
        }
    }
</style>