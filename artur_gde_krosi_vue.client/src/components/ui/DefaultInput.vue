<template>
    <input type="text" :placeholder="placeholderValue"/>
</template>

<script lang="ts">
import { defineComponent } from "vue";

    export default defineComponent({
        data() {
            return {
                name: 'default-input'
            }
        },

        props: {
            placeholderValue: {
                type: String,
                required: true
            }
        }
    })
</script>

