<template>
  <page-header-wrapper>
    <a-card :bordered="false">
      <div class="table-page-search-wrapper">
        <a-form layout="inline">
          <a-row :gutter="48">
            <a-col :md="8" :sm="24">
              <a-form-item label="权限Code">
                <a-input placeholder="请输入" v-model="queryParam.code" />
              </a-form-item>
            </a-col>
            <a-col :md="8" :sm="24">
              <a-form-item label="权限类型">
                <a-select placeholder="请选择" v-model="queryParam.type">
                  <a-select-option value="0">全部</a-select-option>
                  <a-select-option value="1">按钮</a-select-option>
                  <a-select-option value="2">路由</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
            <a-col :md="8" :sm="24">
              <span class="table-page-search-submitButtons">
                <a-button type="primary" @click="$refs.table.refresh(true)">查询</a-button>
                <a-button style="margin-left: 8px" @click="queryParam = {}">重置</a-button>
              </span>
            </a-col>
          </a-row>
        </a-form>
      </div>

      <s-table ref="table" :columns="columns" :data="loadData" rowKey="id">
        <span slot="actions" slot-scope="text, record">
          <a-tag v-for="(action, index) in record.actionList" :key="index">{{ action.describe }}</a-tag>
        </span>
        <span slot="type" slot-scope="text">
          <a-tag :color="text == 1 ? 'blue' : 'cyan'">{{ text == 1 ? '按钮' : '路由' }}</a-tag>
        </span>
        <span slot="action" slot-scope="text, record">
          <a @click="handleEdit(record)">编辑</a>
          <a-divider type="vertical" />
          <a-dropdown>
            <a class="ant-dropdown-link">
              更多 <a-icon type="down" />
            </a>
            <a-menu slot="overlay">
              <a-menu-item>
                <a @click="handleEdit(record, true)">详情</a>
              </a-menu-item>
              <a-menu-item>
                <a href="javascript:;">删除</a>
              </a-menu-item>
            </a-menu>
          </a-dropdown>
        </span>
      </s-table>

      <a-modal title="操作" :width="800" v-model="visible" @ok="handleOk" :confirmLoading="confirmLoading">
        <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
          <a-form-model-item :labelCol="labelCol" prop="code" :wrapperCol="wrapperCol" label="编码" hasFeedback>
            <a-input placeholder="请输入权限编码！" v-model="form.code" :disabled="readonly" />
          </a-form-model-item>
          <a-form-model-item :labelCol="labelCol" prop="name" :wrapperCol="wrapperCol" label="权限名称" hasFeedback>
            <a-input placeholder="请输入权限名称！" v-model="form.name" :disabled="readonly" />
          </a-form-model-item>
          <a-form-model-item :labelCol="labelCol" prop="type" :wrapperCol="wrapperCol" label="权限类型">
            <a-radio-group v-model="form.type" :disabled="readonly">
              <a-radio-button :value=1>
                按钮
              </a-radio-button>
              <a-radio-button :value=2>
                路由
              </a-radio-button>
            </a-radio-group>
          </a-form-model-item>
        </a-form-model>
      </a-modal>

    </a-card>
  </page-header-wrapper>
</template>

<script>
import { STable } from '@/components'

export default {
  name: 'TableList',
  components: {
    STable
  },
  data() {
    return {
      description: '列表使用场景：后台管理中的权限管理以及角色管理，可用于基于 RBAC 设计的角色权限控制，颗粒度细到每一个操作类型。',
      visible: false,
      readonly: false,
      labelCol: {
        xs: { span: 24 },
        sm: { span: 5 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 16 }
      },
      confirmLoading: false,
      form: {},
      rules: {
        code: [{ required: true, message: '请输入权限编码！' }],
        name: [{ required: true, message: '请输入权限名称！' }]
      },
      mdl: {},
      // 高级搜索 展开/关闭
      advanced: false,
      // 查询参数
      queryParam: {},
      // 表头
      columns: [
        {
          title: '编码',
          dataIndex: 'code'
        },
        {
          title: '权限名称',
          dataIndex: 'name'
        },
        {
          title: '类型',
          dataIndex: 'type',
          scopedSlots: { customRender: 'type' }
        },
        {
          title: '操作',
          width: '150px',
          dataIndex: 'action',
          scopedSlots: { customRender: 'action' }
        }
      ],
      // 向后端拉取可以用的操作列表
      permissionList: null,
      // 加载数据方法 必须为 Promise 对象
      loadData: parameter => {
        return this.$http.get('/sysPermissions/page', {
          params: Object.assign(parameter, this.queryParam)
        }).then(res => {
          const result = res.result
          return result
        })
      },

      selectedRowKeys: [],
      selectedRows: []
    }
  },
  methods: {
    handleEdit(record, readonly) {
      this.form = Object.assign({}, record)
      this.visible = true
      if (readonly) {
        this.readonly = true
      }
      setTimeout(() => {
        this.$refs.form.clearValidate()
      }, 2000)

      this.confirmLoading = false
    },
    handleOk() {
      if (this.readonly) {
        this.visible = false
        return
      }
      this.$refs.form.validate(valid => {
        if (valid) {
          this.confirmLoading = true
          this.$http.put('/SysPermissions', this.form).then(res => {
            if (res.code === 200) {
              this.$emit('ok')
              this.$refs.table.refresh(true)
              this.visible = false
            } else {
              this.$message.warning(res.message)
            }
          }).finally(() => {
            this.confirmLoading = false
          })
        }
      })
    },
    onChange(selectedRowKeys, selectedRows) {
      this.selectedRowKeys = selectedRowKeys
      this.selectedRows = selectedRows
    },
    toggleAdvanced() {
      this.advanced = !this.advanced
    }
  },
  watch: {
    /*
      'selectedRows': function (selectedRows) {
        this.needTotalList = this.needTotalList.map(item => {
          return {
            ...item,
            total: selectedRows.reduce( (sum, val) => {
              return sum + val[item.dataIndex]
            }, 0)
          }
        })
      }
      */
  }
}
</script>
