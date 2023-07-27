<template>
  <page-header-wrapper>
    <a-row :gutter="12">
      <a-col :md="3" :sm="24">
        <a-card :bordered="false" :loading="treeLoading">
          <div>
            <a-tree
              :treeData="treeData"
              @select="treeSelect"
              :defaultExpandAll="true"
              :replaceFields="{
                key: 'value',
                title: 'name',
                value: 'value'
              }" />
          </div>
        </a-card>
      </a-col>
      <a-col :md="21" :sm="24">
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
                  <span
                    class="table-page-search-submitButtons"
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
              </a-menu>
              <a-button style="margin-left: 8px">
                批量操作 <a-icon type="down" />
              </a-button>
            </a-dropdown>
          </div>

          <a-table
            rowKey="id"
            :columns="columns"
            :dataSource="loadData"
            :pagination="false"
            :expandIconAsCell="false"
            :loading="tableLoading">
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
              <a-divider type="vertical" />
              <a-dropdown>
                <a class="ant-dropdown-link">
                  更多 <a-icon type="down" />
                </a>
                <a-menu slot="overlay">
                  <a-menu-item>
                    <a href="javascript:;" @click="$refs.menuPermsModal.detail(record)">菜单权限</a>
                  </a-menu-item>
                </a-menu>
              </a-dropdown>
            </template>
            <span slot="serial" slot-scope="text">
              <a-icon slot="serial" :type="text" />
            </span>
          </a-table>
          <menu-perms-modal ref="menuPermsModal" @ok="modelOk" />
          <menu-modal ref="detailForm" @ok="modelOk" />
        </a-card>
      </a-col>
    </a-row>

  </page-header-wrapper>
</template>

<script>
import moment from 'moment'
import { STable } from '@/components'
import { sysDict } from '@/api/system'
import menuModal from './modules/menuModal'
import menuPermsModal from './modules/menuPermsModal'
import { removeEmptyChildren } from '@/utils/common'

export default {
  name: 'TableList',
  components: {
    STable,
    menuModal,
    menuPermsModal
  },
  data () {
    this.columns = columns
    return {
      tableLoading: false,
      treeLoading: false,
      // 高级搜索 展开/关闭
      advanced: false,
      treeInfo: [],
      // 查询参数
      queryParam: {},
      treeData: [],
      // 加载数据方法 必须为 Promise 对象
      loadData: [],
      selectedRowKeys: [],
      selectedRows: []
    }
  },
  created () {
    this.handSerach()
    this.getTreeData()
  },
  computed: {
    rowSelection () {
      return {
        selectedRowKeys: this.selectedRowKeys,
        onChange: this.onSelectChange
      }
    }
  },
  methods: {
    treeSelect (e) {
      this.queryParam.menuType = e[0]
      this.handSerach()
    },
    getTreeData () {
      this.treeLoading = true
      sysDict({ code: 'MenuType' }).then((res) => {
        if (res.code === 200) {
          this.treeData = res.result
        }
      }).finally(() => {
        this.treeLoading = false
      })
    },
    remove (key) {
      this.$http.delete('/sysMenu', { data: { id: `${key}` } }).then(res => {
        if (res.code === 200) {
          this.$message.success('删除成功')
          this.handSerach()
        }
      })
    },
    handSerach () {
      this.tableLoading = true
      this.$http.get('/sysMenu/tree', { params: this.queryParam }).then(res => {
        this.loadData = res.result
        removeEmptyChildren(this.loadData)
      }).finally(() => {
        this.tableLoading = false
      })
    },
    modelOk () {
      this.handSerach()
    },
    onSelectChange (selectedRowKeys, selectedRows) {
      this.selectedRowKeys = selectedRowKeys
      this.selectedRows = selectedRows
    },
    // removeEmptyChildren(data) {
    //   if (data == null || data.length === 0) return
    //   for (let i = 0; i < data.length; i++) {
    //     const item = data[i]
    //     if (item.children != null && item.children.length === 0) {
    //       item.children = null
    //     } else {
    //       this.removeEmptyChildren(item.children)
    //     }
    //   }
    // },
    toggleAdvanced () {
      this.advanced = !this.advanced
    }
  }
}
const columns = [
  {
    title: '菜单名称',
    dataIndex: 'name',
    width: '15%'
  },
  {
    title: '图标',
    dataIndex: 'icon',
    scopedSlots: { customRender: 'serial' },
    width: '5%'
  },
  {
    title: '多语言值',
    dataIndex: 'title',
    width: '25%'
  },
  {
    title: '跳转地址',
    dataIndex: 'redirect'
  },
  {
    title: '菜单路由',
    dataIndex: 'component',
    width: '15%'
  },
  {
    title: '操作',
    key: 'action',
    scopedSlots: { customRender: 'operation' }
  }
]

</script>
<!-- <style>
.ant-tree li{
  margin: 5px 0;
    padding: 4px 0;
    white-space: nowrap;
    list-style: none;
    outline: 0;
    font-size: 15px;
}
</style> -->
