import storage from 'store'
import expirePlugin from 'store/plugins/expire'
import { login, getInfo, logout } from '@/api/login'
import { ACCESS_TOKEN } from '@/store/mutation-types'
import { welcome } from '@/utils/util'

storage.addPlugin(expirePlugin)
const user = {
  state: {
    token: '',
    name: '',
    welcome: '',
    avatar: '',
    roles: [],
    info: {}
  },

  mutations: {
    SET_TOKEN: (state, token) => {
      state.token = token
    },
    SET_NAME: (state, { name, welcome }) => {
      state.name = name
      state.welcome = welcome
    },
    SET_AVATAR: (state, avatar) => {
      state.avatar = avatar
    },
    SET_ROLES: (state, roles) => {
      state.roles = roles
    },
    SET_INFO: (state, info) => {
      state.info = info
    }
  },

  actions: {
    // 登录
    Login({ commit }, userInfo) {
      return new Promise((resolve, reject) => {
        login(userInfo).then(response => {
          const result = response.result
          storage.set(ACCESS_TOKEN, result.token)
          commit('SET_TOKEN', result.token)
          resolve()
        }).catch(error => {
          reject(error)
        })
      })
    },

    // 获取用户信息
    GetInfo({ commit }) {
      return new Promise((resolve, reject) => {
        // 请求后端获取用户信息 /api/user/info
        getInfo().then(response => {
          const { result } = response
          if (result.role) {
            const role = { ...result.role }
            role.permissions = result.role.map(permission => {
              const per = {
                ...permission,
                actionList: (permission.actionEntitySet || {}).map(item => item.action)
              }
              return per
            })
            role.permissionList = result.role.map(permission => { return permission.menuId })
            // 覆盖响应体的 role, 供下游使用
            result.role = role
            commit('SET_ROLES', role)
            commit('SET_INFO', result)
            commit('SET_NAME', { name: result.name, welcome: welcome() })
            commit('SET_AVATAR', result.avatar)
            // 下游
            resolve(result)
          } else {
            reject(new Error('getInfo: roles must be a non-null array !'))
          }
        }).catch(error => {
          reject(error)
        })
      })
    },

    // 登出
    Logout({ commit, state }) {
      return new Promise((resolve) => {
        logout(state.token).then(() => {
          commit('SET_TOKEN', '')
          commit('SET_ROLES', [])
          storage.remove(ACCESS_TOKEN)
          resolve()
        }).catch((err) => {
          console.log('logout fail:', err)
          // resolve()
        }).finally(() => {
        })
      })
    },
    // 切换应用菜单
    MenuChange({ commit }, application) {
      return new Promise((resolve, reject) => {
        const appCode = application.code
        // 缓存获取所有应用
        const allAppMenu = Vue.ls.get(ALL_APPS_MENU)
        // 切换应用
        let appMenu
        allAppMenu.forEach(item => {
          if (item.code === appCode) {
            appMenu = item
            item.active = true
          } else {
            item.active = false
          }
        })
        // 如果找不到
        if (!appMenu) {
          reject(new Error(`找不到应用: ${appCode}`))
          return
        }
        // 找到对应的应用，设置新的缓存
        Vue.ls.set(ALL_APPS_MENU, allAppMenu)
        resolve(appMenu)
        // 切换路由表
        const antDesignmenus = appMenu.menu
        store.dispatch('GenerateRoutes', { antDesignmenus }).then(() => {
          router.addRoutes(store.getters.addRouters)
        })
      })
    }

  }
}

export default user
