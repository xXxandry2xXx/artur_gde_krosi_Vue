<template>
    <div class="authorization-popup-field">
        <span><font-awesome-icon :icon="fieldIcon" /></span>
        <DefaultInput :placeholder="fieldPlaceholder" />
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
            }
        }
    })
</script>