<template>
    <div class="confirmation-content">
        <h1>E-mail адрес успешно подтвержден!</h1>
        <p>Отлично! Вы успешно подтвердили свой E-mail адрес.</p>
        <a href="/">На главную</a>
    </div>
</template>
<script lang="ts">
    import axios from 'axios';
    import { defineComponent } from 'vue';

    export default defineComponent({
        props: ['email', 'token'],

        methods: {
            async confirmEmail(this: any) {
                try {
                    const response = await axios.put(
                        'http://localhost:5263/api/identity/SetingsUser/RegEmeil',
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
                } catch(error) {
                    this.$router.push('/');
                }
            }
        },

        mounted() {
            this.confirmEmail();
        }
    })
</script>