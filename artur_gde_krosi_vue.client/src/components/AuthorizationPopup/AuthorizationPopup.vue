<template>
    <div class="authorization-popup">
        <div class="authorization-popup-background" @click="setLogInPopupVisibility(false)"></div>
        <div class="authorization-popup-content">
            <AuthorizationSection :currentMode="logIn.mode" :currentModeText="logIn.name" :anotherWayButtonText="registration.name" :anotherWayMode="registration.mode" :fields="logIn.fields" />
            <AuthorizationSection :currentMode="registration.mode" :currentModeText="registration.name" :anotherWayButtonText="logIn.name" :anotherWayMode="logIn.mode" :fields="registration.fields" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';
    import AuthorizationSection from '@/components/AuthorizationPopup/AuthorizationSection.vue';

    export default defineComponent({
        components: { AuthorizationSection },

        data() {
            return {
                logIn: {
                    name: 'Вход',
                    mode: 'log-in',
                    fields: [
                        { name: 'Логин или E-mail', id: 'login-username-or-email' },
                        { name: 'Пароль', id: 'login-password' }
                    ]
                },

                registration: {
                    name: 'Регистрация',
                    mode: 'registration',
                    fields: [
                        { name: 'Логин', id: 'reg-username' },
                        { name: 'E-mail', id: 'reg-email' },
                        { name: 'Пароль', id: 'reg-password' },
                        { name: 'Подтверждение пароля', id: 'reg-password-confirmation' },
                        { name: 'Имя', id: 'reg-name' },
                        { name: 'Фамилия', id: 'reg-surname' },
                        { name: 'Отчество', id: 'reg-patronymic' },
                    ]
                }
            }
        },

        methods: {
            ...mapMutations(['setLogInPopupVisibility'])
        }
    })
</script> 