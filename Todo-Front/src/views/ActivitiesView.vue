<script setup lang="ts">
import ActivitiesList from '@/component/ActivitiesList.vue';
import ActivityModalAddForm from '@/component/ActivityModalAddForm.vue';
import ActivityYears from '@/component/ActivityYears.vue';
import GlobalButton from '@/component/global/GlobalButton.vue';

import { ref } from 'vue';

const showModal = ref(false);
</script>

<template>
  <div class="md:grid max-w-212 grid-cols-4 gap-7 grid-rows-1 h-full">
    <ActivityYears class="max-md:hidden" />
    <ActivitiesList class="col-span-2 m-auto w-[min(28rem,98%)] h-full" />
    <div comment="Switch between 2 buttons depending on responsive">
      <GlobalButton
        class="bg-dark h-8 text-light mt-2 max-md:hidden"
        @click="() => (showModal = true)"
      >
        Add a moment
      </GlobalButton>
      <GlobalButton
        class="bg-accent text-light font-bold text-4xl md:hidden fixed bottom-5 right-2 z-1 border-2 border-light"
        @click="() => (showModal = true)"
      >
        ✐
      </GlobalButton>
    </div>
  </div>

  <ActivityModalAddForm
    @close="() => (showModal = false)"
    @posted="() => (showModal = false)"
    :show="showModal"
  />
</template>
