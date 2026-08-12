<script setup lang="ts">
import ActivitiesList from '@/component/ActivitiesList.vue';
import ActivityModalForm from '@/component/ActivityModalAddForm.vue';
import GlobalButton from '@/component/global/GlobalButton.vue';
import { useActivityStore } from '@/stores/activity.store';
import { storeToRefs } from 'pinia';
import { ref } from 'vue';
const store = useActivityStore();
const { activityYears } = storeToRefs(store);

const showModal = ref(false);

function scrollToSection(id: string) {
  document.getElementById(id)?.scrollIntoView();
}
</script>

<template>
  <div class="grid grid-cols-3 gap-7 grid-rows-1">
    <div class="justify-self-end flex flex-col text-surface text-sm mt-2">
      <div>Explore years :</div>
      <button v-for="year in activityYears" :key="year"
        class="text-end cursor-pointer text-xs hover:text-dark hover:italic" :href="`#${year}`"
        @click.prevent="() => scrollToSection(year)">
        - {{ year }}
      </button>
    </div>
    <ActivitiesList />
    <GlobalButton class="bg-dark h-8 text-light mt-2" @click="() => (showModal = true)">
      Add a moment
    </GlobalButton>
  </div>

  <ActivityModalForm @close="() => (showModal = false)" @posted="() => (showModal = false)" :show="showModal" />
</template>
