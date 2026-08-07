<script setup lang="ts">
import { computed, onMounted } from 'vue';
import ActivityCard from '@/component/ActivityCard.vue';
import { storeToRefs } from 'pinia';
import { useActivityStore } from '@/stores/activity';
const store = useActivityStore();
const { activities } = storeToRefs(store);

onMounted(async () => {
  await store.fetchActivities();
});

const hasActivities = computed(() => activities.value && activities.value.length > 0);

const showYear = (index: number) => {
  if (!activities.value) return false;
  if (index === 0) return true;
  return activities.value[index]?.date.year !== activities.value[index - 1]?.date.year;
};
</script>

<template>
  <div class="flex w-96">
    <div
      v-if="hasActivities"
      class="overflow-y-scroll h-full px-2 border-l-2 border-surface"
    >
      <div
        :key="activity.id"
        v-for="(activity, id) in activities"
      >
        <div
          v-if="showYear(id)"
          class="flex items-center mt-2"
          :id="activity.date.year.toString()"
        >
          <div class="font-title text-surface">{{ activity.date.year }}</div>
          <div class="w-full mx-4 h-0 border-surface border-2 rounded"></div>
        </div>
        <ActivityCard
          :activity="activity"
          class="pl-4"
        />
      </div>
    </div>
    <div v-else>Pas d'activités</div>
  </div>
</template>
