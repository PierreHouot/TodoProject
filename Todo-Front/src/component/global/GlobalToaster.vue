<script setup lang="ts">
import type { Toast } from '@/models/toast';
import { useToasterStore } from '@/stores/toaster.store';
import { storeToRefs } from 'pinia';
import { onMounted, ref, watch } from 'vue';

const store = useToasterStore()
const { notifications } = storeToRefs(store)

const displayedNotification = ref<Toast | null>(null)
const timeId = ref<number | null>(null)

onMounted(() => {
    handleToast()
})

watch(() => notifications.value.length, () => {
    handleToast()
})

function handleToast() {
    if (timeId.value || notifications.value.length <= 0)
        return
    displayedNotification.value = notifications.value[0] ?? null
    if (displayedNotification.value == null)
        return

    timeId.value = setTimeout(() => {
        store.discardFirstToast()
        timeId.value = null
        displayedNotification.value = null
        if (timeId.value)
            clearTimeout(timeId.value)
    }, 5000)
}

</script>

<template>
    <Transition mode="out-in" appear>
        <div v-if="displayedNotification" :key="Date.now()"
            class="absolute m-2 top-0 right-0 z-30 w-50 min-h-5 bg-surface rounded border-dark border">
            <h1 class="text-accent px-2 h-full bg-dark text-lg font-bold">{{ displayedNotification?.title }}</h1>
            <p class="text-dark m-2">{{ displayedNotification?.message }}</p>
        </div>
    </Transition>
</template>

<style scoped>
.v-enter-active {
    transition: all 0.5s;
    transform: translateX(300px);
    opacity: 0;
}

.v-leave-active {
    position: absolute;
    transform: translateX(300px);
    opacity: 0;
}
</style>