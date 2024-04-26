<template>
    <div class="authorization-popup-fields-handling" v-if="!$store.state.authorization.succesfulyRegistered.status">
        <LogInSection v-show="$store.state.authorization.loginPopupMode === 'log-in'" />
        <RegistrationSection v-show="$store.state.authorization.loginPopupMode === 'registration'" />
    </div>
    <div class="authorization-popup-succes" v-else>
        <AnimatedCheckmark />
        <p>Вы успешно зарегистрировались! Чтобы войти в свой аккаунт необходимо подтвердить почту. На указанную Вами почту было отправлено письмо с подтверждением.</p>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import LogInSection from '@/components/Popup/Authorization/AuthorizationLoginSection.vue';
    import RegistrationSection from '@/components/Popup/Authorization/AuthorizationRegistrationSection.vue';

    export default defineComponent({
        components: { LogInSection, RegistrationSection },
    })
</script>