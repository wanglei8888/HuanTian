/**
 * 向后端请求用户的菜单，动态生成路由
 */
import { constantRouterMap } from '@/config/router.config'
import { generatorDynamicRouter } from '@/router/generator-routers'

const permission = {
  state: {
    routers: constantRouterMap,
    addRouters: [],
    appCode: ''
  },
  mutations: {
    SET_ROUTERS: (state, routers) => {
      state.addRouters = routers
    },
    SET_APPCODE: (state, appCode) => {
      state.appCode = appCode
    }
  },
  actions: {
    GenerateRoutes ({ commit }, data) {
      return new Promise((resolve, reject) => {
        generatorDynamicRouter(data).then(routers => {
          resolve(routers)
        }).catch(e => {
          reject(e)
        })
      })
    }
  }
}

export default permission
