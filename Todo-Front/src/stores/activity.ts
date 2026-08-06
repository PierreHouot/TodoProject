import { ref } from 'vue';
import { defineStore } from 'pinia';
import type { Activity } from '@/models/activity';
import ActivityApi from '@/api/activity.api';

export const useActivityStore = defineStore('activity', () => {
  const activities = ref<Activity[]>();
  const activitiesByYear = ref<YearActivities[]>();

  async function fetchActivities() {
    activities.value = await ActivityApi.getAll()
      .then(items => {
        items.forEach((item) => {
          item.date =
            new Date(
              new Date(
                item.date.toString()
              ))
        }
        return items
      })
      .then(items => items.sort(sortByDate));
  }

  return { activities, fetchActivities };
});

function sortByDate(left: Activity, right: Activity) {
  console.log(left.date);

  return new Date(left.date) > new Date(right.date) ? -1 : 1
}

interface YearActivities {
  year: number,
  activities: Activity[]
}