<script setup lang="ts">
import type { Activity } from '@/models/activity';
import type { PropType } from 'vue';

defineProps({
    activity: { required: true, type: Object as PropType<Activity> }
});

</script>

<template>
    <div class=" w-full m-2 p-1">
        <div class="bg-dark text-accent p-2 font-black rounded-t-xl text-center">{{ activity.name }}</div>
        <div class="bg-block text-dark p-2 rounded-b-xl">{{ activity.description }}</div>
    </div>
</template>
