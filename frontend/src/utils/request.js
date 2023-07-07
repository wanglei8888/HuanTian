import axios from 'axios'
import store from '@/store'
import storage from 'store'
import notification from 'ant-design-vue/es/notification'
import { VueAxios } from './axios'
import { ACCESS_TOKEN } from '@/store/mutation-types'
import { i18nRender } from '@/locales'
// 创建 axios 实例
const request = axios.create({
  // API 请求的默认前缀
  baseURL: process.env.VUE_APP_API_BASE_URL,
  timeout: 6000 // 请求超时时间
})

// 异常拦截处理器
const errorHandler = (error) => {
  if (error.response) {
    const data = error.response.data
    // 从 localstorage 获取 token
    const token = storage.get(ACCESS_TOKEN)
    if (error.response.status === 403) {
      notification.error({
        message: 'Forbidden',
        description: data.message
      })
    }
    if (error.response.status === 401 && !(data.result && data.result.isLogin)) {
      notification.error({
        message: i18nRender('Unauthorized'),
        description: i18nRender('Authorization verification failed')
      })
      if (token) {
        store.dispatch('Logout').then(() => {
          setTimeout(() => {
            window.location.reload()
          }, 1500)
        })
      }
    }
    if (error.response.status === 500) {
      // 如果是获取文件返回错误信息
      if (error.request.responseType === 'blob') {
        let reader = new FileReader();
        return new Promise((resolve, reject) => {
          reader.onload = function () {
            let apiData = JSON.parse(reader.result);
            // 普通异常报错信息
            notification.error({
              message: i18nRender('Error message'),
              description: apiData.message
            })
            reject(apiData);
          }
          reader.onerror = function () {
            reject(new Error('Failed to read response data'));
          }
          reader.readAsText(error.response.data);
        });
      }
      // 普通异常报错信息
      notification.error({
        message: i18nRender('Error message'),
        description: error.response.data.message
      })
    }
  }
  return Promise.reject(error)
}

// request interceptor
request.interceptors.request.use(config => {
  const token = storage.get(ACCESS_TOKEN)
  // 如果 token 存在
  // 让每个请求携带自定义 token 请根据实际情况自行修改
  if (token) {
    config.headers[ACCESS_TOKEN] = token
  }
  return config
}, errorHandler)

// response interceptor
request.interceptors.response.use((response) => {
  // 如果是获取文件返回文件流
  if (response.request.responseType === 'blob' && response.headers['content-disposition']) {
    return response
  }
  // 请求是文件流，返回的不是文件流数据
  if (response.request.responseType === 'blob' && !response.headers['content-disposition']) {
    let reader = new FileReader();
    return new Promise((resolve, reject) => {
      reader.onload = function () {
        let apiData = JSON.parse(reader.result);
        resolve(apiData);
      }
      reader.onerror = function () {
        reject(new Error('Failed to read response data'));
      }
      reader.readAsText(response.data);
    });
  }
  return response.data
}, errorHandler)

const installer = {
  vm: {},
  install(Vue) {
    Vue.use(VueAxios, request)
  }
}

export default request

export {
  installer as VueAxios,
  request as axios
}
