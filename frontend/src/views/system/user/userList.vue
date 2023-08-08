<template>
  <a-row :gutter="12">
    <a-col :md="3" :sm="24">
      <a-card :bordered="false" :loading="treeLoading">
        <div>
          <a-tree
            :treeData="treeData"
            @select="treeSelect"
            :defaultExpandAll="true"
            :replaceFields="{
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
                <span
                  class="table-page-search-submitButtons"
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
          <a-button type="primary" icon="plus" v-action:Add >新建(测试权限)</a-button>
          <a-dropdown v-if="selectedRowKeys.length > 0">
            <a-menu slot="overlay">
              <a-menu-item key="1">
                <a-popconfirm title="是否要批量删除？" @confirm="remove(selectedRowKeys.join(','), true)">
                  <a-icon type="delete" />删除
                </a-popconfirm></a-menu-item>
            </a-menu>
            <a-button style="margin-left: 8px">
              批量操作 <a-icon type="down" />
            </a-button>
          </a-dropdown>
        </div>

        <s-table
          ref="table"
          size="default"
          rowKey="id"
          :columns="columns"
          :data="loadData"
          :alert="true"
          :rowSelection="rowSelection">
          <span slot="enable" slot-scope="text">
            <a-tag :color="text ? 'green' : 'red'">{{ text == 1 ? '启用' : '禁用' }}</a-tag>
          </span>
          <span slot="language" slot-scope="text">
            {{ text | languageFilter }}
          </span>
          <span slot="avatar" slot-scope="text" style="margin-left: -13px;">
            <img style="width:75px;heigth:75px" slot="avatar" :src="text" />
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
                    <a href="javascript:;" @click="$message.success('修改密码')">修改密码</a>
                  </a-menu-item>
                </a-menu>
              </a-dropdown>
            </template>
          </span>
        </s-table>
        <user-modal ref="userModal" @ok="handleOk" />
      </a-card>
    </a-col>
  </a-row>
</template>

<script>
import moment from 'moment'
import { STable } from '@/components'
import UserModal from './modules/userModal'

export default {
  components: {
    STable,
    UserModal
  },
  data () {
    this.columns = columns
    return {
      treeLoading: false,
      treeData: [],
      visible: false,
      confirmLoading: false,
      // 高级搜索 展开/关闭
      advanced: false,
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
      selectedRows: []
    }
  },
  methods: {
    getTreeData () {
      this.treeLoading = true
      this.$http.get('/sysDept/tree').then((res) => {
        if (res.code === 200) {
          this.treeData = res.result
        }
      }).finally(() => {
        this.treeLoading = false
      })
    },
    treeSelect (e) {
      this.queryParam.deptId = e[0]
      this.$refs.table.refresh()
    },
    remove (key, multipleChoice) {
      this.$http.delete('/sysUser', { data: { Id: key.toString() } }).then(res => {
        if (res.code === 200) {
          this.$message.success('删除成功')
          this.$refs.table.refresh()
          // 是否多选
          if (multipleChoice) {
            this.$refs.table.clearSelected()
          }
        }
      })
    },
    handleOk () {
      this.$refs.table.refresh()
    },
    onSelectChange (selectedRowKeys, selectedRows) {
      this.selectedRowKeys = selectedRowKeys
      this.selectedRows = selectedRows
    },
    toggleAdvanced () {
      this.advanced = !this.advanced
    }
  },
  created () {
    this.getTreeData()
  },
  filters: {
    languageFilter (type) {
      return languageMap[type].text
    }
  },
  computed: {
    rowSelection () {
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

const languageMap = {
  0: {
    status: '0',
    text: '中文'
  },
  1: {
    status: '1',
    text: '英语'
  }
}

</script>
