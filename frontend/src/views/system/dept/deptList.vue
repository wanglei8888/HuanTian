<template>
  <page-header-wrapper>
    <a-card :bordered="false">
      <div class="table-page-search-wrapper">
        <a-form layout="inline">
          <a-row :gutter="gutter">
            <a-col :md="mdSize" :sm="24">
              <a-form-item label="部门名字">
                <a-input v-model="queryParam.name" placeholder="" />
              </a-form-item>
            </a-col>
            <a-col :md="mdSize" :sm="24">
              <a-form-item label="部门启用">
                <a-select v-model="queryParam.enable">
                  <a-select-option :value="true">
                    启用
                  </a-select-option>
                  <a-select-option :value="false">
                    禁用
                  </a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
            <a-col :md="!advanced && mdSize || 24" :sm="24">
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
        <a-button type="primary" icon="plus" @click="$refs.infoModel.detail()">新建</a-button>
        <a-dropdown v-action:edit v-if="selectedRowKeys.length > 0">
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
      <s-table
        ref="table"
        size="default"
        rowKey="id"
        :columns="columns"
        :data="loadData"
        :alert="false"
        :showPagination="false">
        <span slot="enableRadio" slot-scope="text">
          <a-tag :color="text ? 'green' : 'red'">{{ text == 1 ? '启用' : '禁用' }}</a-tag>
        </span>
        <span slot="action" slot-scope="text, record">
          <template>
            <a @click="$refs.infoModel.detail(record)">编辑</a>
            <a-divider type="vertical" />
            <a-popconfirm title="是否要删除此行？" @confirm="remove(record.id)">
              <a>删除</a>
            </a-popconfirm>
          <!-- <a-divider type="vertical" />
            <a-dropdown>
              <a class="ant-dropdown-link">
                更多 <a-icon type="down" />
              </a>
              <a-menu slot="overlay">
                <a-menu-item>
                  <a href="javascript:;" @click="">新</a>
                </a-menu-item>
              </a-menu>
            </a-dropdown>-->
          </template>
        </span>
      </s-table>
      <info-model ref="infoModel" @ok="handleOk" />
    </a-card>
  </page-header-wrapper>
</template>

<script>
import moment from 'moment'
import { STable } from '@/components'
import infoModel from './modules/deptModel'
import { removeEmptyChildren } from '@/utils/common'

export default {
  components: {
    STable,
    infoModel
  },
  data () {
    this.columns = columns
    return {
      // 高级搜索 展开/关闭
      advanced: false,
      mdSize: 6,
      gutter: 48,
      // 查询参数
      queryParam: {},
      // 加载数据方法 必须为 Promise 对象
      loadData: parameter => {
        const requestParameters = Object.assign({}, parameter, this.queryParam)
        return this.$http.get('/sysDept/tree', { params: requestParameters }).then(res => {
          removeEmptyChildren(res.result)
          return res.result
        })
      },
      selectedRowKeys: [],
      selectedRows: []
    }
  },
  methods: {
    remove (key) {
      this.$http.delete('/sysDept', { data: { Ids: !key.length ? [key] : key } }).then(res => {
        if (res.code === 200) {
          this.$message.success('删除成功')
          this.$refs.table.refresh()
          this.$refs.table.clearSelected()
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
    title: '部门名字',
    dataIndex: 'name'
  },
  {
    title: '部门描述',
    dataIndex: 'describe'
  },
  {
    title: '部门启用',
    dataIndex: 'enable',
    scopedSlots: { customRender: 'enableRadio' }
  },
  {
    title: '操作',
    dataIndex: 'action',
    scopedSlots: { customRender: 'action' }
  }
]
</script>
