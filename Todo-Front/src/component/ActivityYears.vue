<script setup lang="ts">
import { useActivityStore } from '@/stores/activity.store';
import { storeToRefs } from 'pinia';
const store = useActivityStore();
const { activityYears } = storeToRefs(store);
function scrollToSection(id: string) {
  document.getElementById(id)?.scrollIntoView();
}
</script>

<template>
  <div class="justify-self-end flex flex-col mt-2">
    <div class="text-dark">Explore years :</div>
    <button
      v-for="year in activityYears"
      :key="year"
      class="text-end cursor-pointer text-sm hover:text-dark hover:italic text-surface"
      :href="`#${year}`"
      @click.prevent="() => scrollToSection(year)"
    >
      - {{ year }}
    </button>
  </div>
</template>
