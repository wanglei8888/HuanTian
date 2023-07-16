<template>
  <div>
    <a-menu style="height: 55px; border-bottom: 0px;" mode="horizontal">
      <a-menu-item style="top:0px; line-height: 55px; padding-left: 10px; padding-right: 10px"
        @click="switchApp('System')">
        系统菜单
      </a-menu-item>
      <a-menu-item style="top:0px; line-height: 55px; padding-left: 10px; padding-right: 10px"
        @click="switchApp('Business')">
        业务菜单
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
  data() {
    return {
      showMenu: true
    }
  },
  methods: {
    switchApp() {
      var app = this.$store.getters.userInfo
      this.$store.dispatch('GenerateRoutes', { app }).then(res => {
        debugger
      })
    }
  }
}
</script>
