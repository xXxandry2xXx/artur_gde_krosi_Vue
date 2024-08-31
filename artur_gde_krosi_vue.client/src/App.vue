<template>
    <Preloader v-show="$store.state.showPreloader" />

    <transition name="fade">
        <Popup v-if="$store.state.showPopup" />
    </transition>

    <transition name="fade" v-show="isTablet() && isFiltersPanelShown()" @click="setFiltersPanelVisibility(false)">
        <div class="filter-panel-background"></div>
    </transition>

    <AppHeader />

    <main class="main-content">
        <router-view>

        </router-view>
    </main>

    <AppFooter />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapGetters, mapActions } from 'vuex';
    import AppHeader from '@/components/AppHeader/AppHeader.vue';
    import AppFooter from '@/components/AppFooter.vue';
    import Popup from '@/components/Popup/Popup.vue';

    export default defineComponent({
        components: { AppHeader, AppFooter, Popup },

        methods: {
            ...mapActions(['fetchUserCart', 'getLocalTotalPrice', 'fetchProducts']),
            ...mapMutations(['setUser', 'setFiltersPanelVisibility']),
            ...mapGetters(['getAuthorizedUser', 'isTablet', 'isFiltersPanelShown']),

            setCurrentUser() {
                let user = this.getAuthorizedUser();
                this.setUser(user);
            },
        },

        beforeMount() {
            this.setCurrentUser();
            this.fetchProducts();
        }
    })
</script>