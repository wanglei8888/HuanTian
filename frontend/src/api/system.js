import {axios} from '@/utils/request'

const userApi = {
  sysDictionary: '/SysDictionary'
}

/**
 * login func
 * parameter: {
 *     username: '',
 *     password: '',
 *     remember_me: true,
 *     captcha: '12345'
 * }
 * @param parameter
 * @returns {*}
 */
export function sysDict (parameter) {
  return axios({
    url: userApi.sysDictionary,
    method: 'get',
    params: parameter
  })
}