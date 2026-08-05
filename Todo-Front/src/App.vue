<script setup lang="ts">
import GlobalHeader from './component/global/GlobalHeader.vue';
import ActivitiesView from './views/ActivitiesView.vue';

import bgImg from '@/assets/marion-botella.webp';
</script>

<template>
  <div class="h-screen w-screen">
    <img
      :src="bgImg"
      class="-z-10 fixed inset-0 h-full w-full object-cover"
    />
    <div class="flex flex-col items-center w-full h-full">
      <GlobalHeader class="bg-background-dark shrink-0 text-text w-full h-[4rem]" />
      <div class="flex justify-center w-full h-[calc(100%-4rem)] bg-background-light">
        <ActivitiesView />
      </div>
    </div>
  </div>
</template>
