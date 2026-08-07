<script setup lang="ts">
import ActivityAddForm from '@/component/ActivityAddForm.vue';
import ActivityCard from '@/component/ActivityCard.vue';
import GlobalButton from '@/component/global/GlobalButton.vue';
import { useActivityStore } from '@/stores/activity';
import { storeToRefs } from 'pinia';
import { computed, onMounted, ref } from 'vue';
const store = useActivityStore();
const { activities } = storeToRefs(store);

// const Props|props = defineProps<Props>();
onMounted(async () => {
  await store.fetchActivities();
});

const showModal = ref(false);

const hasActivities = computed(() => activities.value && activities.value.length > 0);

const showYear = (index: number) => {
  if (!activities.value) return;
  if (index === 0) return true;
  return activities.value[index]?.date.year !== activities.value[index - 1]!.date.year;
};
</script>

<template>
  <div class="grid grid-cols-3 gap-4 pt-1 grid-rows-1">
    <div></div>
    <div class="flex w-96">
      <div
        v-if="hasActivities"
        class="overflow-y-auto grow pr-1.5 pt-1 h-[calc(100%-1rem)]"
      >
        <div
          :key="activity.id"
          v-for="(activity, id) in activities"
        >
          <div
            v-if="showYear(id)"
            class="flex items-center"
            :id="activity.date.year.toString()"
          >
            <div class="font-title text-surface">{{ activity.date.year }}</div>
            <div
              class="w-full ml-4 h-0 border-surface border-2 rounded"
              style="content: ' '"
            ></div>
          </div>
          <ActivityCard :activity="activity" />
        </div>
      </div>
      <div v-else>Pas d'activités</div>
    </div>

    <GlobalButton
      class="bg-dark h-8 text-light"
      @click="() => (showModal = true)"
    >
      Add a moment
    </GlobalButton>
  </div>

  <ActivityAddForm
    @close="() => (showModal = false)"
    @posted="() => (showModal = false)"
    :show="showModal"
  />
</template>
