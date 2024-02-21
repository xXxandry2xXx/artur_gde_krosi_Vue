<template>
    <input class="input-default" type="text" @input="$emit('update:modelValue', $event.target.value)"/>
</template>

<script lang="ts">
import { defineComponent } from "vue";

    export default defineComponent({
        name: 'DefaultInput',

        props: {
            modelValue: String
        }
    })
</script>