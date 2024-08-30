<template>
    <div class="confirmation-content" v-if="isAllowed">
        <!--<h1>E-mail адрес успешно изменен!</h1>
        <p>Отлично! Вы успешно подтвердили новый E-mail адрес.</p>
        <a href="/">На главную</a>-->
        <span>{{ username }}</span>
        <span>{{ email }}</span>
        <span>{{ token }}</span>
    </div>
    <div v-else>
        <Preloader />
        <div style="height: 400px;"></div>
    </div>
</template>
<script lang="ts">
    import axios from 'axios';
    import apiUrl from '@/helper'
    import { defineComponent } from 'vue';

    export default defineComponent({
        props: [ 'username', 'email', 'token'],

        data() {
            return {
                isAllowed: true
            }
        },

        methods: {
            async confirmNewEmail(this: any) {
                try {
                    const response = await axios.put(
                        apiUrl + '/identity/SetingsUser/RegEmeil',
                        '',
                        {
                            params: {
                                'email': this.email,
                                'token': this.token
                            },
                            headers: {
                                'accept': '*/*'
                            }
                        }
                    );
                    if (response.status === 200) this.isAllowed = true;
                } catch (error) {
                    this.$router.push('/');
                }
            }
        },

        mounted() {
            //this.confirmNewEmail();
        }
    })
</script>