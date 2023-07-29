<template>
  <a-modal
    :title="title"
    :width="1000"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleOk"
    @cancel="handleCancel">
    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="10" :sm="24">
            <a-form-item label="菜单名">
              <a-input v-model="queryParam.name" placeholder="" />
            </a-form-item>
          </a-col>
          <a-col :md="14" :sm="24">
            <span class="table-page-search-submitButtons">
              <a-button type="primary" :loading="selectLoading" @click="handSerach()">查询</a-button>
            </span>
          </a-col>
        </a-row>
      </a-form>
    </div>
    <a-table
      rowKey="id"
      :columns="modelColumns"
      :dataSource="modelLoadData"
      :row-selection="{
        onChange,
        onSelect,
        onSelectAll,
        selectedRowKeys,
      }"
      :pagination="false"
      :expandIconAsCell="false"
      :loading="tableLoading"
      :scroll="{ y: 320 }">
      <span slot="serial" slot-scope="text">
        <a-icon slot="serial" :type="text" />
      </span>
    </a-table>
  </a-modal>
</template>

<script>

const columns = [
  {
    title: '菜单名称',
    dataIndex: 'name',
    width: '30%'
  },
  {
    title: '图标',
    dataIndex: 'icon',
    width: '30%',
    scopedSlots: { customRender: 'serial' }
  },
  {
    title: '跳转地址',
    dataIndex: 'redirect',
    width: '30%'
  }
]
export default {
  name: 'RoleModal',
  data () {
    this.modelColumns = columns
    return {
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      queryParam: {},
      gutter: 24,
      mdSize: 24,
      smSize: 24,
      title: '角色菜单',
      tableLoading: false,
      visible: false,
      confirmLoading: false,
      id: 0,
      modelLoadData: [],
      selectedRowKeys: [],
      selectedRows: [],
      selectLoading: false
    }
  },
  methods: {
    onChange (selectedRowKeys, selectedRows) {
      this.selectedRowKeys = selectedRowKeys
    },
    onSelect (record, selected, selectedRows) {
    },
    onSelectAll (selected, selectedRows, changeRows) {
    },
    // 打开页面初始化
    create (value) {
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      this.id = value
      this.selectLoading = false
      this.visible = true
      this.confirmLoading = false
      this.modelLoadData = []
      this.handSerach()
    },
    handSerach () {
      this.selectLoading = true
      this.$http.get('/sysMenu/tree', { params: this.queryParam }).then(res => {
        this.modelLoadData = res.result
      }).finally(() => {
        this.selectLoading = false
      })
    },
    close () {
      this.$emit('close')
      this.visible = false
    },
    handleOk () {
      if (this.selectedRowKeys.length === 0) {
        this.$message.warning('请选择菜单')
        return
      }
      this.confirmLoading = true
      this.$http.post('/sysRole/addRoleMenu', { roleId: this.id, menuId: this.selectedRowKeys }).then(res => {
        if (res.code === 200) {
          this.$emit('ok')
          this.visible = false
        } else {
          this.$message.warning(res.message)
        }
      })
      .finally(() => {
        this.confirmLoading = false
      })
    },
    handleCancel () {
      this.close()
    }
  }
}
</script>

<style scoped lang="less">
</style>
