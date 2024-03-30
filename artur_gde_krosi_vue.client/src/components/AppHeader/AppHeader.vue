<template>
    <AppMain />
    <AppSubheader />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import AppSubheader from '@/components/AppHeader/AppSubheader.vue';
    import AppMain from '@/components/AppHeader/AppMain.vue';

    export default defineComponent({
        components: { AppSubheader, AppMain }
    })
</script>