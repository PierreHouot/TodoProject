<script setup lang="ts">
import GlobalHeader from './component/global/GlobalHeader.vue';
import ActivitiesView from './views/ActivitiesView.vue';

import bgImg from "@/assets/marion-botella.webp"


</script>

<template>
  <img :src="bgImg" class="-z-10 fixed inset-0 h-screen w-screen object-cover">
  <div class=" flex flex-col items-center w-full h-screen m-auto bg-background-dark text-text">
    <GlobalHeader />
    <div class="flex flex-1 justify-center w-full bg-background-light">
      <ActivitiesView />
    </div>
  </div>
</template>
