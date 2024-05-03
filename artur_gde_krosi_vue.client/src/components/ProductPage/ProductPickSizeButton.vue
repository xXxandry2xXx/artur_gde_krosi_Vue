<template>
    <button class="product-page-size-box" @click="setCurrentChosenVariantId(currentVariant.variantId)">
        {{ currentVariant.shoeSize }}
    </button>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapActions, mapMutations } from 'vuex';

    export default defineComponent({

        props: {
            currentVariant: {
                type: Object,
                reqired: true
            }
        },

        methods: {
            ...mapMutations(['setCurrentChosenVariantId']),
        }
    })
</script>