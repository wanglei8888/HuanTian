<template>
  <page-header-wrapper>
    <a-row :gutter="12">
      <a-col :md="3" :sm="24">
        <a-card :bordered="false" :loading="treeLoading">
          <div>
            <a-tree :treeData="treeData" @select="treeSelect" :defaultExpandAll="true" :replaceFields="{
              key: 'id',
              title: 'name',
              value: 'id'
            }" />
          </div>
        </a-card>
      </a-col>
      <a-col :md="21" :sm="24">
        <a-card :bordered="false">
          <div class="table-page-search-wrapper">
            <a-form layout="inline">
              <a-row :gutter="48">
                <a-col :md="6" :sm="24">
                  <a-form-item label="用户名">
                    <a-input v-model="queryParam.name" placeholder="" />
                  </a-form-item>
                </a-col>
                <a-col :md="6" :sm="24">
                  <a-form-item label="登陆名">
                    <a-input v-model="queryParam.userName" placeholder="" />
                  </a-form-item>
                </a-col>
                <a-col :md="6" :sm="24">
                  <a-form-item label="用户状态">
                    <a-select v-model="queryParam.status" placeholder="请选择">
                      <a-select-option value="">全部</a-select-option>
                      <a-select-option value="0">启用</a-select-option>
                      <a-select-option value="1">禁用</a-select-option>
                    </a-select>
                  </a-form-item>
                </a-col>
                <!-- <template v-if="advanced">
              <a-col :md="8" :sm="24">
                <a-form-item label="调用次数">
                  <a-input-number v-model="queryParam.callNo" style="width: 100%" />
                </a-form-item>
              </a-col>
              <a-col :md="8" :sm="24">
                <a-form-item label="更新日期">
                  <a-date-picker v-model="queryParam.date" style="width: 100%" placeholder="请输入更新日期" />
                </a-form-item>
              </a-col>
              <a-col :md="8" :sm="24">
                <a-form-item label="使用状态">
                  <a-select v-model="queryParam.useStatus" placeholder="请选择" default-value="0">
                    <a-select-option value="0">全部</a-select-option>
                    <a-select-option value="1">关闭</a-select-option>
                    <a-select-option value="2">运行中</a-select-option>
                  </a-select>
                </a-form-item>
              </a-col>
              <a-col :md="8" :sm="24">
                <a-form-item label="使用状态">
                  <a-select placeholder="请选择" default-value="0">
                    <a-select-option value="0">全部</a-select-option>
                    <a-select-option value="1">关闭</a-select-option>
                    <a-select-option value="2">运行中</a-select-option>
                  </a-select>
                </a-form-item>
              </a-col>
            </template> -->
                <a-col :md="!advanced && 6 || 24" :sm="24">
                  <span class="table-page-search-submitButtons"
                    :style="advanced && { float: 'right', overflow: 'hidden' } || {}">
                    <a-button type="primary" @click="$refs.table.refresh(true)">查询</a-button>
                    <a-button style="margin-left: 8px" @click="() => this.queryParam = {}">重置</a-button>
                    <a @click="toggleAdvanced" style="margin-left: 8px">
                      {{ advanced ? '收起' : '展开' }}
                      <a-icon :type="advanced ? 'up' : 'down'" />
                    </a>
                  </span>
                </a-col>
              </a-row>
            </a-form>
          </div>

          <div class="table-operator">
            <a-button type="primary" icon="plus" @click="$refs.userModal.detail()">新建</a-button>
            <a-button type="primary" icon="plus" v-action:Add>新建(测试权限)</a-button>
            <a-dropdown v-if="selectedRowKeys.length > 0">
              <a-menu slot="overlay">
                <a-menu-item key="1">
                  <a-popconfirm title="是否要批量删除？" @confirm="remove(selectedRowKeys)">
                    <a-icon type="delete" />删除
                  </a-popconfirm></a-menu-item>
              </a-menu>
              <a-button style="margin-left: 8px">
                批量操作 <a-icon type="down" />
              </a-button>
            </a-dropdown>
          </div>

          <s-table ref="table" size="default" rowKey="id" :columns="columns" :data="loadData" :alert="true"
            :rowSelection="rowSelection">
            <span slot="enable" slot-scope="text">
              <a-tag :color="text ? 'green' : 'red'">{{ text == 1 ? '启用' : '禁用' }}</a-tag>
            </span>
            <span slot="language" slot-scope="text">
              <a-tag color="geekblue">{{ (languageData.find(q => q.value == text) || { name: text }).name }}</a-tag>
            </span>
            <span slot="avatar" slot-scope="text" style="margin-left: -13px;">
              <img style="width:75px;height:75px" slot="avatar" :src="text" />
            </span>

            <span slot="action" slot-scope="text, record">
              <template>
                <a @click="$refs.userModal.detail(record)">编辑</a>
                <a-divider type="vertical" />
                <a-popconfirm title="是否要删除此行？" @confirm="remove(record.id)">
                  <a>删除</a>
                </a-popconfirm>
                <a-divider type="vertical" />
                <a-dropdown>
                  <a class="ant-dropdown-link">
                    更多 <a-icon type="down" />
                  </a>
                  <a-menu slot="overlay">
                    <a-menu-item>
                      <a href="javascript:;" @click="pwdShowModal(record.id)">修改密码</a>
                    </a-menu-item>
                    <a-menu-item>
                      <a href="javascript:;" @click="$refs.userRoleModal.create(record.id)">授权角色</a>
                    </a-menu-item>
                  </a-menu>
                </a-dropdown>
              </template>
            </span>
          </s-table>
          <user-modal ref="userModal" @ok="handleOk" />
          <user-role-modal ref="userRoleModal" @ok="handleOk" />
          <a-modal title="修改密码" :visible="pwdVisible" :confirm-loading="confirmLoading" @ok="pwdHandleOk"
            @cancel="pwdHandleCancel">
            <a-form-model ref="form" :model="form" :rules="rules">
              <a-row :gutter="24">
                <a-col :md="16" :sm="24">
                  <a-form-model-item label="新密码" prop="password" hasFeedback>
                    <a-input-password v-model="form.password" />
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row :gutter="24">
                <a-col :md="16" :sm="24">
                  <a-form-model-item label="确认密码" prop="passwordAgain" hasFeedback>
                    <a-input-password v-model="form.passwordAgain" />
                  </a-form-model-item>
                </a-col>
              </a-row>
            </a-form-model>
          </a-modal>
        </a-card>
      </a-col>
    </a-row>
  </page-header-wrapper>

</template>

<script>
import md5 from 'md5'
import moment from 'moment'
import { sysDict } from '@/api/system'
import { STable } from '@/components'
import UserModal from './modules/userModal'
import userRoleModal from './modules/userRoleModal'
export default {
  name: 'UserList',
  components: {
    STable,
    UserModal,
    userRoleModal
  },
  data() {
    this.columns = columns
    return {
      treeLoading: false,
      treeData: [],
      pwdVisible: false,
      confirmLoading: false,
      // 高级搜索 展开/关闭
      advanced: false,
      userId: '',
      languageData: [],
      form: {
        password: '',
        passwordAgain: ''
      },
      // 查询参数
      queryParam: {},
      // 加载数据方法 必须为 Promise 对象
      loadData: parameter => {
        const requestParameters = Object.assign({}, parameter, this.queryParam)
        return this.$http.get('/sysUser/page', { params: requestParameters }).then(res => {
          return res.result
        })
      },
      selectedRowKeys: [],
      selectedRows: [],
      rules: {
        password: [{ required: true, validator: this.pwdValidator }],
        passwordAgain: [{ required: true, validator: this.pwdValidatorAgain }]
      },
    }
  },
  methods: {
    dromdownList() {
      sysDict({ code: 'languageType' }).then((res) => {
        if (res.code === 200) {
          this.languageData = res.result
        }
      })
    },
    getTreeData() {
      // this.$store.getters.userInfo.avatar
      this.treeLoading = true
      this.$http.get('/sysDept/tree').then((res) => {
        if (res.code === 200) {
          this.treeData = res.result
        }
      }).finally(() => {
        this.treeLoading = false
      })
    },
    pwdValidator(rule, value, callback) {
      if (!value) {
        callback(new Error('请输入密码'))
      }
      if (value.length < 3) {
        callback(new Error('密码长度不能小于3位'))
      }
      if(this.form.passwordAgain && value !== this.form.passwordAgain){
        callback(new Error('两次输入密码不一致'))
      }
      callback()
    },
    pwdValidatorAgain(rule, value, callback) {
      if (!value) {
        callback(new Error('请输入重复密码'))
      }
      if (value !== this.form.password) {
        callback(new Error('两次输入密码不一致'))
      } 
      callback()
    },
    pwdShowModal(id) {
      this.userId = id
      this.form = {
        password: '',
        passwordAgain: ''
      }
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      this.pwdVisible = true;
    },
    async pwdHandleOk(e) {
      // this.ModalText = 'The modal will be closed after two seconds';
      this.confirmLoading = true;
      this.$refs.form.validate(async valid => {
        if (!valid) {
          this.confirmLoading = false;
          return
        }
        const data  = {
          id: this.userId,
          password: md5(this.form.password)
        }
        await this.$http.put('/sysUser/updatePwd', data).then(res => {
            if (res.code === 200) {
              this.$message.success('保存成功')
              this.$emit('ok')
              this.pwdVisible = false;
            }})
        .finally(() => {
          this.confirmLoading = false;
        })
      })
    },
    pwdHandleCancel(e) {
      this.pwdVisible = false;
    },
    treeSelect(e) {
      this.queryParam.deptId = e[0]
      this.$refs.table.refresh()
    },
    remove(key) {
      this.$http.delete('/sysUser', { data: { Ids: !key.length ? [key] : key } }).then(res => {
        if (res.code === 200) {
          this.$message.success('删除成功')
          this.$refs.table.refresh()
          this.$refs.table.clearSelected()
        }
      })
    },
    handleOk() {
      this.$refs.table.refresh()
    },
    onSelectChange(selectedRowKeys, selectedRows) {
      this.selectedRowKeys = selectedRowKeys
      this.selectedRows = selectedRows
    },
    toggleAdvanced() {
      this.advanced = !this.advanced
    },
    authRole() {
      this.$message.success('授权角色')
    }
  },
  created() {
    this.dromdownList()
    this.getTreeData()
  },
  filters: {
  },
  computed: {
    rowSelection() {
      return {
        selectedRowKeys: this.selectedRowKeys,
        onChange: this.onSelectChange
      }
    }
  }
}
const columns = [
  {
    title: '登陆名',
    dataIndex: 'userName'
  },
  {
    title: '用户名',
    dataIndex: 'name'
  },
  {
    title: '头像',
    dataIndex: 'avatar',
    scopedSlots: { customRender: 'avatar' }
  },
  {
    title: '语言',
    dataIndex: 'language',
    scopedSlots: { customRender: 'language' }
  },
  {
    title: '用户状态',
    dataIndex: 'enable',
    scopedSlots: { customRender: 'enable' }
  },
  {
    title: '最后登陆时间',
    dataIndex: 'lastLoginTime',
    customRender: (text, row, index) => {
      if (!text) {
        return '无'
      }
      return moment(text).format('YYYY-MM-DD HH:mm:ss')
    },
    sorter: true
  },
  {
    title: '操作',
    dataIndex: 'action',
    width: '200px',
    scopedSlots: { customRender: 'action' }
  }
]

</script>
