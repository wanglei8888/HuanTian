<template>
  <page-header-wrapper>
    <a-card :bordered="false">
      <div class="table-page-search-wrapper">
        <a-form layout="inline">
          <a-row :gutter="48">
            <a-col :md="8" :sm="24">
              <a-form-item label="菜单名">
                <a-input v-model="queryParam.name" placeholder="" />
              </a-form-item>
            </a-col>
            <template v-if="advanced">
            </template>
            <a-col :md="!advanced && 8 || 24" :sm="24">
              <span class="table-page-search-submitButtons"
                :style="advanced && { float: 'right', overflow: 'hidden' } || {}">
                <a-button type="primary" @click="handSerach()">查询</a-button>
                <a-button style="margin-left: 8px" @click="() => this.queryParam = {}">重置</a-button>
                <!-- <a @click="toggleAdvanced" style="margin-left: 8px">
                  {{ advanced ? '收起' : '展开' }}
                  <a-icon :type="advanced ? 'up' : 'down'"/>
                </a> -->
              </span>
            </a-col>
          </a-row>
        </a-form>
      </div>

      <div class="table-operator">
        <a-button type="primary" icon="plus" @click="$refs.detailForm.detail()">新建</a-button>
        <a-dropdown v-action:edit v-if="selectedRowKeys.length > 0">
          <a-menu slot="overlay">
            <a-menu-item key="1"><a-icon type="delete" />删除</a-menu-item>
            <!-- lock | unlock -->
            <a-menu-item key="2"><a-icon type="lock" />锁定</a-menu-item>
          </a-menu>
          <a-button style="margin-left: 8px">
            批量操作 <a-icon type="down" />
          </a-button>
        </a-dropdown>
      </div>

      <a-table rowKey="id" :columns="columns" :dataSource="loadData" :row-selection="rowSelection" :pagination="false"
        :expandIconAsCell='false' :loading="tableLoading">
        <template slot="operation" slot-scope="text, record">
          <span>
            <a-popconfirm title="是否要删除此行？" @confirm="remove(record.id)">
              <a>删除</a>
            </a-popconfirm>
          </span>
          <span>
            <a-divider type="vertical" />
            <a @click="$refs.detailForm.detail(record)">编辑</a>
          </span>
        </template>
        <span slot="serial" slot-scope="text">
          <a-icon slot="serial" :type="text" />
        </span>
      </a-table>
      <menu-Form ref="detailForm" @ok="modelOk" />
      <!-- <add-form ref="detailForm" @ok="handleOk"/> -->
    </a-card>
  </page-header-wrapper>
</template>

<script>
import moment from 'moment'
import { STable, Ellipsis } from '@/components'

import StepByStepModal from '../list/modules/StepByStepModal'
import menuForm from '../System/modules/MenuForm'

const columns = [
  {
    title: '菜单名称',
    dataIndex: 'name',
    width: '15%'
  },
  {
    title: '图标',
    dataIndex: 'icon',
    width: '15%',
    scopedSlots: { customRender: 'serial' }
  },
  {
    title: '多语言值',
    dataIndex: 'title'
  },
  {
    title: '跳转地址',
    dataIndex: 'redirect',
    width: '12%'
  },
  {
    title: '菜单类型',
    dataIndex: 'component',
    width: '12%'
  },
  // {
  //   title: '',
  //   dataIndex: 'lastLoginTime',
  //   customRender: (text, row, index) => {
  //     return moment(text).format('YYYY-MM-DD HH:mm:ss')
  //   },
  //   sorter: true
  // },
  {
    title: '操作',
    width: '150px',
    key: 'action',
    scopedSlots: { customRender: 'operation' }
  }
]

const statusMap = {
  0: {
    status: '0',
    text: '启用'
  },
  1: {
    status: '1',
    text: '禁用'
  }
}
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

export default {
  name: 'TableList',
  components: {
    STable,
    Ellipsis,
    menuForm,
    StepByStepModal
  },
  data() {
    this.columns = columns
    return {
      // create model
      confirmLoading: false,
      mdl: null,
      tableLoading: false,
      // 高级搜索 展开/关闭
      advanced: false,
      // 查询参数
      queryParam: {},
      // 加载数据方法 必须为 Promise 对象
      loadData: [],
      selectedRowKeys: [],
      selectedRows: []
    }
  },
  created() {
    this.handSerach()
  },
  computed: {
    rowSelection() {
      return {
        selectedRowKeys: this.selectedRowKeys,
        onChange: this.onSelectChange
      }
    }
  },
  methods: {
    handleAdd() {
      this.mdl = null
    },
    remove(key) {
      this.$http.delete('/sysMenu', { data: { Id: key } }).then(res => {
        if (res.code === 200) {
          this.$message.success('删除成功')
          this.handSerach()
        }
      })
    },
    handSerach() {
      // const requestParameters = Object.assign({}, parameter, this.queryParam)
      this.$http.get('/sysMenu', { params: this.queryParam }).then(res => {
        this.loadData = res.result
      })
      // this.$refs.table.refresh()
    },
    handleEdit(record) {
      this.mdl = { ...record }
    },
    modelOk() {
      console.log(1)
      this.handSerach()
    },
    handleCancel() {
      const form = this.$refs.createModal.form
      form.resetFields() // 清理表单数据（可不做）
    },
    handleSub(record) {
      if (record.status !== 0) {
        this.$message.info(`${record.no} 订阅成功`)
      } else {
        this.$message.error(`${record.no} 订阅失败，规则已关闭`)
      }
    },
    onSelectChange(selectedRowKeys, selectedRows) {
      this.selectedRowKeys = selectedRowKeys
      this.selectedRows = selectedRows
    },
    toggleAdvanced() {
      this.advanced = !this.advanced
    },
    resetSearchForm() {
      this.queryParam = {
        date: moment(new Date())
      }
    }
  }
}
</script>
