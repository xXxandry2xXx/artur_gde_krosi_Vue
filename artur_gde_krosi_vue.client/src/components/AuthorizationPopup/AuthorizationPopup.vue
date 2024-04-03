<template>
    <div class="authorization-popup">
        <div class="authorization-popup-background" @click="setLogInPopupVisibility(false)"></div>
        <div class="authorization-popup-content">
            </div>

            <div>
                <div class="authorization-section" v-show="$store.state.loginPopupMode === 'log-in'">
                    <div class="authorization-section-fields">
                        <AuthorizationField :fieldPlaceholder="'Логин'" :fieldIcon="['fas', 'user']" />
                        <AuthorizationField :fieldPlaceholder="'Пароль'" :fieldIcon="['fas', 'key']" />
                    </div>
                    <BorderedButton class="authorization-popup-button">Вход</BorderedButton>
                </div>

                <div class="authorization-section" v-show="$store.state.loginPopupMode === 'registration'">
                    <div class="authorization-section-fields">
                        <AuthorizationField :fieldPlaceholder="'E-mail'" :fieldIcon="['fas', 'at']" />
                        <AuthorizationField :fieldPlaceholder="'Логин'" :fieldIcon="['fas', 'user']" />
                        <div class="authorization-personal-data">
                            <AuthorizationField :fieldPlaceholder="'Имя'" :fieldIcon="['fas', 'user']" />
                            <AuthorizationField :fieldPlaceholder="'Фамилия'" :fieldIcon="['fas', 'user']" />
                        </div>
                        <AuthorizationField :fieldPlaceholder="'Отчество'" :fieldIcon="['fas', 'user']" />
                        <AuthorizationField :fieldPlaceholder="'Пароль'" :fieldIcon="['fas', 'key']" />
                        <AuthorizationField :fieldPlaceholder="'Подтверждение пароля'" :fieldIcon="['fas', 'key']" />
                    </div>
                    <BorderedButton class="authorization-popup-button">Регистрация</BorderedButton>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
     import { mapMutations } from 'vuex'
 import AuthorizationField from '@/components/AuthorizationPopup/AuthorizationField.vue'

        export default defineComponent({
        components: { AuthorizationField },

        methods: {
            ...mapMutations(['setLogInPopupVisibility', 'openLoginPopup'])
        }
    })

</script> 

</script> 