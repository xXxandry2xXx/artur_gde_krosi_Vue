<template>
    <nav class="app-subheader">
        <AppHeaderUserPanel />
        <div class="app-subheader-search-and-cart">
            <AppSearchPanel />
            <Cart />
        </div>
    </nav>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import AppSearchPanel from '@/components/AppHeader/AppSearchPanel.vue';
    import AppHeaderUserPanel from '@/components/AppHeader/AppHeaderUserPanel.vue';
    import Cart from '@/components/Cart/Cart.vue';

    export default defineComponent({
        components: { AppSearchPanel, AppHeaderUserPanel, Cart },
    })
</script>