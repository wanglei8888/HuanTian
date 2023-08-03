<template>
  <a-modal
    :title="title"
    :width="900"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="confirmLoading">
      <div class="table-page-search-wrapper">
        <!-- <a-form layout="inline">
          <a-row :gutter="48">
            <a-col :md="12" :sm="24">
              <a-form-item label="字典名称">
                <a-input v-model="queryParam.name" allow-clear placeholder="请输入字典名称" />
              </a-form-item>
            </a-col>
            <a-col :md="12" :sm="24">
              <span class="table-page-search-submitButtons">
                <a-button type="primary" @click="handSearch()">查询</a-button>
                <a-button style="margin-left: 8px" @click="() => queryParam = {}">重置</a-button>
              </span>
            </a-col>
          </a-row>
        </a-form> -->
        <a-row style="margin-bottom: 8px;">
          <a-button
            @click="handleAdd()"
            icon="plus"
            type="primary"
            :disabled="queryParam.name ? true : false">新增数据</a-button>
        </a-row>
        <a-row>
          <a-table
            ref="table"
            :columns="columns"
            :rowKey="(record) => record.id"
            :data-source="data"
            :loading="tableLoading"
            :pagination="false">
            <template
              v-for="(col, index) in ['name', 'value', 'order', 'enable']"
              :slot="col"
              slot-scope="text, record, index">
              <div :key="col">
                <a-select
                  v-if="col === 'enable'"
                  style="width: 120px"
                  @change="(e) => enableChange(index, e)"
                  :value="text"
                  :disabled="record.editable ? false : true">
                  <a-select-option :value="true">
                    启用
                  </a-select-option>
                  <a-select-option :value="false">
                    禁用
                  </a-select-option>
                </a-select>
                <a-input
                  v-else-if="record.editable"
                  style="margin: -5px 0"
                  :value="text"
                  @change="e => handleChange(e.target.value, record.id, col)" />
                <template v-else>
                  <div>{{ text }}</div>
                </template>
              </div>
            </template>
            <template slot="operation" slot-scope="text, record, index">
              <div class="editable-row-operations">
                <span v-if="record.editable" style="display: flex;">
                  <a @click="() => save(record.id)" style="padding-right: 20px;">保存</a>
                  <a-popconfirm title="确定取消吗?" @confirm="() => cancel(record.id)">
                    <a style="margin-right: 10px;">取消</a>
                  </a-popconfirm>
                </span>
                <span v-else style="display: flex;">
                  <a :disabled="editingKey !== ''" @click="() => edit(record.id)" style="padding-right: 20px;">编辑</a>
                  <a-popconfirm title="确定删除吗?" @confirm="() => remove(record.id)">
                    <a style="margin-right: 10px;">删除</a>
                  </a-popconfirm>
                </span>
              </div>
            </template>
          </a-table>
        </a-row>
      </div>
    </a-spin>
  </a-modal>
</template>

<script>
import { STable } from '@/components'
export default {
  components: {
    STable
  },
  data () {
    return {
      gutter: 24,
      mdSize: 24,
      smSize: 24,
      title: '编辑字典',
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      visible: false,
      queryParam: {},
      confirmLoading: false,
      info: {},
      data: [],
      cacheData: [],
      columns: columns,
      editingKey: '',
      tableLoading: false
    }
  },
  methods: {
    enableChange (index, e) {
      this.$set(this.data[index], 'enable', e)
    },
    handSearch () {
      this.tableLoading = true
      this.queryParam.code = this.info.code
      this.$http.get('/sysDic', { params: this.queryParam }).then(res => {
        if (res.code === 200) {
          this.data = res.result
        }
      }).finally(() => {
        this.tableLoading = false
      })
    },
    remove (key) {
      const target = this.data.find(item => key === item.id)
      if (target) {
        this.data = this.data.filter(item => item.id !== key)
      }
    },
    handleAdd () {
      const count = this.data.length + 1
      const newData = {
        id: count,
        name: '',
        value: '',
        order: count,
        masterId: this.info.id,
        enable: true,
        deleted: false
      }
      this.data = [...this.data, newData]
      this.edit(count)
    },
    handleChange (value, key, column) {
      const newData = [...this.data]
      const target = newData.find(item => key === item.id)
      if (target) {
        target[column] = value
        this.data = newData
      }
    },
    edit (key) {
      const newData = [...this.data]
      const target = newData.find(item => key === item.id)
      this.editingKey = key
      if (target) {
        target.editable = true
        this.data = newData
      }
    },
    save (key) {
      this.cacheData = this.data.map(item => ({ ...item }))
      const newData = [...this.data]
      const newCacheData = [...this.cacheData]
      const target = newData.find(item => key === item.id)
      const targetCache = newCacheData.find(item => key === item.id)
      if (target && targetCache) {
        delete target.editable
        this.data = newData
        Object.assign(targetCache, target)
        this.cacheData = newCacheData
      }
      this.editingKey = ''
    },
    cancel (key) {
      const newData = [...this.data]
      const target = newData.find(item => key === item.id)
      this.editingKey = ''
      if (target) {
        Object.assign(target, this.cacheData.find(item => key === item.id))
        delete target.editable
        this.data = newData
      }
    },
    // 初始化方法
    detail (record) {
      this.info = record
      this.handSearch()
      this.visible = true
    },
    close () {
      this.$emit('close')
      this.visible = false
    },
    handleOk () {
      this.confirmLoading = true
      const paramas = {
        SysDicDetail: this.data,
        masterId: this.info.id
      }
      this.$http.post('/sysDic/Detail', paramas).then(res => {
        if (res.code === 200) {
          this.$emit('ok')
          this.visible = false
          this.$message.success('保存成功')
        }
      }).finally(() => {
        this.confirmLoading = false
      })
    },
    handleCancel () {
      this.close()
    }
  }
}

const columns = [
  {
    title: '字典名称',
    dataIndex: 'name',
    width: '20%',
    scopedSlots: { customRender: 'name' }
  },
  {
    title: '字典值',
    dataIndex: 'value',
    width: '20%',
    scopedSlots: { customRender: 'value' }
  },
  {
    title: '排序',
    dataIndex: 'order',
    width: '20%',
    scopedSlots: { customRender: 'order' }
  },
  {
    title: '状态',
    dataIndex: 'enable',
    width: '20%',
    scopedSlots: { customRender: 'enable' }
  },
  {
    title: '操作',
    dataIndex: 'operation',
    scopedSlots: { customRender: 'operation' }
  }
]
</script>
