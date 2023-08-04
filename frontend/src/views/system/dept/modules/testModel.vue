<template>
  <div>
    <a-button type="primary" @click="toggleViewMode">{{ viewMode ? '显示 HTML' : '显示效果' }}</a-button>
    <br /><br />
    <a-textarea v-if="!viewMode" v-model="inputText" :disabled="viewMode" :autosize="{ minRows: 4 }"></a-textarea>
    <br /><br />
    <div v-if="viewMode" v-html="inputText"></div>
    <div v-else>{{ inputText }}</div>
  </div>
</template>

<script>
export default {
  data () {
    return {
      inputText: '',
      viewMode: false
    }
  },
  methods: {
    toggleViewMode () {
      this.viewMode = !this.viewMode
    }
  }
}
</script>
