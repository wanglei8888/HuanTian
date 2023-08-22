import storage from 'store'
import store from '@/store'
import { message } from 'ant-design-vue'
import expirePlugin from 'store/plugins/expire'
import { login, getInfo, logout } from '@/api/login'
import { ACCESS_TOKEN, ALL_APP_MENU } from '@/store/mutation-types'
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
    Login ({ commit }, userInfo) {
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
    GetInfo ({ commit }) {
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
            // role.permissionList = result.role.map(permission => { return permission.menuId })
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
    Logout ({ commit, state }) {
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
    MenuChange ({ commit }, application) {
      return new Promise((resolve, reject) => {
        const appCode = application.code
        // 缓存获取所有应用
        const allAppMenu = storage.get(ALL_APP_MENU)
        // 切换应用
        const appMenu = allAppMenu.filter(item => item.menuType === appCode)
        // 如果找不到
        if (!appMenu || appMenu.length === 0) {
          message.error(`找不到应用: ${application.name} - 应用下没有菜单`)
          reject(new Error(`找不到应用: ${application.name} - 应用下没有菜单`))
          return
        }
        resolve(appMenu)
        store.dispatch('GenerateRoutes', appMenu).then((routers) => {
          commit('SET_ROUTERS', routers)
        })
      })
    }
  }
}

export default user
