import { ref } from 'vue';
import { defineStore } from 'pinia';
import type { Activity } from '@/models/activity';
import ActivityApi from '@/api/activity.api';
import { Temporal } from '@js-temporal/polyfill';

export const useActivityStore = defineStore('activity', () => {
  const activities = ref<Activity[]>();
  const activityYears = ref<Array<string>>([]);

  async function fetchActivities() {
    activities.value = await ActivityApi.getAll().then((items) => {
      items.forEach((item) => {
        const year = item.date.year.toString();
        if (!activityYears.value.includes(year)) activityYears.value.push(year);
      });
      return items.sort(sortByDate);
    });
    activityYears.value.sort().reverse();
  }

  return { activities, activityYears, fetchActivities };
});

function sortByDate(left: Activity, right: Activity) {
  return Temporal.PlainDate.compare(right.date, left.date);
}
