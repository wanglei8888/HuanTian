import { mapState } from 'vuex'

const i18nMixin = {
  computed: {
    ...mapState({
      currentLang: state => state.app.lang
    }),
    ...mapState({
      userInfo: state => state.user.info
    }),
  },
  methods: {
    async setLang(lang) {
      this.userInfo.language = lang.replace(/-/g, '_')
      await this.$http.put('/sysUser', this.userInfo).then(res => {
        this.$store.dispatch('setLang', lang)
      }).catch(() => {
        console.log('setLang error')
      })
    }
  }
}

export default i18nMixin
