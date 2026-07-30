<script setup lang="ts">
import ActivityCard from '@/component/ActivityCard.vue';
import { useActivityStore } from '@/stores/activity';
import { storeToRefs } from 'pinia';
import { onMounted } from 'vue';
const store = useActivityStore()
const { activities } = storeToRefs(store)

// const Props|props = defineProps<Props>();
onMounted(async () => {
    await store.fetchActivities()
})
</script>

<template>
    <div :key="activity.id" v-for="activity in activities">
        <ActivityCard :activity="activity" />
    </div>
</template>
