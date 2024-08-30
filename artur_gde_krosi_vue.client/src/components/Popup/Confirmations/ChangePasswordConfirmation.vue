<template>
    <div class="change-data-confirmation-popup">
        <h3>Смена пароля</h3>
        <p>На ваш E-mail придет письмо с ссылкой для смены пароля. Чтобы отправить его, нажмите на кнопку ниже</p>
        <BorderedButton class="confirm-popup-changes-button" @click="sendMessageAndRenderSucces()">Отправить письмо</BorderedButton>
    </div>
</template>

<script lang="ts">
    import axios from 'axios';
    import apiUrl from '@/helper'
    import { defineComponent } from 'vue';
    import { mapMutations, mapGetters } from 'vuex';

    export default defineComponent({

        methods: {
            ...mapGetters(['getAuthorizedUser']),
            ...mapMutations(['setPopupMode']),

            sendMessageAndRenderSucces(this: any) {
                let email = this.getAuthorizedUser().email;
                this.sendMessageForPasswordChanging(email).then((response: any) => {
                    if (response.status === 200) this.setPopupMode('mail-succesfuly-sended');
                });
            },

            async sendMessageForPasswordChanging(email: any) {
                try {
                    const response = await axios.get(apiUrl + '/identity/SetingsUser/GenerateTokenOnPasswordReset', {
                        params: {
                            'email': email
                        },
                        headers: {
                            'accept': '*/*'
                        }
                    });

                    return response;
                } catch (error) {
                    console.log(error);
                }
            }
        },
    })
</script>
