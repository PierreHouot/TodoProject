<script setup lang="ts">
import type { Activity } from '@/models/activity';
import type { PropType } from 'vue';

defineProps({
    activity: { required: true, type: Object as PropType<Activity> }
});

</script>

<template>
    <div class="border rounded m-2 p-1 w-80">
        <div>{{ activity.name }}</div>
        <div>{{ activity.isCompleted }}</div>
    </div>
</template>

<style scoped></style>
