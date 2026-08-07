import { ref } from 'vue';
import { defineStore } from 'pinia';
import type { Activity } from '@/models/activity';
import ActivityApi from '@/api/activity.api';
import { Temporal } from '@js-temporal/polyfill';

export const useActivityStore = defineStore('activity', () => {
  const activities = ref<Activity[]>();
  const activitiesByYear = ref<YearActivities[]>();

  async function fetchActivities() {
    activities.value = await ActivityApi.getAll()
      .then(items => items.sort(sortByDate));
  }

  return { activities, fetchActivities };
});

function sortByDate(left: Activity, right: Activity) {
  return Temporal.PlainDate.compare(right.date, left.date)
}


interface YearActivities {
  year: number,
  activities: Activity[]
}