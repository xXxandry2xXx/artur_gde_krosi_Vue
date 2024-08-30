<template>
    <div class="confirmation-content" v-if="isAllowed">
        <AnimatedCheckmark />
        <h1>E-mail адрес успешно подтвержден!</h1>
        <p>Отлично! Вы успешно подтвердили свой E-mail адрес.</p>
        <a href="/">На главную</a>
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
        props: ['email', 'token'],

        data() {
            return {
                isAllowed: false
            }
        },

        methods: {
            async confirmEmail(this: any) {
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
           this.confirmEmail();
        }
    })
</script>