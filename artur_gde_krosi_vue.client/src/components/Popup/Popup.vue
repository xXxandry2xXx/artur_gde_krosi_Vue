<template>
    <div class="popup">
        <div class="popup-background" @click="closePopup"></div>
        <div class="popup-content">
            <AuthorizationPopupContent v-if="$store.state.popupMode === 'authorization'" />
            <ChangeEmailConfirmation v-else-if="$store.state.popupMode === 'change-email'" />
            <ChangePasswordConfirmation v-else-if="$store.state.popupMode === 'change-password'" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';
    import AuthorizationPopupContent from '@/components/Popup/Authorization/AuthorizationPopup.vue';
    import ChangeEmailConfirmation from '@/components/Popup/Confirmations/ChangeEmailConfirmation.vue';
    import ChangePasswordConfirmation from '@/components/Popup/Confirmations/ChangePasswordConfirmation.vue';

    export default defineComponent({

        components: { AuthorizationPopupContent, ChangeEmailConfirmation, ChangePasswordConfirmation },

        methods: {
            ...mapMutations(['setPopupVisibility', 'setRegistrationStatus']),

            closePopup() {
                this.setPopupVisibility(false);
                this.setRegistrationStatus(false);
            }
        },
    })
</script> 