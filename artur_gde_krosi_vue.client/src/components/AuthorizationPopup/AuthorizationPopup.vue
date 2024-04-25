<template>
    <div class="authorization-popup">
        <div class="authorization-popup-background" @click="setAuthorizationPopupVisibility(false)"></div>
        <div class="authorization-popup-content">
            <div class="authorization-popup-fields-handling" v-if="!$store.state.authorization.succesfulyRegistered.status">
                <LogInSection />
                <RegistrationSection />
            </div>
            <div class="authorization-popup-succes" v-else>
                <AnimatedCheckmark />
                <p>Вы успешно зарегистрировались! Чтобы войти в свой аккаунт необходимо подтвердить почту. На указанную Вами почту было отправлено письмо с подтверждением.</p>
                <!--<p>
                    Вы успешно зарегистрировались! Чтобы войти в свой аккаунт необходимо подтвердить почту.
                    На указанную Вами почту было отправлено письмо с подтверждением.
                    Пожалуйста, перейдите в <a href="userEmailDomen">Ваш почтовый клиент</a> и подтвердите почту.
                </p>-->
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';
    import LogInSection from '@/components/AuthorizationPopup/AuthorizationLoginSection.vue';
    import RegistrationSection from '@/components/AuthorizationPopup/AuthorizationRegistrationSection.vue';

    export default defineComponent({

        components: { LogInSection, RegistrationSection },

        methods: {
            ...mapMutations(['setAuthorizationPopupVisibility']), 
        },

        //computed: {
        //    userEmailDomen(this: any) {
        //        let email = this.$store.state.authorization.registrationUserData.email;
        //        let regex = /@([^.@]+)\.[^.@]+$/;
        //        let match = email.match(regex);

        //        if (match) return match[1];
        //        return '#';
        //    }
        //},
    })
</script> 