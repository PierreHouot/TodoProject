<template>
  <div class="group/dropdown relative">
    <div class="group-hover:opacity-100 opacity-0 m-1 p-1 cursor-default text-xs text-light hover:bg-accent rounded"
      style="writing-mode: sideways-lr">
      •••
    </div>
    <div
      class="hidden group-hover/dropdown:block absolute right-0 text-xs bg-light text-dark p-1 border-2 rounded w-fit">
      <slot>option</slot>
    </div>
  </div>
</template>
