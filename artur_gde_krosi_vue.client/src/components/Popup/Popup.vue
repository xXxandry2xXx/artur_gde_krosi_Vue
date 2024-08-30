<template>
    <div class="popup">
        <div class="popup-background" @click="closePopup"></div>
        <div class="popup-content">
            <AuthorizationPopupContent v-if="$store.state.popupMode === 'authorization'" />
            <ChangeEmailConfirmation v-else-if="$store.state.popupMode === 'change-email'" />
            <ChangePasswordConfirmation v-else-if="$store.state.popupMode === 'change-password'" />
            <MailSuccesfulySended v-else-if="$store.state.popupMode === 'mail-succesfuly-sended'" />
            <CharasteristicAddingForm v-else-if="$store.state.popupMode === 'add-new-char'"/>
            <CharasteristicValueAddingForm v-else-if="$store.state.popupMode === 'add-char-value'" />
            <CharasteristicEditingForm v-else-if="$store.state.popupMode === 'edit-char'" />
            <CharacteristicRemoveConfirmation v-else-if="$store.state.popupMode === 'sure-to-remove-char'" />
            <WhyItIsDisabled v-else-if="$store.state.popupMode === 'why-disabled'" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';
    import AuthorizationPopupContent from '@/components/Popup/Authorization/AuthorizationPopup.vue';
    import ChangeEmailConfirmation from '@/components/Popup/Confirmations/ChangeEmailConfirmation.vue';
    import ChangePasswordConfirmation from '@/components/Popup/Confirmations/ChangePasswordConfirmation.vue';
    import MailSuccesfulySended from '@/components/Popup/MailSuccesfulySended.vue';
    import CharasteristicAddingForm from '@/components/Popup/Characteristics/CharasteristicAddingForm.vue';
    import CharasteristicEditingForm from '@/components/Popup/Characteristics/CharacteristicEditingForm.vue';
    import CharasteristicValueAddingForm from '@/components/Popup/Characteristics/CharacteristicValueAddingForm.vue';
    import CharacteristicRemoveConfirmation from '@/components/Popup/Characteristics/CharacteristicRemoveConfirmation.vue';
    import WhyItIsDisabled from '@/components/Popup/WhyItIsDisabled.vue';

    export default defineComponent({

        components: {
            AuthorizationPopupContent,
            ChangeEmailConfirmation,
            ChangePasswordConfirmation,
            MailSuccesfulySended,
            CharasteristicAddingForm,
            CharasteristicEditingForm,
            CharasteristicValueAddingForm,
            CharacteristicRemoveConfirmation,
            WhyItIsDisabled
        },

        methods: {
            ...mapMutations(['setPopupVisibility', 'setRegistrationStatus']),

            closePopup() {
                this.setPopupVisibility(false);
                this.setRegistrationStatus(false);
            }
        },
    })
</script> 