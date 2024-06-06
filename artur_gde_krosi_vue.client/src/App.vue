<template>
    <Preloader v-show="$store.state.showPreloader" />

    <transition name="fade">
        <Popup v-if="$store.state.showPopup" />
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
            ...mapActions(['fetchUserCart']),
            ...mapMutations(['setUser']),
            ...mapGetters(['getAuthorizedUser']),

            setCurrentUser() {
                let user = this.getAuthorizedUser();
                this.setUser(user);
            },

            checkIfUserExists() {

            }
        },

        beforeMount() {
            this.setCurrentUser();
            this.fetchUserCart();
        }
    })
</script>