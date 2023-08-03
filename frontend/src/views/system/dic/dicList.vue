<template>
  <page-header-wrapper>
    <a-card :bordered="false">
      <div class="table-page-search-wrapper">
        <a-form layout="inline">
          <a-row :gutter="48">
            <a-col :md="8" :sm="24">
              <a-form-item label="类型名称">
                <a-input v-model="queryParam.name" allow-clear placeholder="请输入类型名称" />
              </a-form-item>
            </a-col>
            <a-col :md="8" :sm="24">
              <a-form-item label="唯一编码">
                <a-input v-model="queryParam.code" allow-clear placeholder="请输入唯一编码" />
              </a-form-item>
            </a-col>
            <a-col :md="8" :sm="24">
              <span class="table-page-search-submitButtons">
                <a-button type="primary" @click="$refs.table.refresh(true)">查询</a-button>
                <a-button style="margin-left: 8px" @click="() => queryParam = {}">重置</a-button>
              </span>
            </a-col>
          </a-row>
        </a-form>
      </div>

      <s-table ref="table" :columns="columns" :data="loadData" :alert="false" :rowKey="(record) => record.id">
        <template slot="operator">
          <a-button @click="$refs.dicModal.detail()" icon="plus" type="primary">新增类型</a-button>
        </template>
        <span slot="enable" slot-scope="text">
          <a-tag :color="text ? 'green' : 'red'">{{ text | enableFilter }}</a-tag>
        </span>
        <span slot="action" slot-scope="text, record">
          <a @click="$refs.dicDetailModal.detail(record)">字典</a>
          <a-divider type="vertical" />
          <a-dropdown>
            <a class="ant-dropdown-link">
              更多 <a-icon type="down" />
            </a>
            <a-menu slot="overlay">
              <a-menu-item>
                <a @click="$refs.dicModal.detail(record)">编辑</a>
              </a-menu-item>
              <a-menu-item>
                <a-popconfirm placement="topRight" title="确认删除？" @confirm="() => remove(record.id)">
                  <a>删除</a>
                </a-popconfirm>
              </a-menu-item>
            </a-menu>
          </a-dropdown>
        </span>
      </s-table>
      <dic-modal ref="dicModal" @ok="handleOk" />
      <dic-detail-modal ref="dicDetailModal" @ok="handleOk" />
    </a-card>
  </page-header-wrapper>
</template>
<script>
import { STable } from '@/components'
import dicModal from './modules/dicModal'
import dicDetailModal from './modules/dicDetailModal'
export default {
  components: {
    STable,
    dicModal,
    dicDetailModal
  },
  data() {
    return {
      // 查询参数
      queryParam: {},
      // 表头
      columns: columns,
      // 加载数据方法 必须为 Promise 对象
      loadData: parameter => {
        const requestParameters = Object.assign({}, parameter, this.queryParam)
        return this.$http.get('/sysDic/page', { params: requestParameters }).then(res => {
          if (res.code === 200) {
            return res.result
          }
        })
      },
      statusDict: []
    }
  },
  created() {

  },
  methods: {
    remove(key) {
      this.$http.delete('/sysDic', { data: { id: key.toString() } }).then(res => {
        if (res.code === 200) {
          this.$message.success('删除成功')
          this.$refs.table.refresh()
        }
      })
    },
    handleOk() {
      this.$refs.table.refresh()
    }
  },
  filters: {
    enableFilter(type) {
      if (type === true) {
        return '启用'
      }
      else {
        return '禁用'
      }
    }
  },
}
const columns = [
  {
    title: '字典名称',
    width: '25%',
    dataIndex: 'name'
  },
  {
    title: '唯一编码',
    width: '25%',
    dataIndex: 'code'
  },
  {
    title: '状态',
    width: '25%',
    dataIndex: 'enable',
    scopedSlots: { customRender: 'enable' }
  },
  {
    title: '操作',
    dataIndex: 'action',
    scopedSlots: { customRender: 'action' }
  }
]
</script>
<style lang="less">
.table-operator {
  margin-bottom: 18px;
}

button {
  margin-right: 8px;
}
</style>
