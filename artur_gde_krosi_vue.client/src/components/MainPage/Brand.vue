<template>
    <div class="main-page-brand">
        <img :src="brand.imageSrc" :alt="brand.name" />
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        props: {
            brand: {
                name: String,
                imageSrc: String,
                required: true
            }
        }
    })
</script>