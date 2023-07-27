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
        <a-row style="margin-bottom: 8px;">
          <a-button
            @click="handleAdd()"
            icon="plus"
            type="primary">新增数据</a-button>
          <a-tooltip placement="topLeft" title="新增增删改查按钮权限">
            <a-button
              @click="handleCrudAdd()"
              icon="plus"
              style="margin-left: 8px"
              type="primary">常规权限</a-button>
          </a-tooltip>
          <a-tooltip placement="topLeft" title="新增增删改查路由权限">
            <a-button
              @click="handleRouteAdd()"
              icon="plus"
              style="margin-left: 8px"
              type="primary">常规路由</a-button>
          </a-tooltip>
        </a-row>
        <a-row>
          <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
            <a-table
              ref="table"
              :columns="columns"
              :rowKey="(record) => record.id"
              :data-source="form.data"
              :loading="tableLoading"
              :scroll="{ y: 400 }"
              :pagination="false">
              <template slot="type" slot-scope="text, record,index">
                <a-form-model-item>
                  <a-select
                    style="width: 120px;background: #fff;color: rgba(0, 0, 0, 0.65);"
                    v-model="record.type"
                    :disabled="record.editable ? false : true">
                    <a-select-option :value="1">
                      按钮
                    </a-select-option>
                    <a-select-option :value="2">
                      路由
                    </a-select-option>
                  </a-select>
                </a-form-model-item>
              </template>
              <template v-for="(col, index) in ['name', 'code']" :slot="col" slot-scope="text, record,index">
                <a-form-model-item :prop="`data.${index}.${col}`" :key="index" :rules="rules[col]">
                  <template v-if="record.editable">
                    <a-input style="margin: -5px 0" v-model="record[col]" />
                  </template>
                  <template v-else>
                    <div>{{ text }}</div>
                  </template>
                </a-form-model-item>
              </template>
              <template slot="operation" slot-scope="text, record, index">
                <a-form-model-item>
                  <div class="editable-row-operations">
                    <span v-if="record.editable" style="display: flex;">
                      <a @click="() => save(record.id)" style="padding-right: 20px;">保存</a>
                      <a-popconfirm title="确定取消吗?" @confirm="() => cancel(record.id)">
                        <a style="margin-right: 10px;">取消</a>
                      </a-popconfirm>
                    </span>
                    <span v-else style="display: flex;">
                      <a
                        :disabled="editingKey !== ''"
                        @click="() => edit(record.id, false)"
                        style="padding-right: 20px;">编辑</a>
                      <a-popconfirm title="确定删除吗?" @confirm="() => remove(record.id)">
                        <a style="margin-right: 10px;">删除</a>
                      </a-popconfirm>
                    </span>
                  </div>
                </a-form-model-item>
              </template>
            </a-table>
          </a-form-model>
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
      title: '编辑数据',
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
      form: { data: [] },
      confirmLoading: false,
      info: {},
      data: [],
      columns: columns,
      editingKey: '',
      tableLoading: false,
      rules: {
        name: [{ required: true, message: '请输入名称！' }],
        code: [{ required: true, message: '请输入编码！' }]
      }
    }
  },
  methods: {
    handSearch () {
      this.tableLoading = true
      this.queryParam.menuId = this.info.id
      this.$http.get('/sysPermissions', { params: this.queryParam }).then(res => {
        if (res.code === 200) {
          this.form.data = res.result
          this.data = res.result
        }
      }).finally(() => {
        this.tableLoading = false
      })
    },
    remove (key) {
      const target = this.form.data.find(item => key === item.id)
      if (target) {
        this.form.data = this.form.data.filter(item => item.id !== key)
      }
    },
    handleAdd () {
      const count = this.form.data.length + 1
      const newData = {
        id: count,
        name: '',
        code: '',
        menuId: this.info.id,
        type: 1,
        isNew: true
      }
      this.form.data = [...this.form.data, newData]
      this.edit(count, true)
    },
    generateUniqueId () {
      const timestamp = new Date().getTime()
      const randomNum = Math.floor(Math.random() * 1000000)
      return timestamp + randomNum
    },
    handleChange (value, key, column) {
      const newData = [...this.form.data]
      const target = newData.find(item => key === item.id)
      if (target) {
        target[column] = value
        this.form.data = newData
      }
    },
    edit (key, isNew) {
      const newData = [...this.form.data]
      const target = newData.find(item => key === item.id)
      this.editingKey = key
      if (target) {
        target.editable = true
        target.isNew = isNew
        this.form.data = newData
      }
    },
    save (key) {
      this.$refs.form.validate(valid => {
        if (!valid) return
        const newData = [...this.form.data]
        const target = newData.find(item => key === item.id)
        target.isNew = false
        if (target) {
          delete target.editable
          this.form.data = newData
        }
        this.editingKey = ''
      })
    },
    cancel (key) {
      const newData = [...this.form.data]
      const target = newData.find(item => key === item.id)
      this.editingKey = ''
      if (target) {
        delete target.editable
        this.form.data = newData
        if (target.isNew) {
          this.remove(key)
        }
      }
    },
    handleCrudAdd () {
      const newData = [
        {
        id: this.generateUniqueId(),
        name: '增加按钮',
        code: 'add',
        menuId: this.info.id,
        type: 1,
        isNew: false
      },
      {
        id: this.generateUniqueId(),
        name: '修改按钮',
        code: 'update',
        menuId: this.info.id,
        type: 1,
        isNew: false
      },
      {
        id: this.generateUniqueId(),
        name: '删除按钮',
        code: 'delete',
        menuId: this.info.id,
        type: 1,
        isNew: false
      },
      {
        id: this.generateUniqueId(),
        name: '查询按钮',
        code: 'get',
        menuId: this.info.id,
        type: 1,
        isNew: false
      }
    ]
      this.form.data = [...this.form.data, ...newData]
    },
    handleRouteAdd () {
      const newData = [
        {
        id: this.generateUniqueId(),
        name: '增加路由',
        code: 'add',
        menuId: this.info.id,
        type: 2,
        isNew: false
      },
      {
        id: this.generateUniqueId(),
        name: '修改路由',
        code: 'update',
        menuId: this.info.id,
        type: 1,
        isNew: false
      },
      {
        id: this.generateUniqueId(),
        name: '删除路由',
        code: 'delete',
        menuId: this.info.id,
        type: 1,
        isNew: false
      },
      {
        id: this.generateUniqueId(),
        name: '查询路由',
        code: 'get',
        menuId: this.info.id,
        type: 1,
        isNew: false
      },
      {
        id: this.generateUniqueId(),
        name: '分页路由',
        code: 'page',
        menuId: this.info.id,
        type: 1,
        isNew: false
      }
    ]
      this.form.data = [...this.form.data, ...newData]
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
      if (!this.form.data.length) {
        this.$message.error('请添加菜单权限')
        return
      }
      this.confirmLoading = true
      this.$http.post('/sysPermissions', this.form.data).then(res => {
        if (res.code === 200) {
          this.$message.success('保存成功')
          this.visible = false
          this.$emit('ok')
        } else {
          this.$message.warning(res.message)
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
    title: '名称',
    dataIndex: 'name',
    width: '25%',
    scopedSlots: { customRender: 'name' }
  },
  {
    title: '编码',
    dataIndex: 'code',
    width: '25%',
    scopedSlots: { customRender: 'code' }
  },
  {
    title: '类型',
    dataIndex: 'type',
    width: '25%',
    scopedSlots: { customRender: 'type' }
  },
  {
    title: '操作',
    dataIndex: 'operation',
    scopedSlots: { customRender: 'operation' }
  }
]
</script>
