/**
 * desc: 二进制流导出
 */
import http from '@/api/http'
import { message } from 'ant-design-vue'

export function downLoadGet ({ url, params, callback }) {
  // 卧槽了，http.get设置响应头会被清空，只能这么写
  message.loading({ content: '正在下载，请稍候...', key: url, duration: 8 })

  http({
    url: url,
    params: params || {},
    method: 'get',
    responseType: 'blob'
  }).then(res => {
    // 返回的是普通信息非二进制流
    if (res.code === 200) {
      message.success({ content: '生成成功', key: url })
      return
    }
    const content = res.data
    const blob = new Blob([content])
    const name = res.request.getResponseHeader('Content-Disposition')
    if (window.navigator.msSaveOrOpenBlob) { // 兼容IE
      const fileName = decodeURI(name.split('filename=')[1])
      navigator.msSaveBlob(blob, fileName)
    } else {
      const elink = document.createElement('a')
      elink.download = decodeURI(name.split('filename=')[1])
      elink.style.display = 'none'
      elink.href = URL.createObjectURL(blob)
      document.body.appendChild(elink)
      elink.click()
      document.body.removeChild(elink)
      message.success({ content: '下载成功', key: url, duration: 2 })
    }
  }).catch(err => {
    message.error({ content: err.message, key: url, duration: 1 })
  })
}
export function downLoadPost ({ url, params, callback }) {
  // 卧槽了，http.get设置响应头会被清空，只能这么写
  message.loading({ content: '正在下载，请稍候...', key: url, duration: 8 })
  http({
    url: url,
    data: params || {},
    method: 'post',
    responseType: 'blob'
  }).then(res => {
    // 返回的是普通信息非二进制流
    if (res.code == 200) {
      message.success({ content: '生成成功', key: url })
      return
    }
    const content = res.data
    const blob = new Blob([content])
    const name = res.request.getResponseHeader('Content-Disposition')
    if (window.navigator.msSaveOrOpenBlob) { // 兼容IE
      const fileName = decodeURI(name.split('filename=')[1])
      navigator.msSaveBlob(blob, fileName)
    } else {
      const elink = document.createElement('a')
      elink.download = decodeURI(name.substring(name.indexOf('=') + 1, name.lastIndexOf(';')))
      elink.style.display = 'none'
      elink.href = URL.createObjectURL(blob)
      document.body.appendChild(elink)
      elink.click()
      document.body.removeChild(elink)
      message.success({ content: '生成成功', key: url })
    }
  }).catch(err => {
    message.error({ content: '生成失败', key: url, duration: 1 })
  })
}
