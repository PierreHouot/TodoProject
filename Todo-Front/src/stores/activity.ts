import { ref } from 'vue';
import { defineStore } from 'pinia';
import type { Activity } from '@/models/activity';
import ActivityApi from '@/api/activity.api';

export const useActivityStore = defineStore('activity', () => {
  const activities = ref<Activity[]>();

  async function fetchActivities() {
    activities.value = await ActivityApi.getAll();
  }

  return { activities, fetchActivities };
});
