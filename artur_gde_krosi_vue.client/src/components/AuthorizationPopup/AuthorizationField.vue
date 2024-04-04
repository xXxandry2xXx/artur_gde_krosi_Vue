<template>
    <div class="authorization-popup-field">
        <span><font-awesome-icon :icon="fieldIcon" /></span>
        <DefaultInput v-if="isPassword" type="password" :placeholder="fieldPlaceholder" />
        <DefaultInput v-else :placeholder="fieldPlaceholder" />
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        props: {
            fieldPlaceholder: {
                type: String,
                required: true
            },

            fieldIcon: {
                type: Array,
                required: true
            },

            isPassword: {
                type: Boolean
            }
        },
    })
</script>