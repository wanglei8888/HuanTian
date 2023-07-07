<template>
  <page-header-wrapper>
    <a-card :bordered="false">
      <div v-show="codeGenDetailShow">
        <div class="table-page-search-wrapper">
          <a-form layout="inline">
            <a-row :gutter="48">
              <a-col :md="8" :sm="24">
                <a-form-item label="名称">
                  <a-input v-model="queryParam.name" placeholder="" />
                </a-form-item>
              </a-col>
              <a-col :md="8" :sm="24">
                <a-form-item label="表名">
                  <a-input v-model="queryParam.tableName" placeholder="" />
                </a-form-item>
              </a-col>
              <a-col :md="!advanced && 8 || 24" :sm="24">
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
          <a-button type="primary" icon="plus" @click="$refs.codeGenModal.detail()">新建</a-button>
          <a-dropdown v-action:edit v-if="selectedRowKeys.length > 0">
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

        <s-table ref="table" size="default" rowKey="id" :columns="columns" :data="loadData" :alert="true"
          :rowSelection="rowSelection" showPagination="auto">
          <!-- :showPagination="false" -->
          <span slot="enable" slot-scope="text">
            <a-tag :color="text ? 'green' : 'red'">{{ text | enableFilter }}</a-tag>
          </span>
          <span slot="language" slot-scope="text">
            {{ text | languageFilter }}
          </span>
          <span slot="avatar" slot-scope="text" style="margin-left: -13px;">
            <img style="width:75px;heigth:75px" slot="avatar" :src=text />
          </span>

          <span slot="action" slot-scope="text, record">
            <template>
              <a @click="$refs.codeGenModal.detail(record)">编辑</a>
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
                    <a href="javascript:;" @click="detailOpen(record)">配置信息</a>
                  </a-menu-item>
                  <a-menu-item>
                    <a-popconfirm title="是否要开始生成？" @confirm="runLocal(record.id)">
                      <a>开始生成</a>
                    </a-popconfirm>
                  </a-menu-item>
                </a-menu>
              </a-dropdown>
            </template>
          </span>
        </s-table>
        <code-gen-modal ref="codeGenModal" @ok="handleOk" />
      </div>
      <code-gen-detail-modal ref="codeGenDetailModal" @ok="detailHandleOk" />
    </a-card>
  </page-header-wrapper>
</template>

<script>
import moment from 'moment'
import { STable } from '@/components'
import { downLoadPost } from '@/api/downLoad'
import codeGenModal from './modules/CodeGenModal'
import codeGenDetailModal from './modules/CodeGenDetailModal'


export default {
  components: {
    STable,
    codeGenModal,
    codeGenDetailModal
  },
  data() {
    this.columns = columns
    return {
      // create model
      visible: false,
      confirmLoading: false,
      mdl: null,
      // 高级搜索 展开/关闭
      advanced: false,
      codeGenDetailShow: true,
      // 查询参数
      queryParam: {},
      // 加载数据方法 必须为 Promise 对象
      loadData: parameter => {
        const requestParameters = Object.assign({}, parameter, this.queryParam)
        return this.$http.get('/sysCodeGen/page', { params: requestParameters }).then(res => {
          return res.result
        })
      },
      selectedRowKeys: [],
      selectedRows: []
    }
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
    remove(key, multipleChoice) {
      this.$http.delete('/sysCodeGen', { data: { Id: key.toString() } }).then(res => {
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
    runLocal(key) {
      downLoadPost({ url: '/sysCodeGen/RunLocal', params: { Id: key } })
    },
    detailOpen(record) {
      this.codeGenDetailShow = false
      this.$refs.codeGenDetailModal.detail(record)
    },
    handleOk() {
      this.$refs.table.refresh()
    },
    detailHandleOk() {
      this.codeGenDetailShow = true
    },
    onSelectChange(selectedRowKeys, selectedRows) {
      this.selectedRowKeys = selectedRowKeys
      this.selectedRows = selectedRows
    },
    toggleAdvanced() {
      this.advanced = !this.advanced
    }
  }
}

const columns = [
  {
    title: '名称',
    dataIndex: 'name'
  },
  {
    title: '表格名称',
    dataIndex: 'tableName'
  },
  {
    title: '所属菜单',
    dataIndex: 'menuId',
    customRender: (text, row, index) => {
      if (text == 0) {
        return '无'
      }
      return text
    },
  },
  {
    title: '生成方式',
    dataIndex: 'generationWay',
    customRender: (text, row, index) => {
      if (text == 0) {
        return '生成到项目'
      }
      return '打包生成'
    },
  },
  {
    title: '操作',
    dataIndex: 'action',
    width: '200px',
    scopedSlots: { customRender: 'action' }
  }
]


</script>
