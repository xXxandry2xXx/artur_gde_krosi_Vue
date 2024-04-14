<template>
    <Preloader v-show="$store.state.showPreloader" />

    <transition name="fade">
        <AuthorizationPopup v-show="$store.state.authorization.showLogInPopup" />
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
    import { mapMutations, mapGetters } from 'vuex';
    import AppHeader from '@/components/AppHeader/AppHeader.vue';
    import AppFooter from '@/components/AppFooter.vue';
    import AuthorizationPopup from '@/components/AuthorizationPopup/AuthorizationPopup.vue';

    export default defineComponent({
        components: { AppHeader, AppFooter, AuthorizationPopup },

        methods: {
            ...mapMutations(['setUser']),
            ...mapGetters(['getAuthorizedUser']),

            setCurrentUser() {
                let user = this.getAuthorizedUser();
                this.setUser(user);
            }
        },

        beforeMount() {
            this.setCurrentUser();
        }
    })
</script>