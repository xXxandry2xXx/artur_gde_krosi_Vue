<template>
    <AppHeaderMain />
    <AppSubheader />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import AppSubheader from '@/components/AppHeader/AppSubheader.vue';
    import AppHeaderMain from '@/components/AppHeader/AppHeaderMain.vue';

    export default defineComponent({
        components: { AppSubheader, AppHeaderMain }
    })
</script>