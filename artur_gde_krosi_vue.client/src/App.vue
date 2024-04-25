<template>
    <Preloader v-show="$store.state.showPreloader" />

    <transition name="fade">
        <Popup v-show="$store.state.showPopup" />
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
    import Popup from '@/components/Popup/Popup.vue';

    export default defineComponent({
        components: { AppHeader, AppFooter, Popup },

        methods: {
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
        }
    })
</script>