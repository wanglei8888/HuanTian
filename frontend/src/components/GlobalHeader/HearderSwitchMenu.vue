<template>
  <div>
    <a-menu style="height: 55px; border-bottom: 0px;" mode="horizontal" v-model="selectType">
      <a-menu-item
        style="top:0px; line-height: 55px; padding-left: 10px; padding-right: 10px"
        v-for="(item) in appList"
        :key="item.code"
        @click="switchApp(item.code)">
        {{ item.name }}
      </a-menu-item>
    </a-menu>
  </div>
</template>

<script>
import AvatarDropdown from './AvatarDropdown'
import SelectLang from '@/components/SelectLang'

export default {
  name: 'RightContent',
  components: {
    AvatarDropdown,
    SelectLang
  },
  data () {
    return {
      showMenu: true,
      selectType: []
    }
  },
  computed: {
    appList () {
      return this.$store.getters.userInfo.app
    },
    selectTypeChange () {
      return [this.$store.getters.appCode]
    }
  },
  methods: {
    switchApp (code) {
      const applicationData = this.appList.filter(item => item.code === code)
      this.$store.dispatch('MenuChange', applicationData[0]).then(res => {
        this.$store.commit('SET_APPCODE', code)
        // this.$message.success(`切换到应用: ${applicationData[0].name}`)
      })
    }
  },
  created () {
    this.selectType = this.selectTypeChange
  }
}
</script>
